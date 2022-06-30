using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    public enum State { Patrol, Attack }
    public State state;

    public float health;
    public int damage;
    public int viewDistance;
    public int coins;

    //PatrolPoints
    private GameObject patrolPointsGameObject;
    public Transform[] patrolPoints;
    private int currentPatrolPoint;

    public float attackCooldown;
    private float currentAttackCooldown;

    public ParticleSystem deathParticle;

    //References
    private NavMeshAgent agent;
    private CharacterControlScript player;
    private Shop shop;
    private GameManager gameManager;

    public virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<CharacterControlScript>();
        shop = FindObjectOfType<Shop>();
        gameManager = FindObjectOfType<GameManager>();

        //Find Patrol Points
        patrolPointsGameObject = FindObjectOfType<FindPatrolPoints>().gameObject;
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            patrolPoints[i] = patrolPointsGameObject.transform.GetChild(i).transform;
        }

        //Give agents random speed, angularSpeed and acceleration
        agent.speed = Random.Range(3, 12);
        float randomNumber = Random.Range(1, 10);
        if (randomNumber <= 8)
        {
            agent.speed = Random.Range(3, 5);
        }
        agent.acceleration = Random.Range(6, 10);
        agent.angularSpeed = Random.Range(110, 130);
    }

    public virtual void Update()
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
        if (Vector3.Distance(transform.position, player.transform.position) < viewDistance)
        {
            state = State.Attack;
            agent.stoppingDistance = 3f;
        }

        if (Vector3.Distance(transform.position, player.transform.position) > viewDistance)
        {
            state = State.Patrol;
            agent.stoppingDistance = 0.05f;
        }

        DoDamage();

        //Cooldowns
        currentAttackCooldown = Cooldowns(currentAttackCooldown);
    }

    public virtual void Patrol()
    {
        //Cycle naar de volgende bestemming als bestemming is bereikt
        if (agent.remainingDistance <= 0.5 && !agent.pathPending)
        {
            currentPatrolPoint = Random.Range(0, 20);
        }
        agent.destination = patrolPoints[currentPatrolPoint].position;
    }

    public virtual void Attack()
    {
        agent.SetDestination(player.transform.position);
    }

    public virtual void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0f)
        {
            Die();
        }
    }

    public virtual void DoDamage()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 4f && state == State.Attack && currentAttackCooldown <= 0f)
        {
            print("damage");
            player.playerHealth -= damage;
            currentAttackCooldown = attackCooldown;
        }
    }

    public virtual void Die()
    {
        shop.Coins += coins;
        gameManager.kills += 1; 
        Instantiate(deathParticle, new Vector3(transform.position.x, transform.position.y + 4f, transform.position.z), Quaternion.Euler(90, 0, 0));
        Destroy(gameObject);
    }

    public float Cooldowns(float currentCooldown)
    {
        if (currentCooldown >= 0)
        {
            currentCooldown -= Time.deltaTime;
        }
        return currentCooldown;
    }
}