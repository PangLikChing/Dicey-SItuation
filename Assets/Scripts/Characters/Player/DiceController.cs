using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DiceController : MonoBehaviour
{
    public Rigidbody rb;

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
    }
}