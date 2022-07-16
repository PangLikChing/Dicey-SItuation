using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeFSM : FSM
{
    [HideInInspector] public Animator _animator;
    [HideInInspector] public Snake snake;

    protected override void Awake()
    {
        snake = GetComponent<Snake>();
        _animator = snake.gameObject.GetComponent<Animator>();
        base.Awake();

        snake.fsmAnimator = FSMGO.GetComponent<Animator>();
    }
}
