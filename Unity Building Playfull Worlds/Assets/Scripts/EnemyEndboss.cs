using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyEndboss : MonoBehaviour
{
    private UI UIScript;

    public GameObject EnemyAI;

    //FSM
    public enum State { Patrol, Attack }
    public State state;

    //Agent
    public float ViewDistance = 100f;
    private NavMeshAgent MeshAgent;
    private CharacterControlScript Player;
    private Shop ShopScript;

    //Damage And HP
    public float Health = 3000f;
    public float DamageAmount = 50f;
    private float DamageTimeout;

    //Death
    public ParticleSystem DeathParticle;

    //Summon
    bool Wave1 = false;
    float Wave1Amount = 10f;
    bool Wave2 = false;
    float Wave2Amount = 20f;

    //-----------------------------------------//

    private void Awake()
    {
        MeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        UIScript = FindObjectOfType<UI>();
        Player = FindObjectOfType<CharacterControlScript>();
        ShopScript = FindObjectOfType<Shop>();
        MeshAgent.speed = 3f;
        MeshAgent.acceleration = 6f;
        MeshAgent.angularSpeed = 100f;
    }

    private void Update()
    {
        CheckState();
        DoDamage();
    }

    //---------------------------------------------//

    //FSM
    private void CheckState()
    {
        switch (state)
        {
            case State.Patrol:
                Patrol();
                break;

            case State.Attack:
                Attack();
                break;
        }

        //SwitchState
        if (Vector3.Distance(transform.position, Player.transform.position) < ViewDistance)
        {
            state = State.Attack;
            MeshAgent.stoppingDistance = 3f;
        }

        if (Vector3.Distance(transform.position, Player.transform.position) > ViewDistance)
        {
            state = State.Patrol;
            MeshAgent.stoppingDistance = 0.05f;
        }

        if (Health <= 1500 && Wave1 == false)
        {
            for(int i =0; i < Wave1Amount; i++)
            {
                Instantiate(EnemyAI, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            }
            Wave1 = true;
        }

        if (Health <= 500 && Wave2 == false)
        {
            for (int i = 0; i < Wave2Amount; i++)
            {
                Instantiate(EnemyAI, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            }
            Wave2 = true;
        }

    }
    void Patrol()
    {

    }

    void Attack()
    {
        MeshAgent.SetDestination(Player.transform.position);
    }

    //Damage and HP
    public void TakeDamage(float DamageAmount)
    {
        //EnemyDamage
        Health -= DamageAmount;

        if (Health <= 0f)
        {
            Die();
        }
    }

    void DoDamage()
    {
        //PlayerDamage
        if (Vector3.Distance(transform.position, Player.transform.position) < 4f && state == State.Attack && DamageTimeout <= 0f)
        {
            Player.PlayerHealth = Player.PlayerHealth - DamageAmount;
            DamageTimeout = 5f;
        }

        //DamageTimeout
        if (DamageTimeout >= 0f)
        {
            DamageTimeout -= Time.deltaTime;
        }
    }

    void Die()
    {
        ShopScript.Coins += 10000f;
        UIScript.Win.gameObject.SetActive(true);
        Instantiate(DeathParticle, new Vector3(transform.position.x, transform.position.y + 10f, transform.position.z), Quaternion.Euler(90, 0, 0));
        Destroy(gameObject);
    }
}

