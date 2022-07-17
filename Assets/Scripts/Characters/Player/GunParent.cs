using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunParent : MonoBehaviour
{
    #region Guns Data
    [System.Serializable]
    public class Gun 
    {
        public int max_ammo;
        public int damage;
        public float firing_speed;
        public GameObject model;
        public enum GunType {Pistol, Automatic };
        public GunType type;

    }

    public Gun Automatic = new Gun { damage = 5, firing_speed = 0.2f, max_ammo = 30 , type = Gun.GunType.Automatic };
    public Gun Pistol = new Gun { damage = 3, firing_speed = 0.4f, max_ammo = 7 , type = Gun.GunType.Pistol };
    #endregion

    //GunParent Stats, these are the ones that the player and UI will need access to.
    public Gun.GunType type;
    public int ammo;
    public int max_ammo;
    public float firing_speed;
    public GameObject model;

    //Extra things which need to be private.
    private float shootCD;

    private void Awake()
    {
        if (GameManager.Instance.player)
        { 
            GameManager.Instance.player.gunParent = this;
        }

    }

    public void ChangeGun(Gun _gun)
    {
        //If we have the same gun, just reload
        if (type == _gun.type)
        {
            ammo = max_ammo;
        }
        else
        {
            //Inherit the other stats
            type = _gun.type;
            ammo = _gun.max_ammo;
            firing_speed = _gun.firing_speed;

            //We disable the current model, swap it with the new one and set it active
            model.SetActive(false);
            model = _gun.model;
            model.SetActive(true);
        }
    }

    public void Shoot()
    {

    }



}
