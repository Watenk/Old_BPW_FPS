using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightTrigger : MonoBehaviour
{
    private UI UIScript;
    public bool startBossFight = false;

    private void Start()
    {
        UIScript = FindObjectOfType<UI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            startBossFight = true;
            UIScript.bossHP.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UIScript.bossHP.gameObject.SetActive(false);
        }
    }
}
