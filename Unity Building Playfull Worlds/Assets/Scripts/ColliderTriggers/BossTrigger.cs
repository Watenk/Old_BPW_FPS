using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    private UI UIScript;
    private lvl02 lvl02Script;

    private void Start()
    {
        UIScript = FindObjectOfType<UI>();
        lvl02Script = FindObjectOfType<lvl02>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && lvl02Script.kills <= 99f)
        {
            UIScript.BossWall.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UIScript.BossWall.gameObject.SetActive(false);
        }
    }
}
