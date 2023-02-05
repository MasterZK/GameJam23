using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ScriptableObject itemType; 
    [SerializeField] public bool Interactable = true;

    private PlayerUI playerUI = null;
    private PlayerController player = null;

    public void PickUp()
    {
        player.currentItem = this.itemType;
        player.itemInRange = null;

        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!Interactable)
            return;
        
        if (other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            var playerUI = player.GetComponent<PlayerUI>();

            playerUI.currentObject.SetActive(true);
            this.playerUI = playerUI;
            this.player = player;

            player.itemInRange = this.itemType; 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!Interactable)
            return;

        playerUI.currentObject.SetActive(false);
        playerUI = null;
        player = null;

        player.itemInRange = null;
    }
  
}
