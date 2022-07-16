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
    public int ammo;
    public int max_ammo;
    public float firing_speed;
    public GameObject model;

    private void Awake()
    {

        Debug.Assert(GameManager.Instance.player.gunParent = this, "Unable to assign gunParent inside Player");
    }
}
