using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PeteMetroidvania
{
    [CreateAssetMenu(fileName ="WeaponType", menuName = "Metroidvania/Weapons", order = 1)]
    public class WeaponTypes : ScriptableObject
    {
        public GameObject projectile;
        public float projectileSpeed;
        public int amountToPool;
        public float lifetime;
        public bool automatic;
        public float timeBetweenShots; 
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

