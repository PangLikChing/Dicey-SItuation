using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base state for all FSM states
/// </summary>

public abstract class FSMBaseState : StateMachineBehaviour
{
    protected GameObject owner;
    protected FSM fsm;

    public virtual void Init(GameObject _owner, FSM _fsm)
    {
        owner = _owner;
        fsm = _fsm;
    }
}
