using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float PlayerMovespeed;
    public float PlayerJumpForce;
    

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject groundCheck;

    private bool isGrounded;
    private string currentSate;

    private Rigidbody2D rb;
    private Animator anim;

    private Vector2 moveImput;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>();
    }
    private void Update()
    {
        transform.Translate(new Vector3(moveImput.x,0,0) * PlayerMovespeed * Time.deltaTime);
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, 0.3f, groundLayer);

        if (moveImput.x < 0 || moveImput.x > 0)
        {
            ChangeAnimationState("PlayerWalk");
        }
        else
        {
            ChangeAnimationState("PlayerIdle");
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveImput = ctx.ReadValue<Vector2>();
        
    }
    public void OnJump(InputAction.CallbackContext ctx)
    {

        if (ctx.performed && isGrounded)
        {
            
            anim.PlayInFixedTime("PlayerJump");
            StartCoroutine(jump());
        }
    }

    private void ChangeAnimationState(string newState)
    {
        if (currentSate == newState) return;

        anim.CrossFade(newState, 0.1f);

        currentSate = newState;
  
    }

    IEnumerator jump()
    {
        yield return new WaitForSeconds(0.2f);
        rb.AddForce(Vector3.up * PlayerJumpForce, ForceMode2D.Impulse);
    } 
}
