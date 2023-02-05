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

    private event System.Action OnUpdate;

    private void Update()
    {
        if (OnUpdate != null)
            OnUpdate();
    }

    private void plantSeed(VegtableSeedScriptable seed)
    {
        this.seed = seed;
    }

    private void growPlant()
    {
        if (!watered)
            return;

        timer += Time.deltaTime;
        growProgress = timer / seed.growTime;

        if (timer < seed.growTime)
            return;

        grown = true;
        timer = 0;

        //change texture on grown

        OnUpdate -= growPlant;
        OnUpdate += wiltPlant;
    }

    private void wiltPlant()
    {
        timer += Time.deltaTime;
        wiltingProgress = timer / seed.wiltTime;

        if (timer < seed.wiltTime)
            return;

        wilted = true;

        //change texture on wilted

        OnUpdate -= wiltPlant;
    }

    private void farmPlant()
    {
        planted = watered = grown = wilted = false;

        Instantiate(seed.grownPlant, this.transform.position, Quaternion.identity);
        seed = null;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerUI>(out PlayerUI player))
        {
            player.currentObject.SetActive(true);

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
                OnUpdate += growPlant;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerUI>(out PlayerUI player))
        {
            player.currentObject.SetActive(false);
        }
    }
}
