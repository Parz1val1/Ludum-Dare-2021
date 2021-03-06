using GameSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyBehavior : MonoBehaviour
{
    [SerializeField] private Transform bottomLeftCornerEdgeCheck;
    [SerializeField] private Transform bottomRightCornerEdgeCheck;
    [SerializeField] private float _timeToAdd = 10;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float edgeDetectionDistance = 1f;
    [SerializeField] private bool startFacingLeft = true;
    [SerializeField] private LayerMask whatIsGround; // A mask determining what is ground to the character
    const float groundedRadius = .01f; // Radius of the overlap circle to determine if grounded

    private bool grounded = false;
    private float groundedTimer = 5;
    Vector2 facingDirection = Vector2.left;

    private void Awake()
    {
        if (!startFacingLeft)
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        checkGrounded();

        if (grounded)
        {
            Move(facingDirection);

            var hits = Physics2D.RaycastAll(bottomLeftCornerEdgeCheck.position, Vector2.down + facingDirection, edgeDetectionDistance);
            bool touchingGround = false;
            bool touchingAir = false;
            foreach(RaycastHit2D collision in hits)
            {
                if (collision.collider.gameObject.layer == 7 && !touchingGround)
                {
                    touchingGround = true;
                }
                else if (collision.collider.gameObject.layer == 0 && !touchingAir)
                {
                    touchingAir = true;
                }
            }
            if(touchingAir && !touchingGround)
            {
                Flip();
            }
            
        }
        else
        {
            groundedTimer -= Time.deltaTime;
            if (groundedTimer <= 0)
            {
                TimerManager.AddTimeRemaining(_timeToAdd);
                Destroy(this.gameObject);
            }
        }
    }

    private void Move(Vector2 facingDirection)
    {
        transform.Translate(facingDirection * Time.deltaTime * speed);
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingDirection = -facingDirection;
        //transform.Rotate(new Vector3(0, 180, 0));

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void checkGrounded()
    {
        grounded = false;
        bool leftGrounded = false;
        bool rightGrounded = false;

        // The enemy is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] leftColliders = Physics2D.OverlapCircleAll(bottomLeftCornerEdgeCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < leftColliders.Length; i++)
        {
            if (leftColliders[i].gameObject != gameObject)
            {
                leftGrounded = true;
            }
        }
        Collider2D[] rightColliders = Physics2D.OverlapCircleAll(bottomRightCornerEdgeCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < rightColliders.Length; i++)
        {
            if (rightColliders[i].gameObject != gameObject)
            {
                rightGrounded = true;
            }
        }

        grounded = leftGrounded && rightGrounded;
    }
}
