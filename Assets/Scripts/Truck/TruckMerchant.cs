using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TruckMerchant : MonoBehaviour
{
    [SerializeField] private PlayerMoney playerMoney;

    [Header("Item Repulsion")]
    [SerializeField] private float repulsionStrength = 4f;
    [SerializeField] private Vector2 repulsionDirection = new Vector2(1f,1f);

    [Header("Item values")]
    [SerializeField]private int potatoValue;
    [SerializeField]private int carrotValue;
    [SerializeField]private int radishValue;

    [Header("Item sprites")]
    [SerializeField] private Sprite potatoSprite;
    [SerializeField] private Sprite carrotSprite;
    [SerializeField] private Sprite radishSprite;

    [Header("Challenges")]
    [SerializeField]private ChallengeSlotDisplay display1;
    [SerializeField]private ChallengeSlotDisplay display2;
    [SerializeField]private ChallengeSlotDisplay display3;

    void Start()
    {
        playerMoney.Initialize();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Item collisionItem = null;
        if (TryGetComponent<Item>(out collisionItem))
        {
            //TODO:itemtyp zugänglich machen und hier einbauen
            if (!TrySellItem(Items.WateringCan))
            {
                collisionItem.GetComponent<Rigidbody2D>().AddForce(repulsionDirection.normalized*repulsionStrength,ForceMode2D.Impulse);
            }
        }
    }
    private bool TrySellItem(Items _itemType)
    {
        switch (_itemType)
        {
            case Items.WateringCan:
                break;
            case Items.Shovel:
                break;
            case Items.Seeds:
                break;
            case Items.Carrot:
                break;
        }
        return false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(repulsionDirection.x, repulsionDirection.y, 0));
    }
}


