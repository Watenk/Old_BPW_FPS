using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossFightManager : MonoBehaviour
{
    public bool stage1 = false;
    public bool stage2 = false;
    public bool stage3 = false;
    public bool speedAdjusted = false;

    public GameObject zombie;

    private Boss boss;
    private GameObject bossObject;
    private BossFightTrigger trigger;
    private NavMeshAgent agent;

    private void Start()
    {
        boss = FindObjectOfType<Boss>();
        bossObject = FindObjectOfType<FindBoss>().gameObject;
        trigger = FindObjectOfType<BossFightTrigger>();
        agent = boss.gameObject.GetComponent<NavMeshAgent>();

        agent.speed = 0;
    }

    private void Update()
    {
        if (trigger.startBossFight == true && speedAdjusted == false)
        {
            agent.speed = 4;
            speedAdjusted = true;
        }

        if (boss.health <= 1500 && stage1 == false)
        {
            for (int i = 0; i < 10; i++)
            {
                Instantiate(zombie, boss.transform.position, Quaternion.identity);
            }
            stage1 = true;
        }

        if (boss.health <= 750 && stage2 == false)
        {
            for (int i = 0; i < 20; i++)
            {
                Instantiate(zombie, boss.transform.position, Quaternion.identity);
            }
            stage2 = true;
        }

        if (boss.health <= 250 && stage3 == false)
        {
            for (int i = 0; i < 30; i++)
            {
                Instantiate(zombie, boss.transform.position, Quaternion.identity);
            }
            stage3 = true;
        }
    }
}