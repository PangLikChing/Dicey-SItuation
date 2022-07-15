using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : CharacterBase
{
    public float moveSpeed = 10.0f;

    [SerializeField] protected DiceController dice;

    protected virtual void Awake()
    {
        if (dice == null) dice = GetComponentInChildren<DiceController>();
    }

    protected virtual void Update()
    {
        #region Movement
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 movDir = new Vector3(horizontal, 0, vertical);

        if(movDir != Vector3.zero)
        {
            transform.Translate(movDir * moveSpeed * Time.deltaTime, Space.World);
        }
        #endregion
        
        #region RotateToMousePos
        Vector3 mousePos = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        Vector3 lookAt = Vector3.zero;
        
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            lookAt = hit.point;
        }

        lookAt.y = transform.position.y;

        transform.LookAt(lookAt);
        #endregion
    }
}