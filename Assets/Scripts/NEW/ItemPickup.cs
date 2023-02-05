using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{

    [SerializeField] private BaseItem itemData;
    public SpriteRenderer image;
    public BaseItem ItemData
    {
        get { return itemData; }
        set
        {
            itemData = value;
            image.sprite = itemData.sprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player;
        if (collision.gameObject.TryGetComponent<Player>(out player))
        {
            player.possiblePickup = this;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player;
        if (collision.gameObject.TryGetComponent<Player>(out player))
        {
            player.possiblePickup = null;
        }
    }

//internal BaseItem PickUp()
//{
//    Destroy(this.gameObject);
//    return itemData;
//}
    internal void PickUp(out BaseItem _item)
    {
        _item = itemData;
        Destroy(this.gameObject);
    }
}
