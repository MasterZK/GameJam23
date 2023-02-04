using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/VegtableScriptable", order = 1)]
public class VegtableSeedScriptable : ScriptableObject
{
    public float growTime;
    public float wiltTime;
    public float farmTime;

    public GameObject grownPlant;
    public Sprite wiltPlant;
}
