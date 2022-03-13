using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Camera MainCam;
    private PistolScript Pistol;
    private ShotgunScript Shotgun;
    private MachinegunScript Machinegun;
    private UI UIScript;

    public float reloadTime = 0f;
    bool outOfAmmoBool = false;

    //bullet
    public ParticleSystem shootParticle;
    public Rigidbody Bullet;
    float Bulletspeed = 5000000f;
    public Transform GunPos;

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

    //Machinegun
    public float MachinegunAmmo = 32f;
    public float MachinegunTotalAmmo = 240f;
    float MachinegunMaxReloadAmount = 32f;
    float MachinegunDamage = 10f;
    float MachinegunRange = 75f;
    float MachinegunCooldownAmount = 0.2f;
    public float MachinegunCooldown = 0f;


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

        if (Input.GetKeyDown("2"))
        {
            EquipedWeapon = "Shotgun";
            Pistol.gameObject.SetActive(false);
            Shotgun.gameObject.SetActive(true);
            Machinegun.gameObject.SetActive(false);

            UIScript.ReloadTimeSlider.maxValue = ShotgunCooldownAmount;
        }

        if (Input.GetKeyDown("3"))
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
            if (Input.GetButtonDown("Fire1") && PistolCooldown <= 0.1f)
            {
                Shoot(PistolAmmo, PistolDamage, PistolRange, PistolCooldown);
                PistolAmmo = CalcAmmo(PistolAmmo);
            }
            PistolTotalAmmo = ReloadCalc(PistolAmmo, PistolTotalAmmo, PistolMaxReloadAmount);
            PistolAmmo = ReloadCalc2(PistolAmmo, PistolMaxReloadAmount);
            PistolCooldown = CooldownCalc(PistolCooldown, PistolCooldownAmount);
            reloadTime = PistolCooldown;
            CheckOutOfAmmo(PistolAmmo);
        }

        //Shotgun
        if (EquipedWeapon == "Shotgun")
        {
            if (Input.GetButtonDown("Fire1") && ShotgunCooldown <= 0.1f)
            {
                Shoot(ShotgunAmmo, ShotgunDamage, ShotgunRange, ShotgunCooldown);
                ShotgunAmmo = CalcAmmo(ShotgunAmmo);
            }
            ShotgunTotalAmmo = ReloadCalc(ShotgunAmmo, ShotgunTotalAmmo, ShotgunMaxReloadAmount);
            ShotgunAmmo = ReloadCalc2(ShotgunAmmo, ShotgunMaxReloadAmount);
            ShotgunCooldown = CooldownCalc(ShotgunCooldown, ShotgunCooldownAmount);
            reloadTime = ShotgunCooldown;
            CheckOutOfAmmo(ShotgunAmmo);
        }

        //Machinegun
        if (EquipedWeapon == "Machinegun")
        {
            if (Input.GetButton("Fire1") && MachinegunCooldown <= 0.1f)
            {
                Shoot(MachinegunAmmo, MachinegunDamage, MachinegunRange, MachinegunCooldown);
                MachinegunAmmo = CalcAmmo(MachinegunAmmo);
            }
            MachinegunTotalAmmo = ReloadCalc(MachinegunAmmo, MachinegunTotalAmmo, MachinegunMaxReloadAmount);
            MachinegunAmmo = ReloadCalc2(MachinegunAmmo, MachinegunMaxReloadAmount);
            MachinegunCooldown = CooldownCalc(MachinegunCooldown, MachinegunCooldownAmount);
            reloadTime = MachinegunCooldown;
            CheckOutOfAmmo(MachinegunAmmo);
        }

        //Out Of Ammo
        if (outOfAmmoBool == true)
        {
            UIScript.outOfAmmo.gameObject.SetActive(true);
        }

        if (outOfAmmoBool == false)
        {
            UIScript.outOfAmmo.gameObject.SetActive(false);
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
                //Raycast
                Enemy Enemy = Hit.transform.GetComponent<Enemy>();

                if (Enemy != null)
                {
                    Enemy.TakeDamage(DamageAmount);
                }

                shootParticle.Play();

                //Bullet
                Rigidbody InstantiatedBullet;
                InstantiatedBullet = Instantiate(Bullet, GunPos.position, GunPos.rotation);
                InstantiatedBullet.AddForce(GunPos.forward * (Bulletspeed * Time.deltaTime));
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

        if (Input.GetButton("Fire1") && Cooldown <= 0.1f && EquipedWeapon == "Machinegun")
        {
            Cooldown += CooldownAmount;
        }

        if (Cooldown >= 0f)
        {
            Cooldown -= Time.deltaTime;
        }
        return Cooldown;
    }

    void CheckOutOfAmmo(float currentAmmo)
    {
        if (currentAmmo <= 0f)
        {
            outOfAmmoBool = true;
        }

        if (currentAmmo >= 0.1f)
        {
            outOfAmmoBool = false;
        }
    }
}
