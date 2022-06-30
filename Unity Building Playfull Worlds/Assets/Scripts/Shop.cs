using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private Shooting shooting;
    private CharacterControlScript player;

    public int Coins = 0;
    public bool ShopActive = false;

    private void Start()
    {
        shooting = FindObjectOfType<Shooting>();
        player = FindObjectOfType<CharacterControlScript>();
    }

    private void Update()
    {
        if (ShopActive == true)
        {
            if (Input.GetKeyDown("2") && shooting.shotgunUnlocked == false)
            {
                if (Coins >= 100)
                {
                    Coins -= 100;
                    shooting.shotgunUnlocked = true;
                }
            }

            if (Input.GetKeyDown("3") && shooting.machinegunUnlocked == false)
            {
                if (Coins >= 500)
                {
                    Coins -= 500;
                    shooting.machinegunUnlocked = true;
                }
            }

            if (Input.GetKeyDown("h"))
            {
                if (Coins >= 50)
                {
                    Coins -= 50;
                    player.playerHealth = 100;
                }
            }

            if (Input.GetKeyDown("g"))
            {
                if (Coins >= 70)
                {
                    Coins -= 70;
                    shooting.ReloadAllAmmo();
                }
            }
        }
    }
}
