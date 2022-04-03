using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvl02 : MonoBehaviour
{
    private UI UIScript;

    public float kills;
    public GameObject BossWall;
    float timer = 5f;

    private void Start()
    {
        UIScript = FindObjectOfType<UI>();
    }

    private void Update()
    {
        if (kills >= 100f && timer >= 0f)
        {
            BossWall.gameObject.SetActive(false);
            UIScript.aAreaHasOpened.gameObject.SetActive(true);
            Timer();
        }

        if (timer <= 0f)
        {
            UIScript.aAreaHasOpened.gameObject.SetActive(false);
        }
    }

    void Timer()
    {
        timer -= Time.deltaTime;
    }
}
