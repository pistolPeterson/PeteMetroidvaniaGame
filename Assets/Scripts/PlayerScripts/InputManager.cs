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

        


    }

}
