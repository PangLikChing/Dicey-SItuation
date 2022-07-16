using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    Transform player;

    void Start()
    {
        player = GameManager.Instance.player.transform;
    }

    void LateUpdate()
    {
        // Follow the player
        transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
    }
}
