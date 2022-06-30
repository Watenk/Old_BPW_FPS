using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int killsYouNeedToGet;
    public int kills;
    public GameObject bossWall;

    public bool bossFight = false;

    private void Update()
    {
        if (kills == killsYouNeedToGet)
        {
            bossFight = true;
            bossWall.gameObject.SetActive(false);
        }
    }
}