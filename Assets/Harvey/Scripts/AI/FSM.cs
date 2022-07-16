using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSM : MonoBehaviour
{
    public RuntimeAnimatorController FSMController;

    protected virtual void Awake()
    {
        GameObject FSMGO = new GameObject("FSM", typeof(Animator));
        FSMGO.transform.parent = transform;

        Animator animator = FSMGO.GetComponent<Animator>();
        animator.runtimeAnimatorController = FSMController;

        // optional hiding animator
        animator.hideFlags = HideFlags.HideInInspector;

        FSMBaseState[] behaviours = animator.GetBehaviours<FSMBaseState>();
        foreach(var behaviour in behaviours)
        {
            behaviour.Init(gameObject, this);
        }
    }
}
