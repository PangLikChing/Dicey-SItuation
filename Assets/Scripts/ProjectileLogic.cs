using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProjectileLogic : MonoBehaviour
{
   [ReadOnly] public int damage;
    public bool isAOE;
    public List<Snake> enemiesInRange;

    private void OnCollisionEnter(Collision collision)
    {
        Snake enemy = collision.gameObject.GetComponent<Snake>();

        if (enemy != null)
        {
            if (!isAOE)
            {
                enemy.TakeDamage(damage);
                Destroy(this.gameObject);
            }
            else
            {
                if (enemiesInRange.Count != 0)
                {
                    foreach (Snake enemies in enemiesInRange)
                    {
                        if (enemies != null)
                        {
                            enemies.TakeDamage(damage);
                        }
                    }
                }
                Destroy(this.gameObject);
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Snake enemy = other.GetComponent<Snake>();
        if (enemy != null)
        {
            enemiesInRange.Add(enemy);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Snake enemy = other.GetComponent<Snake>();
        if (enemy != null)
        {
            enemiesInRange.Remove(enemy);
        }
    }

    public void SetDamage(int _damage)
    {
        damage= _damage;
    }

    public void SetAOE()
    {
        isAOE = true;
        GetComponent<SphereCollider>().enabled = true;
    }
}
