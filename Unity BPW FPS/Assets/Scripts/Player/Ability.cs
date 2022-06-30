using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    private CharacterControlScript CharacterControlScript;

    public Light flashLight;

    //SuperFlash
    public float superFlashAmount;
    float superFlashCooldown;
    float superFlashCooldownAmount = 30f;

    //DubbleJump
    public float dubbleJumpAmount;
    float dubbleJumpCooldown;
    float dubbleJumpCooldownAmount = 3;

    //-------------------------------------------------//

    private void Start()
    {
        CharacterControlScript = GetComponent<CharacterControlScript>();
    }

    void Update()
    {
        //SuperFlash
        if (Input.GetKeyDown("f1") && superFlashAmount >= 1f && superFlashCooldown <= 0f)
        {
            superFlashAmount -= 1f;
            superFlashCooldown = superFlashCooldownAmount;
            //Flashlight tweaks
            flashLight.intensity = 5f;
            flashLight.range = 100f;
            flashLight.spotAngle = 90f;
        }

        if (superFlashCooldown >= 0)
        {
            superFlashCooldown -= Time.deltaTime;
        }

        if (superFlashCooldown <= 0f)
        {
            flashLight.intensity = 2.5f;
            flashLight.range = 75f;
            flashLight.spotAngle = 65f;
        }

        //Dubblejump
        if (Input.GetKeyDown("e") && dubbleJumpAmount >= 1f && dubbleJumpCooldown <= 0f && CharacterControlScript.IsOnGround == false)
        {
            CharacterControlScript.Velocity.y = CharacterControlScript.JumpHeight;
            dubbleJumpCooldown = dubbleJumpCooldownAmount;
            dubbleJumpAmount -= 1f;
        }

        if (dubbleJumpCooldown >= 0f)
        {
            dubbleJumpCooldown -= Time.deltaTime;
        }
    }

    public void AddBattery()
    {
        superFlashAmount += 1f;
    }

    public void AddArrow()
    {
        dubbleJumpAmount += 3f;
    }
}
