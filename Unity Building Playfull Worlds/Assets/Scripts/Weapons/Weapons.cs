using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapons : MonoBehaviour
{
    public GameObject weapon;

    public int range;
    public int damage;

    //Ammo
    public int currentAmmo;
    public int maxAmmo;
    public int magazineSize;

    //bullet
    public GameObject bulletPrefab;
    public float bulletSpeed;

    //Cooldown
    public float shootCooldown;
    public float currentShootCooldown; //private
    public float reloadCooldown;
    public float currentReloadCooldown; //private

    //References
    private CharacterController player;
    private Camera playerCamera;

    private void Start()
    {
        player = FindObjectOfType<CharacterController>();
        playerCamera = player.transform.GetChild(0).GetComponent<Camera>();
    }

    public virtual void Update()
    {
        //Inputs
        if (Input.GetButtonDown("Fire1") && currentAmmo >= 1 && currentShootCooldown <= 0 && currentReloadCooldown <= 0)
        {
            print("poef");
            Shoot();
        }

        if (Input.GetKeyDown("r") && currentReloadCooldown <= 0)
        {
            Reload();
        }

        currentShootCooldown = cooldowns(currentShootCooldown);
        currentReloadCooldown = cooldowns(currentReloadCooldown);
    }

    public virtual void Shoot()
    {
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit Hit, range))
        {
            //Enemy's take Damage
        }

        //Instantiate Bullet
        GameObject bullet = Instantiate(bulletPrefab, weapon.transform.position, weapon.transform.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(weapon.transform.forward * (bulletSpeed * Time.deltaTime));
        Destroy(bullet.gameObject, 1);

        currentShootCooldown = shootCooldown;

        currentAmmo -= 1;
    }

    public virtual void Reload()
    {
        //Calc Bullets
        int bulletsToReload = magazineSize - currentAmmo;
        currentAmmo += bulletsToReload;
        maxAmmo -= bulletsToReload;

        currentReloadCooldown = reloadCooldown;
    }

    public float cooldowns(float currentCooldown)
    {
        if (currentCooldown >= 0)
        {
            currentCooldown -= Time.deltaTime;
        }
        return currentCooldown;
    }
}