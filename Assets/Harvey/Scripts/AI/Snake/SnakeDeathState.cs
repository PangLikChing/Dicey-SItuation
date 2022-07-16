using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SnakeDeathState : SnakeBaseState
{
    [Tooltip("The death time of the snake")]
    float deathTime;
    [Tooltip("The death countdown timer of the snake")]
    float deathCountdown = 0.0f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Initialize
        deathTime = snake.timeBeforeDisappear;
        deathCountdown = 0.0f;

        // Disable the snake's colider
        snake.GetComponent<CapsuleCollider>().enabled = false;

        // Play the Death animation
        for (int i = 0; i < snake.animationAnimators.Length; i++)
        {
            snake.animationAnimators[i].SetTrigger("Death");
        }

        Debug.Log($"{snake.gameObject.name} dead");
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // If the snake is still dying
        if (deathCountdown < deathTime)
        {
            // Increament the countdown timer by the time passed in real time
            deathCountdown += Time.deltaTime;
        }
        else
        {
            // Transition back to Disappear State
            snake.fsmAnimator.SetTrigger("Disappear");
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}
