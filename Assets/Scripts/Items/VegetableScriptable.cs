using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/VegetableScriptable", order = 3)]
public class VegetableScriptable : ScriptableObject
{
    public GameObject prefab;

    public int value;
    public Sprite sprite;
}
