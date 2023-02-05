using UnityEngine;

public class Player : MonoBehaviour
{


    public IInteractable possibleInteractable = null;
    public ItemPickup possiblePickup = null;
    [SerializeField] private BaseItem equipedItem;
    public GameObject itemPickup;
    public float verticalOffset = 0;
    public float dropForce = 0;

    public BaseItem EquipedItem
    {
        get
        {
            return equipedItem;
        }
        set
        {
            equipedItem = value;
        }
    }

    private void Update()
    {
        Vector2 moveVec = new Vector2(Input.GetAxis("Horizontal"), 0);
        this.transform.Translate(moveVec * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (EquipedItem != null)
            {
                GameObject clone = Instantiate(itemPickup, transform.position + Vector3.up * verticalOffset, Quaternion.identity);
                clone.GetComponent<ItemPickup>().ItemData = EquipedItem;
                Vector2 forceDir = new Vector2(Random.Range(-1, 1), 1);
                clone.GetComponent<Rigidbody2D>().AddForce(forceDir * dropForce, ForceMode2D.Impulse);

                EquipedItem = null;
            }
            else if (possiblePickup != null)
            {
                possiblePickup.PickUp(out equipedItem);
            }
        }
        if (Input.GetKeyDown(KeyCode.F) && possibleInteractable != null)
        {
            possibleInteractable.Interact(this);
        }
    }

}
