using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyZombieBoss : MonoBehaviour
{
    private lvl02 WinScript;

    //Agent
    public float ViewDistance = 100f;
    private NavMeshAgent MeshAgent;
    private CharacterControlScript Player;
    private Shop ShopScript;

    //Damage And HP
    public float Health = 50f;
    public float DamageAmount = 20f;
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
        WinScript = FindObjectOfType<lvl02>();
        Player = FindObjectOfType<CharacterControlScript>();
        ShopScript = FindObjectOfType<Shop>();
        MeshAgent.speed = Random.Range(5, 12);
        float FifthyFifthy = Random.Range(1, 10);
        if (MeshAgent.speed >= 8 && FifthyFifthy <= 8f)
        {
            MeshAgent.speed = Random.Range(4, 5);
        }
        MeshAgent.acceleration = Random.Range(6, 10);
        MeshAgent.angularSpeed = Random.Range(110, 130);
    }

    private void Update()
    {
        DoDamage();
        Attack();
    }

    //---------------------------------------------//

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
        if (Vector3.Distance(transform.position, Player.transform.position) < 4f && DamageTimeout <= 0f)
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
