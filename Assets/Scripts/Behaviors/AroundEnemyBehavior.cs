using GameSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AroundEnemyBehavior : MonoBehaviour
{
    [SerializeField] private Transform bottomLeftCornerEdgeCheck;
    [SerializeField] private Transform bottomRightCornerEdgeCheck;
    [SerializeField] private float _timeToAdd = 10;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float rotateSpeed = 150f;
    [SerializeField] private float edgeDetectionDistance = 1f;
    [SerializeField] private bool clockwise = false;
    [SerializeField] private LayerMask whatIsGround; // A mask determining what is ground to the character
    const float groundedRadius = .01f; // Radius of the overlap circle to determine if grounded

    private bool grounded = false;
    private float groundedTimer = 5;
    private bool wasGrounded = false;
    private Vector2 facingDirection = Vector2.left;

    void Awake()
    {
        if (clockwise)
        {
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }

    void FixedUpdate()
    {
        CheckGrounded();

        if (grounded)
        {
            Move(facingDirection);

            RaycastHit2D hit = Physics2D.Raycast(bottomLeftCornerEdgeCheck.position, Vector2.down + facingDirection, edgeDetectionDistance);
            if (hit.collider == null)
            {
                //clockwise = !clockwise;
            }
            groundedTimer = 5;
        }
        else
        {
            if (wasGrounded && !grounded)
            {
                Move(-facingDirection);
            }
            transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
            groundedTimer -= Time.deltaTime;
            if(groundedTimer <= 0)
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

    private void CheckGrounded()
    {
        wasGrounded = grounded;
        grounded = false;

        // The enemy is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] leftColliders = Physics2D.OverlapCircleAll(bottomLeftCornerEdgeCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < leftColliders.Length; i++)
        {
            if (leftColliders[i].gameObject != gameObject)
            {
                grounded = true;
            }
        }
        Collider2D[] rightColliders = Physics2D.OverlapCircleAll(bottomRightCornerEdgeCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < rightColliders.Length; i++)
        {
            if (rightColliders[i].gameObject != gameObject)
            {
                grounded = true;
            }
        }
    }
}
