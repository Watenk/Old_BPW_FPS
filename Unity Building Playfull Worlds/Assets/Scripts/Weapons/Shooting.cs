using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public enum State { pistol, shotgun, machinegun }
    State state;

    public string currentWeapon;
    public float currentReloadCooldown;
    public int currentAmmo;
    public int currentMaxAmmo;

    //References
    public Pistol pistol;
    public Shotgun shotgun;
    public Machinegun machinegun;

    private void Update()
    {
        switch (state)
        {
            case State.pistol:
                Pistol();
                break;

            case State.shotgun:
                Shotgun();
                break;

            case State.machinegun:
                Machinegun();
                break;
        }

        if (Input.GetKeyDown("1"))
        {
            state = State.pistol;
        }

        if (Input.GetKeyDown("2"))
        {
            state = State.shotgun;
        }

        if (Input.GetKeyDown("3"))
        {
            state = State.machinegun;
        }
    }

    void Pistol()
    {
        //Switch Weapon
        currentWeapon = "pistol";
        pistol.gameObject.SetActive(true);
        shotgun.gameObject.SetActive(false);
        machinegun.gameObject.SetActive(false);

        currentReloadCooldown = pistol.currentReloadCooldown;
        currentAmmo = pistol.currentAmmo;
        currentMaxAmmo = pistol.maxAmmo;
    }

    void Shotgun()
    {
        //Switch Weapon
        currentWeapon = "shotgun";
        pistol.gameObject.SetActive(false);
        shotgun.gameObject.SetActive(true);
        machinegun.gameObject.SetActive(false);

        currentReloadCooldown = shotgun.currentReloadCooldown;
        currentAmmo = shotgun.currentAmmo;
        currentMaxAmmo = shotgun.maxAmmo;
    }

    void Machinegun()
    {
        //Switch Weapon
        currentWeapon = "machinegun";
        pistol.gameObject.SetActive(false);
        shotgun.gameObject.SetActive(false);
        machinegun.gameObject.SetActive(true);

        currentReloadCooldown = machinegun.currentReloadCooldown;
        currentAmmo = machinegun.currentAmmo;
        currentMaxAmmo = machinegun.maxAmmo;
    }
}