using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int killsYouNeedToGet;
    public int kills;
    public GameObject bossWall;

    public bool bossFight = false;

    private void Update()
    {
        if (kills == 100)
        {
            bossFight = true;
        }
    }


    //public void Play()
    //{
    //    SceneManager.LoadScene("Level01&02");
    //}

    //public void Quit()
    //{
    //    Application.Quit();
    //}
}