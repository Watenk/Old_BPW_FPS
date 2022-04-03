using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private Shooting ShootingScript;
    private CharacterControlScript CharacterControlScript;

    public float Coins = 0f;
    public bool ShopActive = false;

    private void Start()
    {
        ShootingScript = FindObjectOfType<Shooting>();
        CharacterControlScript = FindObjectOfType<CharacterControlScript>();
    }

    private void Update()
    {
        if (ShopActive == true)
        {
           if (Input.GetKeyDown("2") && ShootingScript.ShotgunUnlocked == false)
            {
                if (Coins >= 100f)
                {
                    Coins -= 100f;
                    ShootingScript.ShotgunUnlocked = true;
                }
            }

           if (Input.GetKeyDown("3") && ShootingScript.MachinegunUnlocked == false)
            {
                if (Coins >= 500f)
                {
                    Coins -= 500f;
                    ShootingScript.MachinegunUnlocked = true;
                }
            }

           if (Input.GetKeyDown("h"))
            {
                if (Coins >= 50f)
                {
                    Coins -= 50f;
                    CharacterControlScript.PlayerHealth = 100f;
                }
            }

           if (Input.GetKeyDown("g"))
            {
                if (Coins >= 70f)
                {
                    Coins -= 70f;
                    ShootingScript.ReloadTotalAmmo();
                }
            }
        }
    }
}
