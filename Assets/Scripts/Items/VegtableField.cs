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

    private void Update()
    {
        //update growing and wilting process
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
    }

    private void wiltPlant()
    {
        timer += Time.deltaTime;
        wiltingProgress = timer / seed.wiltTime;

        if (timer < seed.wiltTime)
            return;

        wilted = true;
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

            if (playerTool == 2 && grown && !wilted)
            {
                //startfarming
            }

            if (playerTool == 2 && wilted)
            {
                //start removing wilted
            }

            if (playerTool == 0 && !planted)
            {
                plantSeed(player.currentItem as VegtableSeedScriptable);
            }

            if (playerTool == 1 && planted)
            {
                watered = true;
                //start watering plant
            }
        }
    }


}
