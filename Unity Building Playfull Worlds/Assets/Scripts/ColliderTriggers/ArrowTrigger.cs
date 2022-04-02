using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrigger : MonoBehaviour
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
            AbilityScript.AddArrow();
            Destroy(gameObject);
        }
    }
}
