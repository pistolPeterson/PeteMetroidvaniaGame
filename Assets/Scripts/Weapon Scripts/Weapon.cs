using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PeteMetroidvania
{
    public class Weapon : Abilities
    {
        [SerializeField] protected List<WeaponTypes> weaponTypes;

        protected override void Initialization()
        {
            base.Initialization();
           foreach(WeaponTypes weapon in weaponTypes)
            {
                Debug.Log("hello");
                objectPooler.CreatePool(weapon);
            }
        }
    }
}

