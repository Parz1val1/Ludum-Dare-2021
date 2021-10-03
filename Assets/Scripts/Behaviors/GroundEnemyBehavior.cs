using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyBehavior : MonoBehaviour
{
    [SerializeField] private Transform bottomEdgeCheck;
    [SerializeField] private Transform bottomLeftEdgeCheck;
    [SerializeField] private Transform bottomRightEdgeCheck;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float edgeDetectionDistance = 1f;
    [SerializeField] private LayerMask whatIsGround;                          // A mask determining what is ground to the character
    const float groundedRadius = .2f; // Radius of the overlap circle to determine if grounded

    private bool grounded = false;
    private bool facingRight = false;

    void FixedUpdate()
    {
        Vector2 facingDirection = facingRight ? Vector2.right : Vector2.left;
        Transform facingBottomCornerEdgeCheck = facingRight ? bottomRightEdgeCheck : bottomLeftEdgeCheck;

        checkGrounded();

        if (grounded)
        {
            // Move
            transform.Translate(facingDirection * Time.deltaTime * speed);

            RaycastHit2D hit = Physics2D.Raycast(facingBottomCornerEdgeCheck.position, Vector2.down + facingDirection, edgeDetectionDistance);
            if (hit.collider == null)
            {
                facingRight = !facingRight;
            }
        }
    }

    private void checkGrounded()
    {
        grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
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
