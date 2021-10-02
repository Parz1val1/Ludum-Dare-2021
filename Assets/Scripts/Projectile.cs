using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float velocity = 5f;

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.right * velocity * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this);
    }
}
