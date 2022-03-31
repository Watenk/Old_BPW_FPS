using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public Light flashLight;

    void Start()
    {
        
    }

    void Update()
    {
        //SuperFlash
        if (Input.GetKeyDown("f1"))
        {
            flashLight.intensity = 5f;
            flashLight.range = 100f;
            flashLight.spotAngle = 90f;
        }
    }
}
