using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    //Abilities
    public Text superFlash;
    public Text dubbleJump;

    //Health
    public Slider healthSlider;

    //Ammo
    public Text currentAmmo;
    public Text totalAmmo;

    //reload
    public Slider reloadingSlider;
    public Slider reloadTimeSlider;
    public Text needToReloadText;

    //Shop
    public Text coinsText;
    public Text shopText;

    //Kills
    public Text killCounter;

    //Boss
    public Text aAreaHasOpened;
    public Text areaWall;
    public Slider bossHP;

    //Lose
    public Text youLost;
    public Text pressEtoStartAgain;

    //Win
    public Text win;

    //References
    private Shooting shooting;
    private CharacterControlScript player;
    private Ability ability;
    private GameManager gameManager;
    private Shop shop;
    private AreaTrigger area;

    //--------------------------------------------//

    void Start()
    {
        shooting = FindObjectOfType<Shooting>();
        player = FindObjectOfType<CharacterControlScript>();
        ability = FindObjectOfType<Ability>();
        gameManager = FindObjectOfType<GameManager>();
        shop = FindObjectOfType<Shop>();
        area = FindObjectOfType<AreaTrigger>();
    }

    void Update()
    {
        Weapons();
        Player();
        Boss();
    }

    //----------------------------------------------------//

    void Weapons()
    {
        //Reload
        reloadTimeSlider.value = shooting.currentReloadCooldown;

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
        currentAmmo.text = shooting.currentAmmo.ToString() + "/6";
        totalAmmo.text = shooting.currentMaxAmmo.ToString();

    }

    private void Player()
    {
        healthSlider.value = player.playerHealth;
        coinsText.text = shop.Coins.ToString();
        killCounter.text = gameManager.kills.ToString() + " / 100";
        if (gameManager.kills == gameManager.killsYouNeedToGet)
        {
            aAreaHasOpened.gameObject.SetActive(true);
        }

        superFlash.text = ability.superFlashAmount.ToString();
        dubbleJump.text = ability.dubbleJumpAmount.ToString();
    }

    void TriggerColliders()
    {
        if (area.playerIsInArea)
        {
            areaWall.gameObject.SetActive(true);
        }
        else
        {
            areaWall.gameObject.SetActive(false);
        }
    }

    void Boss()
    {
        //BossHP.value = EnemyEndBossScript.Health;
    }
}
