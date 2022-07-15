using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Vector3 cameraOffset;
    [SerializeField] Transform player;

    private void Update()
    {
        if (player != null)
        {
            transform.position = player.position + cameraOffset;
            transform.LookAt(player);
        }
    }
}
