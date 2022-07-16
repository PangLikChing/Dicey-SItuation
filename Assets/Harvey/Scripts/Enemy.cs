using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent), typeof(CapsuleCollider))]
public class Enemy : MonoBehaviour
{
    [Tooltip("The maximum health of the enemy")]
    [SerializeField] int maxHealth = 0;
    [Tooltip("The current health of the enemy")]
    int currentHealth = 0;
    [Tooltip("The nav mesh agent of the enemy")]
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [Tooltip("The player's transform")]
    [HideInInspector] public Transform player;
    [Tooltip("The event raised when the enemy first spawn")]
    public UnityEvent<Enemy> enemySpawn;
    [Tooltip("The event raised when the enemy dies")]
    public UnityEvent<Enemy> enemyDeath;

    void Awake()
    {
        // Initialize
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameManager.Instance.player.transform;
        currentHealth = maxHealth;

        // Tell game manager that an enemy is spawnned
        enemySpawn.Invoke(this);
    }

    public virtual void TakeDamage(int damage)
    {
        // Take damage
        currentHealth -= damage;

        // If health is less than or equal to 0
        if (currentHealth >= 0)
        {
            // Die
            Death();
        }
    }

    private void Death()
    {
        // Throw a debug message
        Debug.Log($"{gameObject.name} is dead!");

        // Raise the death event
        enemyDeath.Invoke(this);
    }
}
