using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float velocity = 5f;
    public AudioClip clip;

    private void Start()
    {
        Destroy(this.gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.right * velocity * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
        }
        else
        {
            AudioSource.PlayClipAtPoint(clip, this.transform.position);
            this.gameObject.SetActive(false);
            Destroy(this.gameObject, .5f);
        }
    }
}
