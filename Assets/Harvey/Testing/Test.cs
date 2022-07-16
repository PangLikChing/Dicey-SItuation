using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Test : MonoBehaviour
{
    public UnityEvent<int> Update;

    public UnityEvent<int, int> UpdateAmmo;

    [SerializeField] int testIntValue1, testIntValue2;
    [SerializeField] float testFloatValue;

    public UnityEvent<int> takeDamage;

    public void test()
    {
        //Update.Invoke(testIntValue1);

        UpdateAmmo.Invoke(testIntValue1, testIntValue2);
    }

    private void Start()
    {
        takeDamage.Invoke(10);
    }
}
