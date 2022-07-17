using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunParent : MonoBehaviour
{
    //GunParent Stats, these are the ones that the player and UI will need access to.
    [ReadOnly] public Gun.GunType type;
    [ReadOnly] public int ammo;
    [ReadOnly] public int max_ammo;
    [ReadOnly] public float firing_speed;
    public Gun gun;
    public GunAudio gunAudio;

    //Extra things which need to be private.
    private float shootCD;

    private void Awake()
    {
        if (GameManager.Instance.player)
        { 
            GameManager.Instance.player.gunParent = this;
        }

        if (gunAudio == null) gunAudio = GetComponent<GunAudio>();

        //initial gun setup
        type = gun.type;
        firing_speed = gun.firing_speed;
        ammo = gun.max_ammo;
        max_ammo = gun.max_ammo;
        gunAudio.shot = gun.gunshot;
    }

    private void Start()
    {
        GameManager.Instance.updateWeaponSprite.Invoke(gun.sprite);

        GameManager.Instance.updateAmmo.Invoke(ammo, max_ammo);
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
            gunAudio.PlaySound(gunAudio.reload);
        }
        else
        {
            //Inherit the other stats
            type = _gun.type;
            ammo = _gun.max_ammo;
            max_ammo = _gun.max_ammo;
            firing_speed = _gun.firing_speed;

            //We disable the current model, swap it with the new one and set it active
            gun.model.SetActive(false);
            gun = _gun;
            gun.model.SetActive(true);
            GameManager.Instance.updateWeaponSprite.Invoke(gun.sprite);
        }

        GameManager.Instance.updateAmmo.Invoke(ammo, max_ammo);
    }

    public void Shoot()
    {
        if (shootCD >= firing_speed)
        {
            if (ammo > 0)
            {
                //Because the only logic separate is for shotgun because its the most amazing thing ever
                if (gun.type == Gun.GunType.Shotgun)
                {

                    //Cache Eulers for later
                    //We take the forward angle and calculate steps to spawn the projectiles in
                    Vector3 eulerAngles = transform.rotation.eulerAngles;
                    float fwdAngle = GetAngleForward();
                    float angleDir = fwdAngle + 60.0f * 0.5f;
                    float angle = angleDir;
                    float angleStep = 15.0f;

                    for (int i = 0; i < 5; i++)
                    {
                        //We do math here (obv) to figure out what the resulted position is going to be
                        float angleRad = angle * (Mathf.PI / 180.0f);
                        Vector3 result = new Vector3(Mathf.Cos(angleRad), 0, Mathf.Sin(angleRad));
                        result = Quaternion.Euler(0, +eulerAngles.y, 0) * result;
                        Quaternion pos = Quaternion.FromToRotation(transform.forward, result);

                        ProjectileLogic projectile = Instantiate(gun.projectile, transform.position, pos * gun.projectile.transform.rotation).GetComponent<ProjectileLogic>();
                        projectile.SetDamage(gun.damage);

                        angle -= angleStep;
                    }

                    shootCD = 0f;
                }
                else if (gun.type == Gun.GunType.RPG)
                {
                    ProjectileLogic projectile = Instantiate(gun.projectile, transform.position, transform.rotation * gun.projectile.transform.rotation).GetComponent<ProjectileLogic>();
                    projectile.SetDamage(gun.damage);
                    projectile.SetAOE();
                    shootCD = 0f;
                }
                //All other instanatiations are in one line.
                else
                {
                    ProjectileLogic projectile = Instantiate(gun.projectile, transform.position, transform.rotation * gun.projectile.transform.rotation).GetComponent<ProjectileLogic>();
                    projectile.SetDamage(gun.damage);
                    shootCD = 0f;
                }
                //Reduce Ammo
                ammo--;
                gunAudio.PlaySound(gunAudio.shot);
                GameManager.Instance.updateAmmo.Invoke(ammo, max_ammo);
            }
            else
            {
                gunAudio.PlaySound(gunAudio.empty);
            }
        }

    }
    
    private float GetAngleForward()
    {
        Vector3 aimDir = transform.forward;
        float angle = Mathf.Atan2(aimDir.z, aimDir.x) * Mathf.Rad2Deg;
        if (angle < 0)
        {
            angle += 360;
        }
        return angle;
    }
}
