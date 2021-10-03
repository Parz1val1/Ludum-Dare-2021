using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public PlayerController controller;
    public Transform killFloor;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    Vector3 lastPlayerPosition;
    bool jump = false;
    bool jumped = false;

    private void Start()
    {
        lastPlayerPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        if (this.transform.position.y < killFloor.position.y)
        {
            this.transform.position = lastPlayerPosition;
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            this.GetComponent<Rigidbody2D>().angularVelocity = 0f;
        }
    }

    void FixedUpdate()
    {
        jumped = controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        if (jumped)
        {
            lastPlayerPosition = this.transform.position;
            jumped = false;
        }
        jump = false;
    }
}
