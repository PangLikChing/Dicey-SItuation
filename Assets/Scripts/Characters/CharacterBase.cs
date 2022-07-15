using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterBase : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] protected float health = 100f;
    [SerializeField] protected float maxHealth = 100f;
    [SerializeField] protected bool alive = false;
    public UnityEvent<CharacterBase> onDeath;

    public float Health
    {
        get { return health; }
    }
    public float MaxHealth
    {
        get { return maxHealth; }
    }
    public bool Alive
    {
        get { return alive; }
    }

    protected virtual void Die()
    {
        alive = false;

        onDeath?.Invoke(this);
    }

    public virtual void TakeDamage(float damage)
    {
        if (damage <= 0)
            return;

        health -= damage;
    }

    public virtual void Heal(float heal)
    {
        if (heal <= 0)
            return;

        health += heal;

        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
}
