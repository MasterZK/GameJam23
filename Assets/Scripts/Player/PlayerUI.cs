using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pickUpUI;

    [SerializeField] public TextMeshProUGUI currentText;
    [SerializeField] public GameObject currentObject;

    private Items[] items = new Items[5];
    public Items currentItem;

    // Start is called before the first frame update
    void Start()
    {
        currentObject = pickUpUI.gameObject;
        currentText = pickUpUI;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
