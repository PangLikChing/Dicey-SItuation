using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VoidEventListener : MonoBehaviour
{
    [Tooltip("The event channel scritpable object")]
    public VoidEventChannel channel;
    [Tooltip("Callback to respond to the unity event")]
    public UnityEvent response;

    private void OnEnable()
    {
        channel.RegisterListener(this);
    }

    private void OnDisable()
    {
        channel.UnregisterListener(this);
    }

    public void OnEventRaised()
    {
        response.Invoke();
    }
}
