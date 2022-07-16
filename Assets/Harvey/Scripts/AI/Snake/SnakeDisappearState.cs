using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeDisappearState : SnakeBaseState
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Destory the snake
        Destroy(snake.gameObject);

        // Throw a debug message
        Debug.Log($"{snake.gameObject.name} dissappear");
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}
