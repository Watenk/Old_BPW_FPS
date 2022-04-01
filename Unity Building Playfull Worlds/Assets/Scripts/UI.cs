using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    private CharacterControlScript CharacterControlScript;
    private Shooting ShootingScript;
    private Ability AbilityScript;

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
    bool needToReload = false;


    //--------------------------------------------//

    void Start()
    {
        CharacterControlScript = FindObjectOfType<CharacterControlScript>();
        ShootingScript = FindObjectOfType<Shooting>();
        AbilityScript = FindObjectOfType<Ability>();
    }

    void Update()
    {
        Health();
        CalcAmmo();
        Reload();
        ReloadCooldown();
        Abilities();
    }

    //----------------------------------------------------//

    public void Play()
    {
        SceneManager.LoadScene("Level01");
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

    void Abilities()
    {
        superFlash.text = AbilityScript.superFlashAmount.ToString();
        dubbleJump.text = AbilityScript.dubbleJumpAmount.ToString();
    }
}
