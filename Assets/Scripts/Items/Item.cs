using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ScriptableObject itemType; 
    [SerializeField] public bool Interactable = true;

    [SerializeField] private Transform connectionPoint;
    [SerializeField] private Collider2D toolCollider;
    [SerializeField] private Rigidbody2D toolBody;

    private GameObject playerUI = null;
    private GameObject player = null;

    private void Start()
    {
        toolCollider = GetComponent<Collider2D>();
        toolBody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Interact()
    {
        PickUpItem();
    }

    private void PickUpItem()
    {
        playerUI.SetActive(false);

        this.transform.parent = player.transform;
        this.transform.localPosition = Vector3.zero;
        toolCollider.enabled = false;
        toolBody.simulated = false;

        //add to player inventory
    }

    public void DropItem()
    {
       // pickedUp = false;

        this.transform.parent = null;
        toolCollider.enabled = true;
        toolBody.simulated = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!Interactable)
            return;
        
        if (other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            var playerUI = player.GetComponent<PlayerUI>();

            playerUI.currentObject.SetActive(true);
            this.player = playerUI.gameObject;
            this.playerUI = playerUI.currentObject;

           // player.pickableItem 


        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!Interactable)
            return;

        playerUI.SetActive(false);

        playerUI = null;
        player = null;
    }
}
