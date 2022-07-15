using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Test : MonoBehaviour
{
    public UnityEvent<int> Update;

    [SerializeField] int testIntValue;
    [SerializeField] float testFloatValue;

    public void test()
    {
        Update.Invoke(testIntValue);
    }
}
