using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController
{
   // PlayerInputManager.instance.playerCount;
}
public class Bed : MonoBehaviour
{
    private Action OnPlayersReady;
    private int playerReadyCount = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerReadyCount++;
            if (playerReadyCount == PlayerInputManager.instance.playerCount)
            {
                OnPlayersReady?.Invoke();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerReadyCount--;
        }
    }


    public void SubscribeOnPlayersReady(Action _action)
    {
        OnPlayersReady += _action;
        
    }
    public void UnsubscribeOnPlayersReady(Action _action)
    {
        OnPlayersReady -= _action;
    }

}
