using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyEndboss : MonoBehaviour
{
    //FSM
    public enum State { Patrol, Attack }
    public State state;

    //Agent
    public float ViewDistance = 100f;
    public Transform[] PatrolPoints;
    private int PatrolPoint;
    private NavMeshAgent MeshAgent;
    private CharacterControlScript Player;
    private Shop ShopScript;

    //Damage And HP
    public float Health = 3000f;
    public float DamageAmount = 50f;
    private float DamageTimeout;

    //Death
    public ParticleSystem DeathParticle;

    //-----------------------------------------//

    private void Awake()
    {
        MeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
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
        ShopScript.Coins += 1000f;
        Instantiate(DeathParticle, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(90, 0, 0));
        Instantiate(DeathParticle, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.Euler(90, 0, 0));
        Instantiate(DeathParticle, new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), Quaternion.Euler(90, 0, 0));
        Instantiate(DeathParticle, new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z), Quaternion.Euler(90, 0, 0));
        Instantiate(DeathParticle, new Vector3(transform.position.x, transform.position.y + 4f, transform.position.z), Quaternion.Euler(90, 0, 0));
        Destroy(gameObject);
    }
}

