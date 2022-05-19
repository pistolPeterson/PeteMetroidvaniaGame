using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PeteMetroidvania
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] protected KeyCode crouchHeld;
        [SerializeField] protected KeyCode dashHeld;
        [SerializeField] protected KeyCode sprintHeld;
        [SerializeField] protected KeyCode jump;
        [SerializeField] protected KeyCode weaponFired;

        [SerializeField] protected KeyCode upHeld;
        [SerializeField] protected KeyCode downHeld;
        [SerializeField] protected KeyCode tiltedUpheld;
        [SerializeField] protected KeyCode tiltedDownheld;

        [SerializeField] protected KeyCode aimingHeld;

        public virtual bool CrouchHeld()
        {
            if (Input.GetKey(crouchHeld))
            {
                return true;
            }
            return false;
        }



        public virtual bool DashPressed()
        {
            if (Input.GetKeyDown(dashHeld) )
            {
              
                return true;
            }
            else
                return false;

        }

        public virtual bool JumpHeld()
        {
            if (Input.GetKey(jump))
            {
                return true;
            }
            else
                return false;
        }

        public virtual bool JumpPressed()
        {
            if (Input.GetKeyDown(jump))
            {
               
                return true;
            }
            else
                return false;
        }

        public virtual bool SprintingHeld()
        {
            if (Input.GetKey(sprintHeld))
            {
              
                return true;
            }
            else
            {
                return false;
            }
        }


        public virtual bool WeaponFired()
        {
            if (Input.GetKeyDown(weaponFired))
            {
               
                return true;
            }
            else
                return false;
        }









        public virtual bool UpHeld()
        {
            if (Input.GetKey(upHeld))
            {

                return true;
            }
            else
                return false;
        }

        public virtual bool WeaponFiredHeld()
        {
            if (Input.GetKey(weaponFired))
            {

                return true;
            }
            else
                return false;
        }



        public virtual bool DownHeld()
        {
            if (Input.GetKey(downHeld))
            {

                return true;
            }
            else
                return false;
        }

        public virtual bool TiltedDownHeld()
        {
            if (Input.GetKey(tiltedDownheld))
            {

                return true;
            }
            else
                return false;
        }

        public virtual bool TiltedUpHeld()
        {
            if (Input.GetKey(tiltedUpheld))
            {

                return true;
            }
            else
                return false;
        }

        public virtual bool AimingHeld()
        {
            if (Input.GetKey(aimingHeld))
            {

                return true;
            }
            else
                return false;
        }



    }

}
