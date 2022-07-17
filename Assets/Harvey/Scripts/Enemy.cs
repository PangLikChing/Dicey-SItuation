using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent), typeof(Rigidbody))]
public class Enemy : CharacterBase
{
    [Tooltip("The nav mesh agent of the enemy")]
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [Tooltip("The player's transform")]
    [HideInInspector] public Transform player;
    [Tooltip("Score reward for defeating the enemy")]
    [HideInInspector] public int enemyScoreValue = 1;
    [Tooltip("The event raised when the enemy first spawn")]
    public UnityEvent<Enemy> enemySpawn;
    [Tooltip("The event raised when the enemy dies")]
    public UnityEvent<Enemy> enemyDeath;

    void Awake()
    {
        // Initialize
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameManager.Instance.player.transform;
        health = maxHealth;

        // Tell game manager that an enemy is spawnned
        enemySpawn.Invoke(this);
    }

    public override void TakeDamage(int damage)
    {
        // Take damage
        health -= damage;

        // If health is less than or equal to 0
        if (health <= 0)
        {
            // Die
            Die();
        }
    }

    protected override void Die()
    {
        // Throw a debug message
        Debug.Log($" base {gameObject.name} is dead!");

        // Raise the death event
        enemyDeath.Invoke(this);
    }
}
