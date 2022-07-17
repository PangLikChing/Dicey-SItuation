using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProjectileLogic : MonoBehaviour
{
    private int damage;

    private void OnTriggerEnter(Collider other)
    {
        PlayerLogic player = other.GetComponent<PlayerLogic>();

        if (player != null)
        {
            player.TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
    private void Awake()
    {
        transform.LookAt(transform.forward, Vector3.up);
    }

    public void SetDamage(int _damage)
    {
        damage= _damage;
    }
}
