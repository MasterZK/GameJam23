using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum EGameState
{
    DAY,
    NIGHT
}
public class GameStateManager : MonoBehaviour
{
    [SerializeField] private EGameState currState;

    [Header("Day")]
    [SerializeField] private GameObject dayLevel;
    [SerializeField] private Slider sunSlider;
    [SerializeField, Min(0.01f)] private float dayTime = 60f;
    private float dayTimer;
    public UnityEvent OnDayBreak;

    [Header("Night")]
    [SerializeField] private GameObject nightLevel;
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
        //state exit
        switch (currState)
        {
            case EGameState.DAY:
                break;
            case EGameState.NIGHT:
                break;
        }
        currState = _nextState;
        //state enter
        switch (currState)
        {
            case EGameState.DAY:
                OnDayBreak?.Invoke();
                DayTimer = 0;
                //TODO:Set player position
                break;
            case EGameState.NIGHT:
                OnNightFall?.Invoke();
                //TODO:Set player position
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
