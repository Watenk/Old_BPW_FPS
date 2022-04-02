using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Win WinScript;

    //FSM
    public enum State {Patrol, Attack }
    public State state;

    //Agent
    public float ViewDistance = 50f;
    public Transform[] PatrolPoints;
    private int PatrolPoint;
    private NavMeshAgent MeshAgent;
    private CharacterControlScript Player;
    private Shop ShopScript;

    //Damage And HP
    public float Health = 30f;
    public float DamageAmount = 10f;
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
        WinScript = FindObjectOfType<Win>();
        Player = FindObjectOfType<CharacterControlScript>();
        ShopScript = FindObjectOfType<Shop>(); 
        MeshAgent.speed = Random.Range(3, 12);
        float FifthyFifthy = Random.Range(1, 10);
        if (MeshAgent.speed >=8 && FifthyFifthy <= 8f)
        {
            MeshAgent.speed = Random.Range(3, 5);
        }
        MeshAgent.acceleration = Random.Range(6, 10);
        MeshAgent.angularSpeed = Random.Range(110, 130);
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
        //Cycle naar de volgende bestemming als bestemming is bereikt
        if (MeshAgent.remainingDistance <= 0.5 && !MeshAgent.pathPending)
        {
            PatrolPoint = Random.Range(0, 8);
        }

        //Wat is bestemming
        MeshAgent.destination = PatrolPoints[PatrolPoint].position;
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
            DamageTimeout = 3f;
        }

        //DamageTimeout
        if (DamageTimeout >= 0f)
        {
            DamageTimeout -= Time.deltaTime;
        }

    }

    void Die()
    {
        ShopScript.Coins += 10f;
        WinScript.kills += 1f;
        Instantiate(DeathParticle, new Vector3(transform.position.x, transform.position.y + 4f, transform.position.z), Quaternion.Euler(90, 0, 0));
        Destroy(gameObject);
    }
}
