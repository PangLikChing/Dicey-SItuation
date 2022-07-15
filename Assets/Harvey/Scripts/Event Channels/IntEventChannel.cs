using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IntEventChannel", menuName = "Event/Int Event Channel")]
public class IntEventChannel : ScriptableObject
{
    private List<IntEventListener> listeners = new List<IntEventListener>();
    public void Raise(int integer)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(integer);
        }
    }

    public void RegisterListener(IntEventListener listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(IntEventListener listener)
    {
        listeners.Remove(listener);
    }
}
