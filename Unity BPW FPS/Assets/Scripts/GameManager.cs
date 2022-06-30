using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int killsYouNeedToGet;
    public int kills;
    public GameObject bossWall;
    public bool bossFight = false;
    public bool won = false;

    private Boss boss;

    private void Start()
    {
        boss = FindObjectOfType<Boss>();
    }

    private void Update()
    {
        if (kills == killsYouNeedToGet)
        {
            bossFight = true;
            bossWall.gameObject.SetActive(false);
        }
    }
}