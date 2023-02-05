using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum EGameState
{
    INIT,
    DAY,
    NIGHT
}
public class GameStateManager : MonoBehaviour
{
    [SerializeField] private EGameState currState = EGameState.INIT;
    [SerializeField] private PlayerRuntimeCollection playerCollection;
    private List<PlayerController> playerList;
    [SerializeField]private AudioSource audioSource;

    [Header("Day")]
    [SerializeField] private GameObject dayLevel;
    [SerializeField] private GameObject daySpawn;
    [SerializeField] private AudioClip dayClip;
    [SerializeField] private Slider sunSlider;
    [SerializeField, Min(0.01f)] private float dayTime = 60f;
    private float dayTimer;
    public UnityEvent OnDayBreak;

    [Header("Night")]
    [SerializeField] private GameObject nightLevel;
    [SerializeField] private GameObject nightSpawn;
    [SerializeField] private AudioClip nightClip;
    [SerializeField] private Bed bed;
    public UnityEvent OnNightFall;

    private float DayTimer
    {
        get
        {
            return dayTimer;
        }
        set
        {
            dayTimer = value;
            if (dayTimer >= dayTime)
            {
                SetState(EGameState.NIGHT);
            }
            sunSlider.value = dayTimer / dayTime;
        }
    }
    private void Start()
    {
        dayLevel.SetActive(false);
        nightLevel.SetActive(false);
        SetState(EGameState.DAY);
    }
    private void Update()
    {
        //state update
        switch (currState)
        {
            case EGameState.DAY:
                DayTimer += Time.deltaTime;
                break;
            case EGameState.NIGHT:
                break;
        }

    }
    private void SetState(EGameState _nextState)
    {
        if (currState != EGameState.INIT)
        {
            //state exit
            switch (currState)
            {
                case EGameState.DAY:
                    dayLevel.SetActive(false);
                    break;
                case EGameState.NIGHT:
                    nightLevel.SetActive(false);
                    break;
            }
        }
        currState = _nextState;
        //state enter
        switch (currState)
        {
            case EGameState.DAY:
                OnDayBreak?.Invoke();
                dayLevel.SetActive(true);
                DayTimer = 0;

                playerList = playerCollection.GetPlayerList();
                for (int i = 0; i < playerList.Count; i++)
                {
                    playerList[i].gameObject.transform.position = daySpawn.transform.position;
                }
                audioSource.clip = dayClip;
                audioSource.Play();
                break;
            case EGameState.NIGHT:
                OnNightFall?.Invoke();
                nightLevel.SetActive(true);
                playerList = playerCollection.GetPlayerList();
                for (int i = 0; i < playerList.Count; i++)
                {
                    playerList[i].gameObject.transform.position = nightSpawn.transform.position;
                }
                audioSource.clip = nightClip;
                audioSource.Play();
                break;
        }
    }
    private void OnEnable()
    {
        bed.SubscribeOnPlayersReady((delegate { SetState(EGameState.DAY); }));
    }
    private void OnDisable()
    {
        bed.UnsubscribeOnPlayersReady((delegate { SetState(EGameState.DAY); }));
    }
}
