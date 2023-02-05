using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ERatState
{
    RAT_WALK,
    RAT_GNAW,
    RAT_STUN
}

public class Rat : MonoBehaviour
{
    [SerializeField]private ERatState currState;
    [Header("Health")]
    [SerializeField]private int maxHP;
    private int currHP;
    [SerializeField]private SpriteRenderer spriteRenderer;

    [Header("Gnawing")]
    [SerializeField,Min(0.01f)]private float gnawTime = 0.01f;
    private float gnawTimer;
    [SerializeField] private Image pBarFill;
    [SerializeField] private Image pBarBackground;

    [Header("Moving")]
    private float movementSpeed;
    private float movementModifier;

    [Header("Stunned")]
    [SerializeField, Min(0.01f)] private float stunTime = 0.01f;
    private float stunTimer;

    private void Start()
    {
        currHP = maxHP;
        currState = ERatState.RAT_WALK;
    }
    private void Update()
    {
        //state update
        switch (currState)
        {
            case ERatState.RAT_WALK:
                //movement
                break;
            case ERatState.RAT_GNAW:
                gnawTimer += Time.deltaTime;
                if (gnawTimer>=gnawTime)
                {
                    //TODO: gemüse hp weg
                }
                UpdateGnawBar(gnawTimer,gnawTime);
                break;
            case ERatState.RAT_STUN:
                stunTimer += Time.deltaTime;
                if (stunTimer >= stunTime)
                {
                    SetState(ERatState.RAT_WALK);
                }
                break;
        }
    }

    private void UpdateGnawBar(float _current,float _max)
    {
        pBarFill.fillAmount = _current / _max;
    }

    public void TakeDamage(int _damage)
    {
        if (_damage == 0)
        {
            return;
        }
        currHP -= _damage;
        if (currHP<=0)
        {
            //TODO:spawn death gore particles
            Destroy(this);
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(HitFlashes());
            SetState(ERatState.RAT_STUN);
        }
    }

    private void SetState(ERatState _nextState)
    {
        //state exit
        switch (currState)
        {
            case ERatState.RAT_WALK:
                break;
            case ERatState.RAT_GNAW:
                pBarFill.fillAmount = 0;
                pBarBackground.enabled = false;
                break;
            case ERatState.RAT_STUN:
                break;
        }
        currState = _nextState;
        //state enter
        switch (currState)
        {
            case ERatState.RAT_WALK:
                break;
            case ERatState.RAT_GNAW:
                pBarFill.fillAmount = 0;
                pBarBackground.enabled = true;
                gnawTimer = 0;
                break;
            case ERatState.RAT_STUN:
                stunTimer = 0;
                break;
        }

    }

    IEnumerator HitFlashes()
    {
        spriteRenderer.enabled = false;
        new WaitForSeconds(0.1f);
        spriteRenderer.enabled = true;
        new WaitForSeconds(0.1f);
        spriteRenderer.enabled = false;
        new WaitForSeconds(0.1f);
        spriteRenderer.enabled = true;
        new WaitForSeconds(0.1f);
        spriteRenderer.enabled = false;
        new WaitForSeconds(0.1f);
        spriteRenderer.enabled = true;
        new WaitForSeconds(0.1f);
        spriteRenderer.enabled = false;
        new WaitForSeconds(0.1f);
        spriteRenderer.enabled = true;

        yield return null;
    }
}