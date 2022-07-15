using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementProt : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float heightAdjust = 0.3f;

    public GameObject dice;
    public Rigidbody diceRigidbody;
    public SphereCollider sphereCollider;
    public BoxCollider boxCollider;

    bool heightRaised = false;

    private void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        if (vertical != 0.0f || horizontal != 0.0f)
        {
            if(!heightRaised)
            {
                heightRaised = true;
                transform.Translate(0, heightAdjust, 0);
                sphereCollider.enabled = true;
                boxCollider.enabled = false;
            }
            Vector3 movDir = new Vector3(horizontal, 0, vertical);
            Vector3 torque = new Vector3(vertical, 0, -horizontal);
            
            transform.Translate(movDir * (moveSpeed * Time.deltaTime));

            if(diceRigidbody != null)
            {
                diceRigidbody.AddTorque(torque * moveSpeed);
            }
        }
        else if (heightRaised)
        {
            heightRaised = false;
            transform.Translate(0, -heightAdjust, 0);
            sphereCollider.enabled = false;
            boxCollider.enabled = true;
            diceRigidbody.angularVelocity = Vector3.zero;
        }
    }
}
