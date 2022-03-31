using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{
    //Refrences
    public Camera MainCam;
    private PistolScript Pistol;
    private ShotgunScript Shotgun;
    private MachinegunScript Machinegun;
    private UI UIScript;

    //Weapons
    public float currentAmmo;
    public string CurrentWeapon = "Pistol";
    bool ShotgunUnlocked = true;
    bool MachinegunUnlocked = true;

    //bullet
    public Rigidbody Bullet;
    float Bulletspeed = 5000000f;
    public Transform GunPos;

    //Reload
    public float currentReloadTime;

    //ShotFlash
    public GameObject ShotLight;
    float ShotflashCooldownAmount = 0.2f;
    float ShotflashCooldown;
    bool ShotflashLightIsOn = false;
    public ParticleSystem ShotParticles;

    //Pistol
    public float PistolAmmo = 6f;
    public float PistolTotalAmmo = 120f;
    float PistolMagazine = 6f;
    float PistolDamage = 20f;
    float PistolRange = 100f;
    float PistolShootCooldownAmount = 1f;
    public float PistolShootCooldown;
    float reloadShootCooldownPistol;
    public float reloadShootCooldownAmountPistol = 2f;

    //Shotgun
    public float ShotgunAmmo = 2f;
    public float ShotgunTotalAmmo = 24f;
    float ShotgunMagazine = 2f;
    float ShotgunDamage = 100f;
    float ShotgunRange = 20f;
    float ShotgunShootCooldownAmount = 1f;
    public float ShotgunShootCooldown;
    float reloadShootCooldownShotgun;
    public float reloadShootCooldownAmountShotgun = 5f;

    //Machinegun
    public float MachinegunAmmo = 32f;
    public float MachinegunTotalAmmo = 240f;
    float MachinegunMagazine = 32f;
    float MachinegunDamage = 10f;
    float MachinegunRange = 75f;
    float MachinegunShootCooldownAmount = 0.2f;
    public float MachinegunShootCooldown;
    float reloadShootCooldownMachinegun;
    public float reloadShootCooldownAmountMachinegun = 5f;

    //Arrays
    public float[] PistolArray = new float[7];
    public float[] ShotgunArray = new float[7];
    public float[] MachinegunArray = new float[7];

    //--------------------------------------------------//

    private void Start()
    {
        //Refrences
        Pistol = FindObjectOfType<PistolScript>();
        Shotgun = FindObjectOfType<ShotgunScript>();
        Machinegun = FindObjectOfType<MachinegunScript>();
        UIScript = FindObjectOfType<UI>();
        //Disable Shotgun And Machinegun
        Shotgun.gameObject.SetActive(false);
        Machinegun.gameObject.SetActive(false);
        //Arrays
        //Pistol
        PistolArray[0] = PistolShootCooldown;
        PistolArray[1] = PistolAmmo;
        PistolArray[2] = PistolTotalAmmo;
        PistolArray[3] = PistolMagazine;
        PistolArray[4] = PistolDamage;
        PistolArray[5] = PistolRange;
        PistolArray[6] = PistolShootCooldownAmount;
        //Shotgun
        ShotgunArray[0] = ShotgunShootCooldown;
        ShotgunArray[1] = ShotgunAmmo;
        ShotgunArray[2] = ShotgunTotalAmmo;
        ShotgunArray[3] = ShotgunMagazine;
        ShotgunArray[4] = ShotgunDamage;
        ShotgunArray[5] = ShotgunRange;
        ShotgunArray[6] = ShotgunShootCooldownAmount;
        //Machinegun
        MachinegunArray[0] = MachinegunShootCooldown;
        MachinegunArray[1] = MachinegunAmmo;
        MachinegunArray[2] = MachinegunTotalAmmo;
        MachinegunArray[3] = MachinegunMagazine;
        MachinegunArray[4] = MachinegunDamage;
        MachinegunArray[5] = MachinegunRange;
        MachinegunArray[6] = MachinegunShootCooldownAmount;
    }

    //----------------------------------------------//

    void Update()
    {
        //Switch Weapon
        if (Input.GetKeyDown("1"))
        {
            CurrentWeapon = "Pistol";
            Shotgun.gameObject.SetActive(false);
            Pistol.gameObject.SetActive(true);
            Machinegun.gameObject.SetActive(false);

            UIScript.ReloadTimeSlider.maxValue = PistolArray[6];
        }

        if (Input.GetKeyDown("2") && ShotgunUnlocked == true)
        {
            CurrentWeapon = "Shotgun";
            Pistol.gameObject.SetActive(false);
            Shotgun.gameObject.SetActive(true);
            Machinegun.gameObject.SetActive(false);

            UIScript.ReloadTimeSlider.maxValue = ShotgunArray[6];
        }

        if (Input.GetKeyDown("3") && MachinegunUnlocked == true)
        {
            CurrentWeapon = "Machinegun";
            Pistol.gameObject.SetActive(false);
            Shotgun.gameObject.SetActive(false);
            Machinegun.gameObject.SetActive(true);

            UIScript.ReloadTimeSlider.maxValue = MachinegunArray[6];
        }

        //Weapons
        if (CurrentWeapon == "Pistol")
        {
            Weapons(PistolArray);
            currentReloadTime = PistolArray[0];
        }

        if (CurrentWeapon == "Shotgun")
        {
            Weapons(ShotgunArray);
            currentReloadTime = ShotgunArray[0];
        }

        if (CurrentWeapon == "Machinegun")
        {
            Weapons(MachinegunArray);
            currentReloadTime = MachinegunArray[0];
        }

        UpdateShotFlashTimer();
        CurrentAmmo(PistolArray, ShotgunArray, MachinegunArray);
    }

    //----------------------------------------------------------//

    void Weapons(float[] Array)
    {
        //Shoot
        if (Input.GetButtonDown("Fire1") && Array[0] <= 0.01f && Array[1] >= 1 && CurrentWeapon != "Machinegun")
        {
            Shoot(Array);
            RemoveAmmo(Array);
            AddShootCooldown(Array);
        }
        //Shoot HoldMouseButton
        if (Input.GetButton("Fire1") && Array[0] <= 0.01f && Array[1] >= 1 && CurrentWeapon == "Machinegun")
        {
            Shoot(Array);
            RemoveAmmo(Array);
            AddShootCooldown(Array);
        }

        //Reload
        if (Input.GetKeyDown("r") && Array[1] <= 0f)
        {
            ReloadAmmo(Array);
        }

        //Update Timer
        UpdateShootCooldownTimer(Array);
    }

    void Shoot(float[] Array)
    {
        RaycastHit Hit;
        if (Physics.Raycast(MainCam.transform.position, MainCam.transform.forward, out Hit, Array[5]))
        {
            //Raycast
            Enemy Enemy = Hit.transform.GetComponent<Enemy>();

            if (Enemy != null)
            {
                Enemy.TakeDamage(Array[4]);
            }
        }
        ShotParticles.Play();
        ShootFlash();

        //Bullet
        Rigidbody InstantiatedBullet;
        InstantiatedBullet = Instantiate(Bullet, GunPos.position, GunPos.rotation);
        InstantiatedBullet.AddForce(GunPos.forward * (Bulletspeed * Time.deltaTime));
        Destroy(InstantiatedBullet.gameObject, 1);
    }

    //Ammo Calc
    void RemoveAmmo(float[] Array)
    {
        Array[1] -= 1f;
    }

    void ReloadAmmo(float[] Array)
    {
        Array[1] = Array[3];
        Array[2] -= Array[3];
    }

    //Cooldown
    void AddShootCooldown(float[] Array)
    {
        Array[0] += Array[6];
    }

    void UpdateShootCooldownTimer(float[] Array)
    {
        if (Array[0] >= 0f)
        {
            Array[0] -= Time.deltaTime;
        }
    }

    void CurrentAmmo(float[] PistolArray, float[] ShotgunArray, float[] MachinegunArray)
    {
        if (CurrentWeapon == "Pistol")
        {
            currentAmmo = PistolArray[1];
        }
        if (CurrentWeapon == "Shotgun")
        {
            currentAmmo = ShotgunArray[1];
        }
        if (CurrentWeapon == "Machinegun")
        {
            currentAmmo = MachinegunArray[1];
        }
    }

    //Shoot Effects
    void ShootFlash()
    {
        ShotLight.SetActive(true);
        ShotflashCooldown = ShotflashCooldownAmount;
        ShotflashLightIsOn = true;
    }

    void UpdateShotFlashTimer()
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
}
