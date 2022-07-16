using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : CharacterBase
{
    [Header("Movement")]
    public float moveSpeed = 10.0f;

    [Header("Dice Roll")]
    public float vertJumpForce = 10f;
    public float horJumpForce = 2.5f;
    public float rotTorque = 20f;
    public float rollCooldown = 1f;
    [SerializeField] protected bool isGrounded = false;
    [SerializeField] protected bool diceRoll = false;
    [SerializeField] protected float rollTimer = 0f;


    public Rigidbody rb;

    protected virtual void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
    }

    protected virtual void Update()
    {
        #region Movement
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 movDir = new Vector3(horizontal, 0, vertical);

        if (movDir != Vector3.zero)
        {
            transform.Translate(movDir * moveSpeed * Time.deltaTime, Space.World);

            if(!isGrounded)
            {
                rb.AddTorque(movDir * rotTorque);
            }
        }
        #endregion

        if(rollTimer > 0)
        {
            rollTimer -= Time.deltaTime;
        }
        else if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            RollTheDice();
        }
    }

    protected void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
        rb.angularVelocity = Vector3.zero;

        if(diceRoll)
        {
            // TODO: Result calculations
            return;
        }
    }

    protected void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    protected void RollTheDice()
    {
        rollTimer = rollCooldown;
        Vector3 jumpForce = Vector3.up * vertJumpForce + Vector3.forward * horJumpForce;
        rb.AddForce(jumpForce, ForceMode.Impulse);
        isGrounded = false;
        diceRoll = true;
    }
}