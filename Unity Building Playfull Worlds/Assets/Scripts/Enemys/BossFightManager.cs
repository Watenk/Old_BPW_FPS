using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightManager : MonoBehaviour
{
    public bool stage1 = false;
    public bool stage2 = false;

    public GameObject zombie;

    private Boss boss;
    private GameObject bossObject;
    private BossFightTrigger trigger;

    private void Start()
    {
        boss = FindObjectOfType<Boss>();
        bossObject = FindObjectOfType<FindBoss>().gameObject;
        trigger = FindObjectOfType<BossFightTrigger>();

        bossObject.SetActive(false);
    }

    private void Update()
    {
        if (trigger.startBossFight == true)
        {
            bossObject.SetActive(true);
        }

        if (boss.health <= 250 && stage1 == false)
        {
            for (int i = 0; i < 10; i++)
            {
                Instantiate(zombie, boss.transform.position, Quaternion.identity);
            }
            stage1 = true;
        }

        if (boss.health <= 100 && stage2 == false)
        {
            for (int i = 0; i < 20; i++)
            {
                Instantiate(zombie, boss.transform.position, Quaternion.identity);
            }
            stage2 = true;
        }
    }
}