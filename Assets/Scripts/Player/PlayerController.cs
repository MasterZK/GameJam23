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

    private Rigidbody2D rb;

    private Vector2 moveImput;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        transform.Translate(new Vector3(moveImput.x,0,0) * PlayerMovespeed * Time.deltaTime);
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, 0.3f, groundLayer);
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveImput = ctx.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && isGrounded)
        {
            rb.AddForce(Vector3.up * PlayerJumpForce, ForceMode2D.Impulse);
        }
    }
}
