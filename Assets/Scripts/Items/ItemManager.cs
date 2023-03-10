using UnityEngine;

//reference ids for tools array in itemmanager
public enum Tools
{
    WateringCan = 1,
    Shovel = 2,
}

public enum Items
{
    WateringCan = 1,
    Shovel = 2,
    Seeds = 3,
    Carrot = 4,
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
