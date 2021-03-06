using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PeteMetroidvania
{
    public class Weapon : Abilities
    {
        [SerializeField] protected List<WeaponTypes> weaponTypes;
        [SerializeField] public Transform gunBarrel;
        [SerializeField] protected Transform gunRotation;



        public List<GameObject> currentPool = new List<GameObject>();
        public GameObject currentProjectile;
        public WeaponTypes currentWeapon;
        public float currentTimeTillChangeArms; 

        private GameObject projectileParentFolder;
        private float currentTimeBetweenShots;

        protected override void Initialization()
        {
            base.Initialization();
           foreach(WeaponTypes weapon in weaponTypes)
            {
                
                GameObject newPool = new GameObject();
                projectileParentFolder = newPool;
                objectPooler.CreatePool(weapon, currentPool, projectileParentFolder);
            }

            currentWeapon = weaponTypes[0];
        }

        protected virtual void Update()
        {
           
            if (input.WeaponFired())
            {
               
                FireWeapon(); 
            }
        }

        protected virtual void FixedUpdate()
        {
            PointGun();
            NegateTimeTillChangeArms();
            FireWeaponHeld();
        }

        protected virtual  void FireWeaponHeld()
        {
           if(input.WeaponFiredHeld())
            {
                if(currentWeapon.automatic)
                {
                    Debug.Log("current weapon is automatic");
                    currentTimeTillChangeArms = currentWeapon.lifetime;
                    aimManager.ChangeArms();
                    currentTimeBetweenShots -= Time.deltaTime;
                    if(currentTimeBetweenShots < 0)
                    {
                        currentProjectile = objectPooler.GetObject(currentPool);
                        if (currentProjectile != null)
                        {
                            Invoke("PlaceProjectile", .1f);
                        }

                        currentTimeBetweenShots = currentWeapon.timeBetweenShots;
                    }
                }
            }
        }

        protected virtual void PointGun()
        {
            if (!aimManager.aiming)
            {
                if (!character.isFacingLeft)
                {
                    if (character.isWallsliding)
                    {
                        aimManager.whereToAim.position = new Vector2(aimManager.bounds.min.x, aimManager.bounds.center.y);
                    }
                    else
                    {
                        aimManager.whereToAim.position = new Vector2(aimManager.bounds.max.x, aimManager.bounds.center.y);
                    }

                }
                else
                {
                    if (character.isWallsliding)
                    {
                        aimManager.whereToAim.position = new Vector2(aimManager.bounds.max.x, aimManager.bounds.center.y);
                    }
                    else
                    {
                        aimManager.whereToAim.position = new Vector2(aimManager.bounds.min.x, aimManager.bounds.center.y);
                    }
                }
            }
            aimManager.aimingGun.transform.GetChild(0).position = aimManager.whereToAim.position;
            aimManager.aimingLeftHand.transform.GetChild(0).position = aimManager.whereToPlaceHand.position;
        }


        protected virtual void NegateTimeTillChangeArms()
        {
            currentTimeTillChangeArms -= Time.deltaTime;
        }

        protected virtual void FireWeapon()
        {
            currentTimeTillChangeArms = currentWeapon.lifetime;
            aimManager.ChangeArms();
            currentProjectile = objectPooler.GetObject(currentPool);
            if(currentProjectile != null)
            {
                Invoke("PlaceProjectile", .1f);
            }
            currentTimeBetweenShots = currentWeapon.timeBetweenShots;
        }

        protected virtual void PlaceProjectile()
        {
            currentProjectile.transform.position = gunBarrel.position;
            currentProjectile.transform.rotation = gunRotation.rotation;
            currentProjectile.SetActive(true);
            if(!character.isFacingLeft )
            {
                currentProjectile.GetComponent<Projectile>().left = false;
            }
            else
                currentProjectile.GetComponent<Projectile>().left = true;
            currentProjectile.GetComponent<Projectile>().fired = true;

        }
    }
}

