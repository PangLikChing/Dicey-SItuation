using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class Snake : Enemy
{
    [Tooltip("Recoil time of the snake")]
    public float recoilTime = 0;
    [Tooltip("Stun time of the snake")]
    public float stunTime = 0;
    [Tooltip("Time before disappearing after death for the snake")]
    public float timeBeforeDisappear = 0;
    [Tooltip("The fsm of the snake")]
    [HideInInspector] public Animator fsmAnimator;
    [Tooltip("Damage of the snake")]
    [SerializeField] int damage = 1;

    private void OnTriggerStay(Collider other)
    {
        // If the enemy collide with the player
        if (other.transform.GetComponent<PlayerLogic>() != null)
        {
            // Push the player away from the snake

            // Reduce player's health
            GameManager.Instance.player.TakeDamage(damage);
            Debug.Log("Player got hit");

            // Stop for a while
            fsmAnimator.SetTrigger("Recoil");
        }
    }

    public override void TakeDamage(int damage)
    {
        // Take damage
        health -= damage;

        // Stun the snake
        fsmAnimator.SetTrigger("Stun");

        // If health is less than or equal to 0
        if (health <= 0)
        {
            // Die
            Die();
        }
    }

    protected override void Die()
    {
        // Transition to death state
        fsmAnimator.SetTrigger("Death");
    }
}
