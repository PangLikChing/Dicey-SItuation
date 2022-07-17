using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Gun : MonoBehaviour
{
        public int max_ammo;
        public int damage;
        public float firing_speed;
        public GameObject model;
        public enum GunType { Pistol, Automatic };
        public GunType type;
        public GameObject projectile;

    private void Awake()
    {
        projectile.GetComponent<ProjectileLogic>().SetDamage(damage);
    }
}
