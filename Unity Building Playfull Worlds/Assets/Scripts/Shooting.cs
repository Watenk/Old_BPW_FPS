using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Camera MainCam;

    //Weapons

    public string EquipedWeapon = "Pistol";

    //Pistol
    public float PistolAmmo = 6f;
    public float PistolTotalAmmo = 120f;
    float PistolDamage = 20f;
    float PistolRange = 100f;

    //Shotgun
    public float ShotgunAmmo = 2f;
    public float ShotgunTotalAmmo = 24f;
    float ShotgunDamage = 100f;
    float ShotgunRange = 20f;

    void Update()
    {
        //Weapons
        //Pistol
        if (Input.GetButtonDown("Fire1") && EquipedWeapon == "Pistol")
        {
            Shoot(PistolAmmo, PistolDamage, PistolRange);
            PistolAmmo = CalcAmmo(PistolAmmo);
        }

        //Shotgun
        if (Input.GetButtonDown("Fire1") && EquipedWeapon == "Shotgun") 
        {
            Shoot(ShotgunAmmo, ShotgunDamage, ShotgunRange);
            ShotgunAmmo = CalcAmmo(ShotgunAmmo); 
        }

        //WeaponSwitch
        if (Input.GetKeyDown("1"))
        {
            EquipedWeapon = "Pistol";
        }

        if (Input.GetKeyDown("2"))
        {
            EquipedWeapon = "Shotgun";
        }
    }

    void Shoot(float AmmoAmount, float DamageAmount, float RangeAmount)
    {
        if (AmmoAmount >= 1)
        {
            RaycastHit Hit;
            if (Physics.Raycast(MainCam.transform.position, MainCam.transform.forward, out Hit, RangeAmount))
            {
                Enemy Enemy = Hit.transform.GetComponent<Enemy>();

                if (Enemy != null)
                {
                    Enemy.TakeDamage(DamageAmount);
                }
            }
        }
    }

    float CalcAmmo(float AmmoAmount)
    {
        if (AmmoAmount >= 1f)
        {
            AmmoAmount -= 1f;
        }
        return AmmoAmount;
    }
}
