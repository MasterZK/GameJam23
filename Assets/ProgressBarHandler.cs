using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarHandler : MonoBehaviour
{
    [SerializeField,Range(0,1)] private float value; 

    [Header("UI")]
    [SerializeField] private Image fillImage; 


    public void SetBarColor(Color _color)
    {
        fillImage.color = _color;
    }
    public void SetValue(float _value,float _maxValue)
    {
        if (_maxValue ==0 )
        {
            return;
        }
        value = _value / _maxValue;
        Mathf.Clamp(value, 0, 1);
        SetFillAmount (value);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_value">value between 0 and 1</param>
    public void SetValue(float _value)
    {
        //if (_value> 1)
        //{
        //    _value = 1;
        //}
        //else if (_value<0)
        //{
        //    _value = 0;
        //}
        value = _value;
        Mathf.Clamp(value, 0, 1);
        SetFillAmount(value);
    }

    private void SetFillAmount(float _value)
    {
        fillImage.fillAmount = _value;
    }

    private void OnValidate()
    {
        SetFillAmount(value);
    }
}
