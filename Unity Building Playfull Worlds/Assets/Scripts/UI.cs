using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    private Win WinScript;
    private CharacterControlScript CharacterControlScript;
    private Shooting ShootingScript;
    private Ability AbilityScript;
    private Shop ShopScript;

    //Abilities
    public Text superFlash;
    public Text dubbleJump;

    //Health
    public Slider HealthSlider;

    //Ammo
    public Text CurrentAmmo;
    public Text TotalAmmo;

    //reload
    public Slider ReloadingSlider;
    public Slider ReloadTimeSlider;
    public Text needToReloadText;

    //Shop
    public Text CoinsText;
    public Text ShopText;

    //Kills
    public Text killCounter;

    //Boss
    public Text BossWall;

    //--------------------------------------------//

    void Start()
    {
        WinScript = FindObjectOfType<Win>();
        CharacterControlScript = FindObjectOfType<CharacterControlScript>();
        ShootingScript = FindObjectOfType<Shooting>();
        AbilityScript = FindObjectOfType<Ability>();
        ShopScript = FindObjectOfType<Shop>();
    }

    void Update()
    {
        Health();
        CalcAmmo();
        Reload();
        ReloadCooldown();
        Abilities();
        Shop();
        KillCounter();
    }

    //----------------------------------------------------//

    public void Play()
    {
        SceneManager.LoadScene("Level01&02");
    }

    public void Quit()
    {
        Application.Quit();
    }


    void ReloadCooldown()
    {
        ReloadTimeSlider.value = ShootingScript.currentReloadTime;
    }

    void Health()
    {
        HealthSlider.value = CharacterControlScript.PlayerHealth;
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
            needToReloadText.gameObject.SetActive(true);
        }

        if (ShootingScript.currentAmmo >= 0.1f && ShootingScript.reloadCooldown <= 0f)
        {
            needToReloadText.gameObject.SetActive(false);
        }

        //Reload Text
        if (ShootingScript.reloadCooldown >= 0.01f)
        {
            ReloadingSlider.gameObject.SetActive(true);
            needToReloadText.text = "Reloading...";
        }

        if (ShootingScript.reloadCooldown <= 0f)
        {
            ReloadingSlider.gameObject.SetActive(false);
            needToReloadText.text = "Reload!";
        }
    }

    void Abilities()
    {
        superFlash.text = AbilityScript.superFlashAmount.ToString();
        dubbleJump.text = AbilityScript.dubbleJumpAmount.ToString();
        ReloadingSlider.value = ShootingScript.reloadCooldown;
    }

    void Shop()
    {
        CoinsText.text = ShopScript.Coins.ToString();
    }

    void KillCounter()
    {
        killCounter.text = WinScript.kills.ToString() + " / 100";
    }
}
