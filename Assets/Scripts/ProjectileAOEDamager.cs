using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAOEDamager : MonoBehaviour
{
    private List<Snake> enemiesInRange;

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
   public void BOOM(int damage)
    {
        foreach (Snake enemy in enemiesInRange)
        {
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
