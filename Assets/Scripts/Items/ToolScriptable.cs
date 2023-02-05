using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ToolScriptable", order = 2)]
public class ToolScriptable : ScriptableObject
{
    public GameObject prefab;

    public bool weaponizable;
    [Range(1f, 3f)]
    public int currentLevel;
    public float[] actionTime = new float[3];
    public Sprite[] sprites = new Sprite[3];    
}
