using UnityEngine;

public class VegtableField : MonoBehaviour
{
    VegtableSeedScriptable seed;

    private float growProgress;
    private float wiltingProgress;

    private float timer;

    private bool planted;
    private bool watered;
    private bool grown;
    private bool wilted;

    private ItemManager itemManager;

    private void Start()
    {
        itemManager = GameObject.FindObjectOfType<ItemManager>();
    }

    private void plantSeed(VegtableSeedScriptable seed)
    {
        this.seed = seed;
    }

    private void growPlant()
    {
        timer += Time.deltaTime;
        growProgress = timer / seed.growTime;

        if (timer < seed.growTime)
            return;

        grown = true;
        timer = 0;

        //spawn vegtable pick up 
        // unsimulated nocollision

    }

    private void wiltPlant()
    {
        timer += Time.deltaTime;
        wiltingProgress = timer / seed.wiltTime;

        if (timer < seed.wiltTime)
            return;

    }

    public void farmPlant()
    {
        Instantiate(seed.grownPlant, this.transform.position, Quaternion.identity);
        seed = null;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerUI>(out PlayerUI player))
        {
            var playerTool = itemManager.getToolID(player.currentItem);


        }
    }


}
