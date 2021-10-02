using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform player, forwardBound, backwardBound;
    [SerializeField]
    private float offset = 4f;
    private float speedOffset = 5f;

    private void Start()
    {
        this.transform.position = new Vector3(player.position.x + offset, this.transform.position.y, -10f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var forwardDiff = player.position.x - forwardBound.position.x;
        if (forwardDiff >= 0)
        {
            this.transform.position = new Vector3(this.transform.position.x +(forwardDiff * speedOffset * Time.deltaTime), this.transform.position.y, this.transform.position.z);
        }
        var backDiff = player.position.x - backwardBound.position.x;
        if (backDiff <= 0)
        {
            this.transform.position = new Vector3(this.transform.position.x + (backDiff * speedOffset * Time.deltaTime), this.transform.position.y, this.transform.position.z);
        }
    }
}
