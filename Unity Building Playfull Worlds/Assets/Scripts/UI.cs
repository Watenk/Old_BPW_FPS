using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private CharacterControlScript Player;

    //JumpCoolDown
    public Slider JumpCooldownSlider;

    //Health
    public Slider HealthSlider;
    
    //--------------------------------------------//

    void Start()
    {
        Player = FindObjectOfType<CharacterControlScript>();
    }

    void Update()
    {
        JumpCoolDown();
        Health();
    }

    //----------------------------------------------------//

    void JumpCoolDown()
    {
        JumpCooldownSlider.value = Player.JumpCooldown;
    }

    void Health()
    {
        HealthSlider.value = Player.PlayerHealth;
    }
}
