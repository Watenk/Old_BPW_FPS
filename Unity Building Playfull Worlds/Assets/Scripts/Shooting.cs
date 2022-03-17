using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    //Refrences
    public Camera MainCam;
    private PistolScript Pistol;
    private ShotgunScript Shotgun;
    private MachinegunScript Machinegun;
    private UI UIScript;

    //Weapons
    public float CurrentAmmoAmount;
    public string EquipedWeapon = "Pistol";
    bool ShotgunUnlocked = true;
    bool MachinegunUnlocked = true;

    //bullet
    public Rigidbody Bullet;
    float Bulletspeed = 5000000f;
    public Transform GunPos;

    //Reload
    public float reloadTime = 0f;
    public float currentReloadShootCooldown;

    //ShotFlash
    public GameObject ShotLight;
    float ShotflashCooldownAmount = 0.2f;
    float ShotflashCooldown;
    bool ShotflashLightIsOn = false;
    public ParticleSystem ShotParticles;

    //Pistol
    public float PistolAmmo = 6f;
    public float PistolTotalAmmo = 120f;
    float PistolMaxReloadAmount = 6f;
    float PistolDamage = 20f;
    float PistolRange = 100f;
    float PistolCooldownAmount = 1f;
    public float PistolCooldown;
    float reloadShootCooldownPistol;
    public float reloadShootCooldownAmountPistol = 2f;

    //Shotgun
    public float ShotgunAmmo = 2f;
    public float ShotgunTotalAmmo = 24f;
    float ShotgunMaxReloadAmount = 2f;
    float ShotgunDamage = 100f;
    float ShotgunRange = 20f;
    float ShotgunCooldownAmount = 1f;
    public float ShotgunCooldown;
    float reloadShootCooldownShotgun;
    public float reloadShootCooldownAmountShotgun = 5f;

    //Machinegun
    public float MachinegunAmmo = 32f;
    public float MachinegunTotalAmmo = 240f;
    float MachinegunMaxReloadAmount = 32f;
    float MachinegunDamage = 10f;
    float MachinegunRange = 75f;
    float MachinegunCooldownAmount = 0.2f;
    public float MachinegunCooldown;
    float reloadShootCooldownMachinegun;
    public float reloadShootCooldownAmountMachinegun = 5f;

    //--------------------------------------------------//

    private void Start()
    {
        Pistol = FindObjectOfType<PistolScript>();
        Shotgun = FindObjectOfType<ShotgunScript>();
        Machinegun = FindObjectOfType<MachinegunScript>();
        UIScript = FindObjectOfType<UI>();

        //Disable shotgun model
        Shotgun.gameObject.SetActive(false);

        //Disable MachineGun model
        Machinegun.gameObject.SetActive(false);
    }

    //----------------------------------------------//

    void Update()
    {
        //WeaponSwitch
        if (Input.GetKeyDown("1"))
        {
            EquipedWeapon = "Pistol";
            Shotgun.gameObject.SetActive(false);
            Pistol.gameObject.SetActive(true);
            Machinegun.gameObject.SetActive(false);

            UIScript.ReloadTimeSlider.maxValue = PistolCooldownAmount;
        }

        if (Input.GetKeyDown("2") && ShotgunUnlocked == true)
        {
            EquipedWeapon = "Shotgun";
            Pistol.gameObject.SetActive(false);
            Shotgun.gameObject.SetActive(true);
            Machinegun.gameObject.SetActive(false);

            UIScript.ReloadTimeSlider.maxValue = ShotgunCooldownAmount;
        }

        if (Input.GetKeyDown("3") && MachinegunUnlocked == true)
        {
            EquipedWeapon = "Machinegun";
            Pistol.gameObject.SetActive(false);
            Shotgun.gameObject.SetActive(false);
            Machinegun.gameObject.SetActive(true);

            UIScript.ReloadTimeSlider.maxValue = MachinegunCooldownAmount;
        }

        //Weapons
        //Pistol
        if (EquipedWeapon == "Pistol")
        {
            if (Input.GetButtonDown("Fire1") && PistolCooldown <= 0.01f)
            {
                Shoot(PistolAmmo, PistolDamage, PistolRange, PistolCooldown);
                PistolAmmo = CalcAmmo(PistolAmmo);
            }
            PistolTotalAmmo = ReloadCalcTotalAmmo(PistolAmmo, PistolTotalAmmo, PistolMaxReloadAmount);
            PistolAmmo = ReloadCalcAmmoAmount(PistolAmmo, PistolMaxReloadAmount);
            PistolCooldown = CooldownCalc(PistolCooldown, PistolCooldownAmount);
            reloadTime = PistolCooldown;
            CurrentAmmo(PistolAmmo);
        }

        //Shotgun
        if (EquipedWeapon == "Shotgun")
        {
            if (Input.GetButtonDown("Fire1") && ShotgunCooldown <= 0.01f)
            {
                Shoot(ShotgunAmmo, ShotgunDamage, ShotgunRange, ShotgunCooldown);
                ShotgunAmmo = CalcAmmo(ShotgunAmmo);
            }
            ShotgunTotalAmmo = ReloadCalcTotalAmmo(ShotgunAmmo, ShotgunTotalAmmo, ShotgunMaxReloadAmount);
            ShotgunAmmo = ReloadCalcAmmoAmount(ShotgunAmmo, ShotgunMaxReloadAmount);
            ShotgunCooldown = CooldownCalc(ShotgunCooldown, ShotgunCooldownAmount);
            reloadTime = ShotgunCooldown;
            CurrentAmmo(ShotgunAmmo);
        }

        //Machinegun
        if (EquipedWeapon == "Machinegun")
        {
            if (Input.GetButton("Fire1") && MachinegunCooldown <= 0.01f)
            {
                Shoot(MachinegunAmmo, MachinegunDamage, MachinegunRange, MachinegunCooldown);
                MachinegunAmmo = CalcAmmo(MachinegunAmmo);
            }
            MachinegunTotalAmmo = ReloadCalcTotalAmmo(MachinegunAmmo, MachinegunTotalAmmo, MachinegunMaxReloadAmount);
            MachinegunAmmo = ReloadCalcAmmoAmount(MachinegunAmmo, MachinegunMaxReloadAmount);
            MachinegunCooldown = CooldownCalc(MachinegunCooldown, MachinegunCooldownAmount);
            reloadTime = MachinegunCooldown;
            CurrentAmmo(MachinegunAmmo);
        }

        //Overig Update
        ShotFlash();
        CalcReloadShootCooldown();
        CurrentReloadShootCooldown();
    }

    //----------------------------------------------------------//

    //Shoot
    void Shoot(float AmmoAmount, float DamageAmount, float RangeAmount, float Cooldown)
    {
        if (AmmoAmount >= 1 && Cooldown <= 0.01f && currentReloadShootCooldown <= 0.01f)
        {
            RaycastHit Hit;
            if (Physics.Raycast(MainCam.transform.position, MainCam.transform.forward, out Hit, RangeAmount))
            {
                //Raycast
                Enemy Enemy = Hit.transform.GetComponent<Enemy>();

                if (Enemy != null)
                {
                    Enemy.TakeDamage(DamageAmount);
                }
            }

            ShotParticles.Play();

            //Shot Flash
            ShotLight.SetActive(true);
            ShotflashCooldown = ShotflashCooldownAmount;
            ShotflashLightIsOn = true;

            //Bullet
            Rigidbody InstantiatedBullet;
            InstantiatedBullet = Instantiate(Bullet, GunPos.position, GunPos.rotation);
            InstantiatedBullet.AddForce(GunPos.forward * (Bulletspeed * Time.deltaTime));
            Destroy(InstantiatedBullet.gameObject, 1);
        }
    }

    //Shoot Effects
    void ShotFlash()
    {
        if (ShotflashCooldown >= 0f)
        {
            ShotflashCooldown -= Time.deltaTime;
        }

        if (ShotflashCooldown <= 0.01f && ShotflashLightIsOn == true)
        {
            ShotLight.SetActive(false);
            ShotflashLightIsOn = false;
        }
    }

    //Ammo Calc
    void CurrentAmmo(float currentAmmoAmount)
    {
        CurrentAmmoAmount = currentAmmoAmount;
    }

    float CalcAmmo(float AmmoAmount)
    {
        if (AmmoAmount >= 1f)
        {
            AmmoAmount -= 1f;
        }
        return AmmoAmount;
    }

    float ReloadCalcTotalAmmo(float AmmoAmount, float TotalAmmo, float MaxReload)
    {
       if (Input.GetKeyDown("r") && CurrentAmmoAmount <= 0f)
        {
            float ReloadAmount = MaxReload - AmmoAmount;
            TotalAmmo -= ReloadAmount;
        }
        return TotalAmmo;
    }
    
    float ReloadCalcAmmoAmount(float AmmoAmount, float MaxReload)
    {
        if (Input.GetKeyDown("r") && CurrentAmmoAmount <= 0f)
        {
            float ReloadAmount = MaxReload - AmmoAmount;
            AmmoAmount += ReloadAmount;
            ReloadShootCooldown();
        }
        return AmmoAmount;
    }

    //Cooldown
    float CooldownCalc(float Cooldown, float CooldownAmount)
    {
        if (Input.GetButtonDown("Fire1") && Cooldown <= 0.01f)
        {
            Cooldown += CooldownAmount;
        }

        if (Input.GetButton("Fire1") && Cooldown <= 0.01f && EquipedWeapon == "Machinegun")
        {
            Cooldown += CooldownAmount;
        }

        if (Cooldown >= 0f)
        {
            Cooldown -= Time.deltaTime;
        }
        return Cooldown;
    }

    void ReloadShootCooldown()
    {
        if (EquipedWeapon == "Pistol")
        {
            reloadShootCooldownPistol = reloadShootCooldownAmountPistol;
        }

        if (EquipedWeapon == "Shotgun")
        {
            reloadShootCooldownShotgun = reloadShootCooldownAmountShotgun;
        }

        if (EquipedWeapon == "Machinegun")
        {
            reloadShootCooldownMachinegun = reloadShootCooldownAmountMachinegun;
        }
    }

    void CalcReloadShootCooldown()
    {
        if (reloadShootCooldownPistol >= 0f)
        {
            reloadShootCooldownPistol -= Time.deltaTime;
        }

        if (reloadShootCooldownShotgun >= 0f)
        {
            reloadShootCooldownShotgun -= Time.deltaTime;
        }

        if (reloadShootCooldownMachinegun >= 0f)
        {
            reloadShootCooldownMachinegun -= Time.deltaTime;
        }
    }

    void CurrentReloadShootCooldown()
    {
        if (EquipedWeapon == "Pistol")
        {
            currentReloadShootCooldown = reloadShootCooldownPistol;
        }

        if (EquipedWeapon == "Shotgun")
        {
            currentReloadShootCooldown = reloadShootCooldownShotgun;
        }

        if (EquipedWeapon == "Machinegun")
        {
            currentReloadShootCooldown = reloadShootCooldownMachinegun;
        }
    }


}
