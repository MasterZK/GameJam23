using UnityEngine;

public class VegtableField : MonoBehaviour
{
    VegtableSeedScriptable seed;

    private SpriteRenderer spriteRenderer;
    private Sprite farmLandSprite;

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

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
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

        spriteRenderer.sprite = seed.grownPlantSprite;

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

        spriteRenderer.sprite = seed.wiltPlantSprite;

        OnUpdate -= wiltPlant;
    }

    private void farmPlant()
    {
        planted = watered = grown = wilted = false;

        Instantiate(seed.grownPlant, this.transform.position, Quaternion.identity);
        seed = null;

        spriteRenderer.sprite = farmLandSprite;
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
                farmPlant();
            }

            if (playerTool == 2 && wilted)
            {
                //start removing wilted
                spriteRenderer.sprite = farmLandSprite;
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
