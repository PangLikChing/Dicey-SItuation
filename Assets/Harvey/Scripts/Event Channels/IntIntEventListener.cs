using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IntIntEventListener : MonoBehaviour
{
    [Tooltip("The event channel scritpable object")]
    public IntIntEventChannel channel;
    [Tooltip("Callback to respond to the unity event")]
    public UnityEvent<int, int> response;

    private void OnEnable()
    {
        channel.RegisterListener(this);
    }

    private void OnDisable()
    {
        channel.UnregisterListener(this);
    }

    public void OnEventRaised(int integer1, int integer2)
    {
        response.Invoke(integer1, integer2);
    }
}
