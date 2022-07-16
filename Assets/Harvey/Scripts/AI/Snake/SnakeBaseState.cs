using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBaseState : FSMBaseState
{
    protected Snake snake;

    public override void Init(GameObject _owner, FSM _fsm)
    {
        base.Init(_owner, _fsm);

        SnakeFSM snakeFSM = (SnakeFSM)fsm;
        snake = snakeFSM.snake;
    }
}
