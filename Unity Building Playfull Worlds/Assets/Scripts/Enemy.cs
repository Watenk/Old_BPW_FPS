using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //FSM
    public enum State { Idle, Patrol, Attack }
    public State state;

    //Agent
    public float ViewDistance = 30f;
    public Transform[] PatrolPoints;
    private int PatrolPoint;
    private NavMeshAgent MeshAgent;
    private CharacterControlScript Player;

    //Damage And HP
    public float Health = 30f;
    public float DamageAmount = 10f;
    private float DamageTimeout;

    //-----------------------------------------//

    private void Awake()
    {
        MeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        Player = FindObjectOfType<CharacterControlScript>();
        MeshAgent.speed = Random.Range(3, 6);
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
            case State.Idle: 
                Idle(); 
                break;

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

    void Idle()
    {
        
    }

    void Patrol()
    {
        //Cycle naar de volgende bestemming als bestemming is bereikt
        if (MeshAgent.remainingDistance <= 0.5 && !MeshAgent.pathPending)
        {
            PatrolPoint = Random.Range(0, 4);
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
        Destroy(gameObject);
    }
}
