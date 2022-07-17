using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunParent : MonoBehaviour
{
    //The guns themselves
    public Gun automatic;
    public Gun pistol;


    //GunParent Stats, these are the ones that the player and UI will need access to.
  [ReadOnly]  public Gun.GunType type;
  [ReadOnly] public int ammo;
  [ReadOnly] public int max_ammo;
  [ReadOnly] public float firing_speed;
    public Gun gun;

    //Extra things which need to be private.
    private float shootCD;

    private void Awake()
    {
        if (GameManager.Instance.player)
        { 
            GameManager.Instance.player.gunParent = this;
        }

    }
    private void Update()
    {
        shootCD += Time.deltaTime;
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
            gun.model.SetActive(false);
            gun = _gun;
            gun.model.SetActive(true);
        }
    }

    public void Shoot()
    {
        if (shootCD >= firing_speed && ammo > 0)
        {

            if (gun.type == Gun.GunType.Pistol | gun.type == Gun.GunType.Automatic | gun.type == Gun.GunType.Sniper)
            {
                Instantiate(gun.projectile, transform.position, transform.rotation);
                shootCD = 0f;
            }
            else if (gun.type == Gun.GunType.Shotgun)
            {

            }
            else if (gun.type == Gun.GunType.RPG)
            {

            }
           //If we want an RPG with AOE damage, need to add specific code for this below and use the GunType.RPG or sth 
        }
    }
}
