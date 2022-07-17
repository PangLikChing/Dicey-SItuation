using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerLogic : CharacterBase
{    
    public Rigidbody rigidBody;
    
    [Header("Movement")]
    public float moveSpeed = 10.0f;

    [Header("Dice Roll")]
    [Tooltip("How much vertical force we apply to the dice on a jump")]
    public float vertJumpForce = 10f;
    [Tooltip("How much torque should we apply when player tries to roll the dice")]
    public float rotTorque = 20f;
    [Tooltip("How much time should pass after player rolls the dice before they can roll again")]
    public float rollCooldown = 1f;
    [Tooltip("How much time in seconds should we wait before we count the collision")]
    public float collisionTime = 0.2f;
    [SerializeField][ReadOnly] protected bool isGrounded = false;
    [SerializeField][ReadOnly] protected bool diceRoll = false;
    [SerializeField][ReadOnly] protected float rollTimer = 0f;
    [SerializeField][ReadOnly] protected float colTimer = 0f;
    [SerializeField] protected DiceSides diceSides;

    [Header("Guns")]
    public GunParent gunParent;

    [Header("Events")]
    public UnityEvent playerDeath;


    protected virtual void Awake()
    {
        if (rigidBody == null) rigidBody = GetComponent<Rigidbody>();

        if (diceSides == null) diceSides = GetComponentInChildren<DiceSides>();

        health = maxHealth;

        GameManager.Instance.updateHealth.Invoke(health);

        GameManager.Instance.player = this;
    }

    private void Start()
    {
        RollTheDice();
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
                rigidBody.AddTorque(movDir * rotTorque);
            }
        }
        #endregion

        #region RTD
        if (rollTimer > 0)
        {
            rollTimer -= Time.deltaTime;
        }
        else if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            RollTheDice();
        }
        #endregion

        #region Schoot
        if (Input.GetKey(KeyCode.Mouse0))
        {
            gunParent.Shoot();
        }
        #endregion
    }

    protected void OnCollisionStay(Collision collision)
    {     
        if(colTimer >= collisionTime)
        {
            isGrounded = true;
            rigidBody.angularVelocity = Vector3.zero;
            
            if(diceRoll)
            {
                diceRoll = false;
                GetDiceResults();
            }
        }
        else
        {
            colTimer += Time.deltaTime;
        }
    }

    protected void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
        colTimer = 0f;
    }

    protected void RollTheDice()
    {
        rollTimer = rollCooldown;
        Vector3 jumpForce = Vector3.up * vertJumpForce;
        rigidBody.AddForce(jumpForce, ForceMode.Impulse);

        StartCoroutine(RTD());
    }

    protected void GetDiceResults()
    {
        if (gunParent != null && diceSides != null)
        {
            Gun newGun = diceSides.GetHighestSide().gun;

            if(newGun != null)
                gunParent.ChangeGun(newGun);
        }
    }

    IEnumerator RTD()
    {
        yield return new WaitForSeconds(collisionTime);

        isGrounded = false;
        diceRoll = true;
    }
    
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        GameManager.Instance.updateHealth?.Invoke(health);

        if (health <= 0)
        {
            Die();

            // Tell the game manager that the player is dead
            playerDeath.Invoke();
        }
    }

    public void ShootGun()
    {
        if(gunParent.gun != null)
        {
            gunParent.Shoot();
        }
    }
}