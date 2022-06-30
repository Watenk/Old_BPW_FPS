using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    //Abilities
    public Text superFlash;
    public Text dubbleJump;

    //Player
    public Slider healthSlider;
    public Text killCounter;

    //Weapons
    public Text currentAmmo;
    public Text totalAmmo;
    public Slider shootCooldownSlider;
    public Slider reloadingSlider;
    public Text needToReloadText;

    //Shop
    public Text coinsText;
    public Text shopText;

    //Boss
    public Text areaText;
    public Text aAreaHasOpened;
    public Slider bossHP;

    //Win/Lose
    public Text youLost;
    public Text pressEtoStartAgain;
    public Text win;

    //References
    private Shooting shooting;
    private CharacterControlScript player;
    private Ability ability;
    private GameManager gameManager;
    private Shop shop;
    private AreaTrigger area;
    private Enemy boss;
    private BossFightTrigger trigger;

    //--------------------------------------------//

    void Start()
    {
        shooting = FindObjectOfType<Shooting>();
        player = FindObjectOfType<CharacterControlScript>();
        ability = FindObjectOfType<Ability>();
        gameManager = FindObjectOfType<GameManager>();
        shop = FindObjectOfType<Shop>();
        area = FindObjectOfType<AreaTrigger>();
        boss = FindObjectOfType<FindBoss>().GetComponent<Enemy>();
        trigger = FindObjectOfType<BossFightTrigger>();
    }

    void Update()
    {
        Weapons();
        Player();
        Boss();
        TriggerColliders();
        WinLose();
    }

    //----------------------------------------------------//

    void Weapons()
    {
        //Reload
        shootCooldownSlider.value = shooting.currentShootCooldown;
        reloadingSlider.value = shooting.currentReloadCooldown;

        if (shooting.currentAmmo <= 0)
        {
            needToReloadText.gameObject.SetActive(true);
        }
        else
        {
            needToReloadText.gameObject.SetActive(false);
        }

        if (shooting.currentReloadCooldown >= 0.01f)
        {
            reloadingSlider.gameObject.SetActive(true);
            needToReloadText.text = "Reloading...";
        }
        else
        {
            reloadingSlider.gameObject.SetActive(false);
            needToReloadText.text = "Reload!";
        }

        //Ammo
        currentAmmo.text = shooting.currentAmmo.ToString() + "/" + shooting.currentMagazineSize;
        totalAmmo.text = shooting.currentMaxAmmo.ToString();
    }

    private void Player()
    {
        healthSlider.value = player.playerHealth;
        coinsText.text = shop.Coins.ToString();
        killCounter.text = gameManager.kills.ToString() + " / 100";
        if (gameManager.kills == gameManager.killsYouNeedToGet && trigger.startBossFight == false)
        {
            aAreaHasOpened.gameObject.SetActive(true);
        }
        else
        {
            aAreaHasOpened.gameObject.SetActive(false);
        }

        superFlash.text = ability.superFlashAmount.ToString();
        dubbleJump.text = ability.dubbleJumpAmount.ToString();
    }

    void TriggerColliders()
    {
        if (area.playerIsInArea && gameManager.bossFight != true)
        {
            areaText.gameObject.SetActive(true);
        }
        else
        {
            areaText.gameObject.SetActive(false);
        }
    }

    void Boss()
    {
        if (trigger.startBossFight == true)
        {
            bossHP.value = boss.health;
        }
    }

    void WinLose()
    {
        if (gameManager.won == true)
        {
            win.gameObject.SetActive(true);
        }
    }
}
