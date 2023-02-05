using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public enum ERatState
{
    RAT_WALK,
    RAT_JUMP,
    RAT_GNAW,
    RAT_STUN
}

public class Rat : MonoBehaviour
{
    [SerializeField] private TriggerBox triggerBox;
    [SerializeField]private ERatState currState;

    private float timer = 0;
    [Header("Health")]
    [SerializeField]private int maxHP;
    private int currHP;
    [SerializeField]private SpriteRenderer spriteRenderer;

    [Header("Walking")]
    [SerializeField]private float movementSpeed;
    private float movementModifier;

    [Header("Jump")]
    [SerializeField] private Rigidbody2D ratRB;
    [SerializeField]private Vector2 jumpDir;
    [SerializeField] private float jumpForce;
    private const float jumpDuration = 0.667f;

    [Header("Gnawing")]
    [SerializeField,Min(0.01f)]private float gnawTime = 0.01f;
    [SerializeField] private ProgressBarHandler pBar;
    [SerializeField] private Color gnawColor;

   

    [Header("Stunned")]
    [SerializeField, Min(0.01f)] private float stunTime = 0.01f;

    [Header("Animation")]
    [SerializeField]private Animator ratAnimator;
    [SerializeField] private string walkAnimation;
    [SerializeField] private string jumpAnimation;
    [SerializeField] private string gnawAnimation;
    [SerializeField] private string idleAnimation;
    private string currentSate;

    private void Start()
    {
        currHP = maxHP;
        currState = ERatState.RAT_WALK;
        pBar.SetBarColor(gnawColor);
    }
    private void Update()
    {
        //state update
        switch (currState)
        {
            case ERatState.RAT_WALK:
                transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
                //movement
                break;
            case ERatState.RAT_JUMP:
                timer += Time.deltaTime;
                if (timer >= jumpDuration)
                {
                    SetState(ERatState.RAT_GNAW);
                    break;
                }
                //jump
                break;
            case ERatState.RAT_GNAW:
                timer += Time.deltaTime;
                if (timer >= gnawTime)
                {
                    //TODO: gemüse hp weg
                    SetState(ERatState.RAT_GNAW);
                    break;
                }
                UpdateGnawBar(timer, gnawTime);
                break;
            case ERatState.RAT_STUN:
                timer += Time.deltaTime;
                if (timer >= stunTime)
                {
                    SetState(ERatState.RAT_WALK);
                }
                break;
        }
    }

    private void UpdateGnawBar(float _current,float _max)
    {
        pBar.SetValue(_current,_max);
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
            case ERatState.RAT_JUMP:

                //jump
                break;
            case ERatState.RAT_GNAW:
                pBar.SetValue(0);
                pBar.gameObject.SetActive(false);
                break;
            case ERatState.RAT_STUN:
                break;
        }
        currState = _nextState;
        //state enter
        switch (currState)
        {
            case ERatState.RAT_WALK:
                ChangeAnimationState(walkAnimation);
                break;
            case ERatState.RAT_JUMP:
                timer = 0;
                ratRB.AddForce(jumpDir * jumpForce,ForceMode2D.Impulse);
                ChangeAnimationState(jumpAnimation);
                //jump
                break;
            case ERatState.RAT_GNAW:
                timer = 0;
                pBar.SetValue(0);
                pBar.gameObject.SetActive(true);
                ChangeAnimationState(gnawAnimation);
                break;
            case ERatState.RAT_STUN:
                timer = 0;
                ChangeAnimationState(idleAnimation);
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

    private void ChangeAnimationState(string newState)
    {
        if (currentSate == newState) return;

        ratAnimator.CrossFade(newState, 0.1f);

        currentSate = newState;

    }

    private void OnEnable()
    {
        triggerBox.OnTriggered += StartJump;
    }
    private void OnDisable()
    {
        triggerBox.OnTriggered -= StartJump;
    }
    private void StartJump()
    {
        SetState(ERatState.RAT_JUMP);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(jumpDir.x,jumpDir.y,0));
    }
}