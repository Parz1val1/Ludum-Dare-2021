using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingEnemyBehavior : MonoBehaviour
{
    [SerializeField] private Transform bottomEdgeCheck;
    [SerializeField] private bool startFacingLeft = true;
    [SerializeField] private float jumpInterval = 0.75f;
    [SerializeField] private float jumpForce = 150f;
    [SerializeField] private float jumpUpMultiplier = 3f;
    [SerializeField] private float jumpForwardMultiplier = 1f;
    [SerializeField] private LayerMask whatIsGround; // A mask determining what is ground to the character
    const float groundedRadius = .01f; // Radius of the overlap circle to determine if grounded

    private bool grounded = false;
    private bool wasGrounded = false;
    Vector2 facingDirection = Vector2.left;
    private float nextJumpTime = 0f;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        nextJumpTime = jumpInterval;

        if (!startFacingLeft)
        {
            Flip();
        }

        CheckGrounded();
    }

    void FixedUpdate()
    {
        CheckGrounded();

        if (grounded)
        {
            if (!wasGrounded)
            {
                Flip();

            }

            if (Time.time > nextJumpTime)
            {
                nextJumpTime = Time.time + jumpInterval;

                Jump(facingDirection);
            }
        }
    }

    private void Jump(Vector2 facingDirection)
    {
        _rigidbody2D.AddForce(GetForwardJumpVector(facingDirection));
    }

    private Vector2 GetForwardJumpVector(Vector2 facingDirection)
    {
        return (Vector2.up * jumpForce * jumpUpMultiplier) + (facingDirection * jumpForce * jumpForwardMultiplier);
    }

    private void Flip()
    {
        facingDirection = -facingDirection;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void CheckGrounded()
    {
        wasGrounded = grounded;
        grounded = false;

        // The enemy is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(bottomEdgeCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;
            }
        }
    }
}
