using UnityEngine;

public enum Tools
{
    WateringCan = 1,
    Shovel = 2,
}

public class ItemManager : MonoBehaviour
{
    [SerializeField] private ToolScriptable[] tools = new ToolScriptable[3];

    public int getToolID(ScriptableObject tryTool)
    {
        if (tryTool is ToolScriptable)
            return System.Array.IndexOf(tools, tryTool);

        if (tryTool is VegtableSeedScriptable)
            return 0;

        return -1;
    }
}
