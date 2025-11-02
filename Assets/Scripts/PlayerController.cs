using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 vectorMove;
    [SerializeField] private float moveSpeed = 5f;
    [Range(0f, 10f)]
    [SerializeField] private float acceleration = 5f;

    private float currentSpeed = 0f;
    private Rigidbody2D rb;
    public float jumpForce = 5f;
    private SpriteRenderer spriteRenderer;

    private bool isGrounded;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.1f;

    Vector2 vecGravity;
    [SerializeField] private float fallMultiplier = 2f;
    //[SerializeField] private ParticleController particleController;

    private bool facingRight = true;

    [SerializeField] private AudioSource jump;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Movement(InputAction.CallbackContext context)
    {
        vectorMove = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jump.PlayOneShot(jump.clip);
        }
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        UpdateSpeed();
        rb.linearVelocity = new Vector2(vectorMove.x * currentSpeed, rb.linearVelocity.y);

        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }

        Flip(); 
    }

    void UpdateSpeed()
    {
        float targetSpeed = Mathf.Abs(vectorMove.x) > 0 ? moveSpeed : 0f;
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration * Time.fixedDeltaTime);
    }

    void Flip()
    {
        if (vectorMove.x > 0 && !facingRight)
        {
            facingRight = true;
            spriteRenderer.flipX = false;
        }
        else if (vectorMove.x < 0 && facingRight)
        {
            facingRight = false;
            spriteRenderer.flipX = true;
        }
    }

    void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
