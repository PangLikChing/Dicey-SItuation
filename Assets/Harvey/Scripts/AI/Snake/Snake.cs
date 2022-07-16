using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : Enemy
{
    [Tooltip("The recoil time of the snake")]
    public float recoilTime = 0;
    [Tooltip("The fsm of the snake")]
    [HideInInspector] public Animator fsmAnimator;

    void Start()
    {
        // Initialize
        fsmAnimator = transform.GetChild(transform.childCount - 1).GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        // If the enemy collide with the player
        if (other.transform.GetComponent<PlayerLogic>() != null)
        {
            // Push the player away from the snake

            // Reduce player's health
            Debug.Log("Player got hit");

            // Stop for a while
            fsmAnimator.SetTrigger("Recoil");
        }
    }

    public override void TakeDamage(int damage)
    {
        // Take damage
        base.TakeDamage(damage);

        // Die
        Death();
    }

    private void Death()
    {
        // Transition to death state
        fsmAnimator.SetTrigger("Death");

        // Throw a debug message
        Debug.Log($"{gameObject.name} in death state.");
    }
}
