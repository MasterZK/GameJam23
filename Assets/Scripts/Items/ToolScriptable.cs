using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ToolScriptable", order = 2)]
public class ToolScriptable : ScriptableObject
{
    public bool weaponizable;
    public float[] actionTime = new float[3];

}
