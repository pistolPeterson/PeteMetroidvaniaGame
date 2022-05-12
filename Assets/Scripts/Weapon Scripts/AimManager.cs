using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;


namespace PeteMetroidvania
{
    public class AimManager : Abilities
    {

        public Solver2D aimingGun;
        public Solver2D aimingLeftHand;
        public Solver2D notAimingGun;
        public Solver2D notAimingLeftHand;

        public Transform whereToAim;
        public Transform whereToPlaceHand;
        public Transform origin;

        public Bounds bounds;

        protected override void Initialization()
        {
            base.Initialization();
            aimingGun.enabled = false;
            aimingLeftHand.enabled = false;
            bounds.center = origin.position;
             
        }

        public virtual void ChangeArms()
        {
            notAimingGun.enabled = !notAimingGun.enabled;
            aimingGun.enabled = !aimingGun.enabled;
            aimingLeftHand.enabled = !aimingLeftHand.enabled;   
            notAimingLeftHand.enabled= !notAimingLeftHand.enabled;
        }


        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(origin.position, bounds.size);
        }
    }
}

