using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : CharacterBase
{
    public float moveSpeed = 10.0f;

    protected virtual void Update()
    {
        #region Movement
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 movDir = new Vector3(horizontal, 0, vertical);

        if (movDir != Vector3.zero)
        {
            transform.Translate(movDir * moveSpeed * Time.deltaTime, Space.World);
        }
        #endregion
    }
}