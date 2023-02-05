using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private BaseItem samen;

    public void Interact(Player player)
    {
        Debug.Log("Interact");
        if (player.EquipedItem != null)
        {
            return;
        }
        player.EquipedItem = samen;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player;
        if (collision.gameObject.TryGetComponent<Player>(out player))
        {
            player.possibleInteractable = this;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player;
        if (collision.gameObject.TryGetComponent<Player>(out player))
        {
            player.possibleInteractable = null;
        }
    }
}
