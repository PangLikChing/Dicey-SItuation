using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Vector3 cameraOffset;
    public Transform follow;

    private void Update()
    {
        if (follow != null)
        {
            transform.position = follow.position + cameraOffset;
            transform.LookAt(follow);
        }
    }
}
