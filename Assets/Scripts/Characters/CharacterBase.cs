using Project.Build.Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterBase : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] [ReadOnly] protected int health = 100;
    [SerializeField] protected int maxHealth = 100;
    [SerializeField] protected bool alive = true;
    //public UnityEvent<CharacterBase> onDeath;

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

        //onDeath?.Invoke(this);
    }

    public virtual void TakeDamage(int damage)
    {
        if (damage <= 0)
            return;

        health -= damage;
    }

    public virtual void Heal(int heal)
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
