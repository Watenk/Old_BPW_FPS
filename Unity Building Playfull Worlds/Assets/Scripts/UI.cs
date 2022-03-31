using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private CharacterControlScript Player;
    private Shooting ShootingScript;

    //JumpCoolDown
    public Slider JumpCooldownSlider;

    //Health
    public Slider HealthSlider;

    //Ammo
    public Text CurrentAmmo;
    public Text TotalAmmo;

    //reload
    public Slider ReloadingSlider;
    public Slider ReloadTimeSlider;
    public Text needToReloadText;
    bool needToReload = false;


    //--------------------------------------------//

    void Start()
    {
        Player = FindObjectOfType<CharacterControlScript>();
        ShootingScript = FindObjectOfType<Shooting>();
    }

    void Update()
    {
        JumpCoolDown();
        Health();
        CalcAmmo();
        Reload();
        ReloadCooldown();
    }

    //----------------------------------------------------//

    void JumpCoolDown()
    {
        JumpCooldownSlider.value = Player.JumpCooldown;
    }

    void ReloadCooldown()
    {
        ReloadTimeSlider.value = ShootingScript.currentReloadTime;
    }

    void Health()
    {
        HealthSlider.value = Player.PlayerHealth;
    }

    void CalcAmmo()
    {
        if (ShootingScript.CurrentWeapon == "Pistol")
        {
            CurrentAmmo.text = ShootingScript.PistolArray[1].ToString() + "/6";
            TotalAmmo.text = ShootingScript.PistolArray[2].ToString();
        }

        if (ShootingScript.CurrentWeapon == "Shotgun")
        {
            CurrentAmmo.text = ShootingScript.ShotgunArray[1].ToString() + "/2";
            TotalAmmo.text = ShootingScript.ShotgunArray[2].ToString();
        }

        if (ShootingScript.CurrentWeapon == "Machinegun")
        {
            CurrentAmmo.text = ShootingScript.MachinegunArray[1].ToString() + "/32";
            TotalAmmo.text = ShootingScript.MachinegunArray[2].ToString();
        }
    }

    void Reload()
    {
        ReloadTimeSlider.value = ShootingScript.currentReloadTime;

        //Need te reload?
        if (ShootingScript.currentAmmo <= 0f)
        {
            needToReload = true;
        }

        if (ShootingScript.currentAmmo >= 0.1f)
        {
            needToReload = false;
        }

        //Reload Text
        if (needToReload == true)
        {
            needToReloadText.gameObject.SetActive(true);
        }

        if (needToReload == false)
        {
            needToReloadText.gameObject.SetActive(false);
        }
    }
}
