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
    float PistolMaxReloadAmount = 6f;
    float PistolDamage = 20f;
    float PistolRange = 100f;
    float PistolCooldownAmount = 1f;
    public float PistolCooldown = 0f;

    //Shotgun
    public float ShotgunAmmo = 2f;
    public float ShotgunTotalAmmo = 24f;
    float ShotgunMaxReloadAmount = 2f;
    float ShotgunDamage = 100f;
    float ShotgunRange = 20f;
    float ShotgunCooldownAmount = 3f;
    public float ShotgunCooldown = 0f;


    void Update()
    {

        //Weapons
        //Pistol
        if (EquipedWeapon == "Pistol")
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot(PistolAmmo, PistolDamage, PistolRange, PistolCooldown);
                PistolAmmo = CalcAmmo(PistolAmmo);
            }
            PistolTotalAmmo = ReloadCalc(PistolAmmo, PistolTotalAmmo, PistolMaxReloadAmount);
            PistolAmmo = ReloadCalc2(PistolAmmo, PistolMaxReloadAmount);
            PistolCooldown = CooldownCalc(PistolCooldown, PistolCooldownAmount);
        }

        //Shotgun
        if (EquipedWeapon == "Shotgun")
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot(ShotgunAmmo, ShotgunDamage, ShotgunRange, ShotgunCooldown);
                ShotgunAmmo = CalcAmmo(ShotgunAmmo);
            }
            ShotgunTotalAmmo = ReloadCalc(ShotgunAmmo, ShotgunTotalAmmo, ShotgunMaxReloadAmount);
            ShotgunAmmo = ReloadCalc2(ShotgunAmmo, ShotgunMaxReloadAmount);
            ShotgunCooldown = CooldownCalc(ShotgunCooldown, ShotgunCooldownAmount);
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

    //Shoot
    void Shoot(float AmmoAmount, float DamageAmount, float RangeAmount, float Cooldown)
    {
        if (AmmoAmount >= 1 && Cooldown <= 0.1f)
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

    //Ammo Calc
    float CalcAmmo(float AmmoAmount)
    {
        if (AmmoAmount >= 1f)
        {
            AmmoAmount -= 1f;
        }
        return AmmoAmount;
    }

    float ReloadCalc(float AmmoAmount, float TotalAmmo, float MaxReload)
    {
       if (Input.GetKeyDown("r"))
        {
            float ReloadAmount = MaxReload - AmmoAmount;
            TotalAmmo -= ReloadAmount;
        }
        return TotalAmmo;
    }

    float ReloadCalc2(float AmmoAmount, float MaxReload)
    {
        if (Input.GetKeyDown("r"))
        {
            float ReloadAmount = MaxReload - AmmoAmount;
            AmmoAmount += ReloadAmount;
        }
        return AmmoAmount;
    }

    //Cooldown
    float CooldownCalc(float Cooldown, float CooldownAmount)
    {
        if (Input.GetButtonDown("Fire1") && Cooldown <= 0.1f)
        {
            Cooldown += CooldownAmount;
        }

        if (Cooldown >= 0f)
        {
            Cooldown -= Time.deltaTime;
        }
        return Cooldown;
    }
}
