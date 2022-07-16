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
    [Tooltip("Damage of the snake")]
    [SerializeField] int damage = 1;
    [Tooltip("Knockback force of the snake")]
    [SerializeField] int knockbackForce = 1;
    [Tooltip("Uppercut force of the snake")]
    [SerializeField] int uppercutForce = 1;
    [Tooltip("Animators that control animation of the snake")]
    public Animator[] animationAnimators;
    [Tooltip("The fsm of the snake")]
    [HideInInspector] public Animator fsmAnimator;
    Vector3 pushDirection;

    private void OnTriggerStay(Collider other)
    {
        // If the enemy collide with the player
        if (other.transform.GetComponent<PlayerLogic>() != null)
        {
            // Calculate the push direction
            pushDirection = -(transform.position - other.transform.position).normalized;

            try
            {
                // Push the player away from the snake
                other.transform.GetComponent<Rigidbody>().AddForce(uppercutForce * Vector3.up);
                other.transform.GetComponent<Rigidbody>().AddForce(knockbackForce * pushDirection);
            }
            catch
            {
                Debug.Log($"{other.gameObject.name} does not have a Rigidbody!");
            }

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
