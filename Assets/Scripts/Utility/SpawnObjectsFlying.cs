using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectsFlying : MonoBehaviour
{
    public Transform projectile;


    void Start()
    {
        InvokeRepeating("LaunchProjectile", 1.0f, Random.Range( 1.5f, 2f));
    }

    public void LaunchProjectile()
    {
        Instantiate(projectile, transform.position, Quaternion.identity);
    }
}
