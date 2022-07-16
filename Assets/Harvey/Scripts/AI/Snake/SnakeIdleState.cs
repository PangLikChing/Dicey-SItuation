using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SnakeIdleState : SnakeBaseState
{
    [Tooltip("The nav mesh agent of the snake")]
    NavMeshAgent navMeshAgent;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Initialize

        Debug.Log("Snake spawnned");

        // temp, see if there are anything need to be done before finishing idle
        snake.fsmAnimator.SetTrigger("Chase");
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}
