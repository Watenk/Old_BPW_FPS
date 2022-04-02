using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    private Shop ShopScript;
    private UI UIScript;

    private void Start()
    {
        ShopScript = FindObjectOfType<Shop>();
        UIScript = FindObjectOfType<UI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UIScript.ShopText.gameObject.SetActive(true);
            ShopScript.ShopActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UIScript.ShopText.gameObject.SetActive(false);
            ShopScript.ShopActive = false;
        }
    }
}
