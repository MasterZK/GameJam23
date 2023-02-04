using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Items
{
    WateringCan = 1,
    Shovel = 2,
    Seeds = 3,
    Carrot = 4,

}

public class ItemManager : MonoBehaviour
{
    [Header("Watering Can Stats")]
    [SerializeField] private float actionTimeWaterCan;

    [Header("Shovel Stats")]
    [SerializeField] private float actionTimeShovel;
    [SerializeField] private float attackDmg;


}
