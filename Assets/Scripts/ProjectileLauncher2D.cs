using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher2D : MonoBehaviour
{

    public Transform spawnPoint;
    public GameObject projectile;

    // Update is called once per frame
    void Update()
    {
        var worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var direction = Quaternion.LookRotation(Vector3.forward, (worldMousePosition - transform.position)) * Quaternion.Euler(0, 0, 90);
        this.transform.rotation = direction;
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(projectile, spawnPoint.transform.position, direction);
        }
    }
}
