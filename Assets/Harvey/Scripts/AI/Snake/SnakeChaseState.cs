using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SnakeChaseState : SnakeBaseState
{
    [Tooltip("The nav mesh agent of the snake")]
    NavMeshAgent navMeshAgent;
    [Tooltip("The player's transform")]
    Transform player;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Initialize
        player = snake.player;
        navMeshAgent = snake.navMeshAgent;

        // Play the idle animation
        // The animation should be a looping one
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Move towards the player
        navMeshAgent.SetDestination(player.position);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}