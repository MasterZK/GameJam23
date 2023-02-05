using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerRuntimeCollection playerCollection;

    public float PlayerMovespeed;
    public float PlayerJumpForce;

    public ScriptableObject currentItem;
    public ScriptableObject itemInRange;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject groundCheck;

    private bool isGrounded;
    private bool canMove = true;
    private string currentSate;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    private Vector2 moveInput;


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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, 0.3f, groundLayer);
       
        if (canMove)
        {
            transform.Translate(new Vector3(moveInput.x, 0, 0) * PlayerMovespeed * Time.deltaTime);

            if (moveInput.x > 0.2)
            {
                spriteRenderer.flipX = false;
            }
            else if (moveInput.x < -0.2f)
            {
                spriteRenderer.flipX = true;
            }


            if (isGrounded)
            {

                if (moveInput.x > 0.2)
                {
                    ChangeAnimationState("PlayerWalk");

                }
                else if (moveInput.x < -0.2f)
                {
                    ChangeAnimationState("PlayerWalk");
                }
                else
                {
                    if (moveInput.x < -0.2f || moveInput.x < -0.2f) return;
                  
                    ChangeAnimationState("PlayerIdle");                   
                }
            }
            else
            {
                currentSate = "";
            }
        }

    }

    public void OnItemSwitch(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
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

    }
    public void OnPlayerInteract(InputAction.CallbackContext ctx)
    {
        //float time = 1;
        //string animation = "";
        //if (false)
        //{
        //    startcoroutine(playerinteraction(time, animation));
        //}
        //else
        //{
        //}
        if (ctx.performed && canMove)
        {
            StartCoroutine(PlayerAttack(0.3f, "Attack_1"));

        }

    }
    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext ctx)
    {

        if (ctx.performed && isGrounded && canMove)
        {
            anim.PlayInFixedTime("PlayerJump");

            rb.AddForce(Vector3.up * PlayerJumpForce, ForceMode2D.Impulse);
        }
    }
    private void ChangeAnimationState(string newState)
    {
        if (currentSate == newState) return;

        anim.CrossFade(newState, 0.1f);

        currentSate = newState;

    }
    public void ToggleMovement()
    {
        canMove = !canMove;
    }
    IEnumerator PlayerInteraction(float time, string animation)
    {
        canMove = false;
        ChangeAnimationState(animation);
        yield return new WaitForSeconds(time);

        canMove = true;
    }
    IEnumerator PlayerAttack(float time, string animation)
    {
        canMove = false;
        anim.PlayInFixedTime(animation);
        yield return new WaitForSeconds(time);

        canMove = true;
    }
    private void OnEnable()
    {
        playerCollection.AddToCollection(this);
    }
    private void OnDisable()
    {
        playerCollection.RemoveFromCollection(this);
    }

}
