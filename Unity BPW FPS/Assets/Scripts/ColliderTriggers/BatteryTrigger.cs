using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryTrigger : MonoBehaviour
{
    private Ability AbilityScript;

    private void Start()
    {
        AbilityScript = FindObjectOfType<Ability>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AbilityScript.AddBattery();
            Destroy(gameObject);
        }
    }
}
