using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public enum State { pistol, shotgun, machinegun }
    State state;

    public bool shotgunUnlocked = false;
    public bool machinegunUnlocked = false;

    public string currentWeapon;
    public float currentShootCooldown;
    public float currentReloadCooldown;
    public int currentAmmo;
    public int currentMaxAmmo;
    public int currentMagazineSize;

    //References
    public Pistol pistol;
    public Shotgun shotgun;
    public Machinegun machinegun;
    public UI ui;

    private void Start()
    {
        ui = FindObjectOfType<UI>();
    }

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

        if (Input.GetKeyDown("2") && shotgunUnlocked == true)
        {
            state = State.shotgun;
        }

        if (Input.GetKeyDown("3") && machinegunUnlocked == true)
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

        currentShootCooldown = pistol.currentShootCooldown;
        currentReloadCooldown = pistol.currentReloadCooldown;
        currentAmmo = pistol.currentAmmo;
        currentMaxAmmo = pistol.currentMaxAmmo;
        currentMagazineSize = pistol.magazineSize;

        ui.reloadingSlider.maxValue = pistol.reloadCooldown;
    }

    void Shotgun()
    {
        //Switch Weapon
        currentWeapon = "shotgun";
        pistol.gameObject.SetActive(false);
        shotgun.gameObject.SetActive(true);
        machinegun.gameObject.SetActive(false);

        currentShootCooldown = shotgun.currentShootCooldown;
        currentReloadCooldown = shotgun.currentReloadCooldown;
        currentAmmo = shotgun.currentAmmo;
        currentMaxAmmo = shotgun.currentMaxAmmo;
        currentMagazineSize = shotgun.magazineSize;

        ui.reloadingSlider.maxValue = shotgun.reloadCooldown;
    }

    void Machinegun()
    {
        //Switch Weapon
        currentWeapon = "machinegun";
        pistol.gameObject.SetActive(false);
        shotgun.gameObject.SetActive(false);
        machinegun.gameObject.SetActive(true);

        currentShootCooldown = machinegun.currentShootCooldown;
        currentReloadCooldown = machinegun.currentReloadCooldown;
        currentAmmo = machinegun.currentAmmo;
        currentMaxAmmo = machinegun.currentMaxAmmo;
        currentMagazineSize = machinegun.magazineSize;

        ui.reloadingSlider.maxValue = machinegun.reloadCooldown;
    }

    public void ReloadAllAmmo()
    {
        pistol.currentMaxAmmo = pistol.maxAmmo;
        pistol.currentAmmo = pistol.magazineSize;
        shotgun.currentMaxAmmo = shotgun.maxAmmo;
        shotgun.currentAmmo = shotgun.magazineSize;
        machinegun.currentMaxAmmo = machinegun.maxAmmo;
        machinegun.currentAmmo = machinegun.magazineSize;
    }
}