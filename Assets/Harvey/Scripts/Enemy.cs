using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent), typeof(CapsuleCollider))]
public class Enemy : MonoBehaviour
{
    [Tooltip("The nav mesh agent of the enemy")]
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [Tooltip("The player's transform")]
    [HideInInspector] public Transform player;

    public UnityEvent<Enemy> enemySpawn;

    void Awake()
    {
        // Initialize
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameManager.Instance.player.transform;

        // Tell game manager that an enemy is spawnned
        enemySpawn.Invoke(this);
    }

    void FixedUpdate()
    {
        //// Move towards the player
        //navMeshAgent.SetDestination(player.position);
    }
}
