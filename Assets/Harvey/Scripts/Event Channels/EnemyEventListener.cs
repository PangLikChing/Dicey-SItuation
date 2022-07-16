using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyEventListener : MonoBehaviour
{
    [Tooltip("The event channel scritpable object")]
    public EnemyEventChannel channel;
    [Tooltip("Callback to respond to the unity event")]
    public UnityEvent<Enemy> response;

    private void OnEnable()
    {
        channel.RegisterListener(this);
    }

    private void OnDisable()
    {
        channel.UnregisterListener(this);
    }

    public void OnEventRaised(Enemy enemy)
    {
        response.Invoke(enemy);
    }
}
