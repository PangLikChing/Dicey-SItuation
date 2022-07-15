using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoolEventChannel", menuName = "Event/Bool Event Channel")]
public class BoolEventChannel : ScriptableObject
{
    private List<BoolEventListener> listeners = new List<BoolEventListener>();
    public void Raise(bool boolean)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(boolean);
        }
    }

    public void RegisterListener(BoolEventListener listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(BoolEventListener listener)
    {
        listeners.Remove(listener);
    }
}
