using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectsFlying : MonoBehaviour
{
    public Transform projectile;


    void Start()
    {
        InvokeRepeating("LaunchProjectile", 0f, Random.Range( 1.75f, 2.5f));
    }

    public void LaunchProjectile()
    {
        Instantiate(projectile, transform.position, Quaternion.identity);
    }
}
