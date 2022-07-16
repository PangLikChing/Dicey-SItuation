using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyEventChannel", menuName = "Event/Enemy Event Channel")]
public class EnemyEventChannel : ScriptableObject
{
    private List<EnemyEventListener> listeners = new List<EnemyEventListener>();
    public void Raise(Enemy enemy)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(enemy);
        }
    }

    public void RegisterListener(EnemyEventListener listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(EnemyEventListener listener)
    {
        listeners.Remove(listener);
    }
}
