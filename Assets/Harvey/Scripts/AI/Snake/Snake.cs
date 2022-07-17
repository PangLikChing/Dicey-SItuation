using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class Snake : Enemy
{
    [Tooltip("The push direction for the knockback")]
    Vector3 pushDirection;
    [Tooltip("Recoil time of the snake (in seconds)")]
    public float recoilTime = 0;
    [Tooltip("Stun time of the snake (in seconds)")]
    public float stunTime = 0;
    [Tooltip("Time before disappearing after death for the snake (in seconds)")]
    public float timeBeforeDisappear = 0;
    [Tooltip("Score reward for defeating the snake")]
    [SerializeField] int scoreReward;
    [Tooltip("The snake cannot deal damage within this cooldown time (in seconds), cannot be longer than recoil time")]
    [SerializeField] float attackCooldown;
    [Tooltip("Attack timer for tracking attack cooldown (in seconds)")]
    float attackCountdown;
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
    public SnakeAudio snakeAudio;

    void Start()
    {
        // Initialize
        enemyScoreValue = scoreReward;

        // Do not allow attack cooldown longer than recoil time
        if (recoilTime < attackCooldown)
        {
            attackCooldown = recoilTime;
        }

        if (snakeAudio == null) snakeAudio = GetComponent<SnakeAudio>();
    }

    void Update()
    {
        // If the snake's attack is still under cooldown
        if (attackCountdown < attackCooldown)
        {
            // Increament the countdown timer by the time passed in real time
            attackCountdown += Time.deltaTime;
        }
        else
        {
            // Ignore
            return;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // If the enemy collide with the player
        if (collision.transform.GetComponent<PlayerLogic>() != null)
        {
            // If the snake is able to attack
            if (attackCountdown >= attackCooldown)
            {
                // Calculate the push direction
                pushDirection = -(transform.position - collision.transform.position).normalized;

                try
                {
                    // Push the player away from the snake
                    collision.transform.GetComponent<Rigidbody>().AddForce(uppercutForce * Vector3.up, ForceMode.Impulse);
                    collision.transform.GetComponent<Rigidbody>().AddForce(knockbackForce * pushDirection, ForceMode.Impulse);
                }
                catch
                {
                    Debug.Log($"{collision.gameObject.name} does not have a Rigidbody!");
                }

                // Reduce player's health
                GameManager.Instance.player.TakeDamage(damage);
                Debug.Log("Player got hit");

                // Stop for a while
                fsmAnimator.SetTrigger("Recoil");

                // Reset attack countdown
                attackCountdown = 0.0f;
            }
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if(alive)
        {
            fsmAnimator.SetTrigger("Stun");
            snakeAudio.PlaySound(snakeAudio.hiss);
        }
    }

    protected override void Die()
    {
        base.Die();
        
        // Transition to death state
        fsmAnimator.SetTrigger("Death");
    }
}
