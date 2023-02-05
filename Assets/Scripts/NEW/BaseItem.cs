using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new BaseItem", menuName = "ScriptableObjects/Items/BaseItem")]
public class BaseItem : ScriptableObject
{
    public string m_name;
    public Sprite sprite;
}
