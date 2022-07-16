using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpriteEventListener : MonoBehaviour
{
    [Tooltip("The event channel scritpable object")]
    public SpriteEventChannel channel;
    [Tooltip("Callback to respond to the unity event")]
    public UnityEvent<Sprite> response;

    private void OnEnable()
    {
        channel.RegisterListener(this);
    }

    private void OnDisable()
    {
        channel.UnregisterListener(this);
    }

    public void OnEventRaised(Sprite sprite)
    {
        response.Invoke(sprite);
    }
}
