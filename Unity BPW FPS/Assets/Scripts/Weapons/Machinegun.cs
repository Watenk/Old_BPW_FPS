using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machinegun : Weapons
{
    public override void Update()
    {
        //Inputs
        if (Input.GetButton("Fire1") && currentAmmo >= 1 && currentShootCooldown <= 0 && currentReloadCooldown <= 0)
        {
            Shoot();
        }

        if (Input.GetKeyDown("r") && currentReloadCooldown <= 0)
        {
            Reload();
        }

        currentShootCooldown = Cooldowns(currentShootCooldown);
        currentReloadCooldown = Cooldowns(currentReloadCooldown);
    }
}