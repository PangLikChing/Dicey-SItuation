using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SnakeRecoilState : SnakeBaseState
{
    [Tooltip("The recoil time of the snake")]
    float recoilTime;
    [Tooltip("The recoil countdown timer of the snake")]
    float recoilCountdown = 0.0f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Initialize
        recoilTime = snake.recoilTime;
        recoilCountdown = 0.0f;

        // Play the Munch animation
        for (int i = 0; i < snake.animationAnimators.Length; i++)
        {
            snake.animationAnimators[i].SetTrigger("Munch");
        }

        // Throw a debug message
        Debug.Log("Recoil");
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // If the snake is still recovering from recoil
        if (recoilCountdown < recoilTime)
        {
            // Increament the countdown timer by the time passed in real time
            recoilCountdown += Time.deltaTime;
        }
        else
        {
            // Transition back to Chase State
            snake.fsmAnimator.SetTrigger("Chase");
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //test
        snake.TakeDamage(10);
        //test
    }
}
