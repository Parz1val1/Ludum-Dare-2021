using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerWithPlatform : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.transform.SetParent(gameObject.transform, true);
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        col.gameObject.transform.parent = null;
    }
}
