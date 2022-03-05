using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Camera MainCam;

    public float Damage = 20f;
    public float Range = 100f;
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit Hit;
        if (Physics.Raycast(MainCam.transform.position, MainCam.transform.forward, out Hit, Range))
        {
            Enemy Enemy = Hit.transform.GetComponent<Enemy>();

            if (Enemy != null)
            {
                Enemy.TakeDamage(Damage);
            }
        }
    }
}
