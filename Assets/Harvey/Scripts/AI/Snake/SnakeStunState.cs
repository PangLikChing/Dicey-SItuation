using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeStunState : SnakeBaseState
{
    [Tooltip("The stun time of the snake")]
    float stunTime;
    [Tooltip("The stun countdown timer of the snake")]
    float stunCountdown = 0.0f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Initialize
        stunTime = snake.stunTime;
        stunCountdown = 0.0f;

        // Play the Fear animation
        for (int i = 0; i < snake.animationAnimators.Length; i++)
        {
            snake.animationAnimators[i].SetTrigger("Fear");
        }

        // Throw a debug message
        Debug.Log("Stun");
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // If the snake is still recovering from stun
        if (stunCountdown < stunTime)
        {
            // Increament the countdown timer by the time passed in real time
            stunCountdown += Time.deltaTime;
        }
        else
        {
            // Transition back to Chase State
            snake.fsmAnimator.SetTrigger("Chase");
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}
