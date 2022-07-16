using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteEventChannel", menuName = "Event/Sprite Event Channel")]
public class SpriteEventChannel : ScriptableObject
{
    private List<SpriteEventListener> listeners = new List<SpriteEventListener>();
    public void Raise(Sprite sprite)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(sprite);
        }
    }

    public void RegisterListener(SpriteEventListener listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(SpriteEventListener listener)
    {
        listeners.Remove(listener);
    }
}
