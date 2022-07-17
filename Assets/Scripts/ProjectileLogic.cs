using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProjectileLogic : MonoBehaviour
{
   [ReadOnly] public int damage;
    public bool isAOE;
   [SerializeField] private GameObject AOEObject;

    private void OnTriggerEnter(Collider other)
    {
        Snake enemy = other.GetComponent<Snake>();

        if (enemy != null)
        {
            if (!isAOE)
            {
                enemy.TakeDamage(damage);
                Destroy(this.gameObject);
            }
            else
            {
                AOEObject.GetComponent<ProjectileAOEDamager>().BOOM(damage);
                Destroy(this.gameObject);
            }

        }
    }

    public void SetDamage(int _damage)
    {
        damage= _damage;
    }

    public void SetAOE()
    {
        AOEObject.SetActive(true);
    }
}
