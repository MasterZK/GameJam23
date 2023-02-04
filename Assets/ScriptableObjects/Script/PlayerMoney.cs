using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new PlayerMoney",menuName ="ScriptableObjects/PlayerRessources/PlayerMoney")]
public class PlayerMoney : ScriptableObject
{
    private int currMoney;
    public int CurrMoney
    {
        get { return currMoney; }
    }

    private Action<int> OnMoneyChange;
    private Action<int> OnTotalUpdated;

    public void AddMoney(int _valueToAdd)
    {
        OnMoneyChange?.Invoke(_valueToAdd);
        currMoney += _valueToAdd;
        OnTotalUpdated?.Invoke(currMoney);
    }
    public bool SubtractMoney(int _valueToSubtract)
    {
        if (currMoney-_valueToSubtract >=0)
        {
            OnMoneyChange?.Invoke(_valueToSubtract);
            currMoney -= _valueToSubtract;
            OnTotalUpdated?.Invoke(currMoney);
            return true;

        }
        else
        {
            return false;
        }
    }

    public void SubscribeToOnMoneyChange(Action<int> _action)
    {
        OnMoneyChange += _action;
    }
    public void UnsubscribeFromOnMoneyChange(Action<int> _action)
    {
        OnMoneyChange -= _action;
    }
    public void SubscribeToOnTotalUpdated(Action<int> _action)
    {
        OnTotalUpdated += _action;
    }
    public void UnsubscribeFromOnTotalUpdated(Action<int> _action)
    {
        OnTotalUpdated -= _action;
    }
    

}
