using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public Light flashLight;

    //SuperFlash
    public float superFlashCount;
    float superFlashCooldown;
    float superFlashCooldownAmount = 30f;

    //-------------------------------------------------//

    void Update()
    {
        //SuperFlash
        if (Input.GetKeyDown("f1") && superFlashCount >= 1f && superFlashCooldown <= 0f)
        {
            superFlashCount -= 1f;
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
        if (Input.GetKeyDown("e"))
        {

        }
    }

    public void AddBattery()
    {
        superFlashCount += 1f;
    }
}
