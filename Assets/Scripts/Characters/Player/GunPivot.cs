using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPivot : MonoBehaviour
{
    public Transform follow;

    private void Update()
    {
        transform.position = follow.position;

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