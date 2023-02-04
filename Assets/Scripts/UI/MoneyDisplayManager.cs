using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyDisplayManager : MonoBehaviour
{
    [SerializeField] private PlayerMoney playerMoney;

    [Header("UI-Elements")]
    [SerializeField]private TextMeshProUGUI displayText;

    private void OnEnable()
    {
        playerMoney.SubscribeToOnTotalUpdated(UpdateDisplayText);
    }
    private void OnDisable()
    {
        playerMoney.UnsubscribeFromOnTotalUpdated(UpdateDisplayText);
    }
    void Start()
    {
        displayText.text = playerMoney.CurrMoney.ToString();
    }

    //TODO: Testing only, remove
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.UpArrow))
    //    {
    //        playerMoney.AddMoney(10);
    //    }
    //    else if (Input.GetKeyDown(KeyCode.DownArrow))
    //    {
    //        playerMoney.SubtractMoney(5);
    //    }
    //}

    private void UpdateDisplayText(int _value)
    {
        displayText.text = _value.ToString();
    }
}
