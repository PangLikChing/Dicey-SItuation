using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IntEventChannel", menuName = "Event/Int Int Event Channel")]
public class IntIntEventChannel : ScriptableObject
{
    private List<IntIntEventListener> listeners = new List<IntIntEventListener>();
    public void Raise(int integer1, int integer2)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(integer1, integer2);
        }
    }

    public void RegisterListener(IntIntEventListener listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(IntIntEventListener listener)
    {
        listeners.Remove(listener);
    }
}
