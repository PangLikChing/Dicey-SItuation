using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoolEventListener : MonoBehaviour
{
    [Tooltip("The event channel scritpable object")]
    public BoolEventChannel channel;
    [Tooltip("Callback to respond to the unity event")]
    public UnityEvent<bool> response;

    private void OnEnable()
    {
        channel.RegisterListener(this);
    }

    private void OnDisable()
    {
        channel.UnregisterListener(this);
    }

    public void OnEventRaised(bool boolean)
    {
        response.Invoke(boolean);
    }
}
