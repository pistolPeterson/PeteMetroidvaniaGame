using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PeteMetroidvania
{
    [RequireComponent(typeof (CapsuleCollider2D))]
    public class Crouch : Abilities
    {
        [SerializeField]
        [Range(0, 1)]
        protected float colliderMultiplier; 
        private CapsuleCollider2D playerCollider;
        private Vector2 originalCollider;
        private Vector2 crouchingColliderSize;
        private Vector2 originalOffset;
        private Vector2 crouchingOffset;

        protected override void Initialization()
        {
            base.Initialization();
            playerCollider = GetComponent<CapsuleCollider2D>();
            originalCollider = playerCollider.size;
            crouchingColliderSize = new Vector2(playerCollider.size.x, (playerCollider.size.y * colliderMultiplier));
            originalOffset = playerCollider.offset;
            crouchingOffset = new Vector2(playerCollider.offset.x, (playerCollider.offset.y * colliderMultiplier));
        }


        protected virtual void FixedUpdate()
        {
            CrouchHeld();
            Crouching();
        }


        protected virtual bool CrouchHeld()
        {
            if(Input.GetKey(KeyCode.X))
            {
                return true;
            }
            return false;
        }


        protected virtual void Crouching()
        {
            if(CrouchHeld() && character.isGrounded)
            {
                character.isCrouching = true; 
                anim.SetBool("Crouching", true);
                playerCollider.size = crouchingColliderSize;
                playerCollider.offset = crouchingOffset;
            }
            else
            {

                if(character.isCrouching)
                {
                    StartCoroutine(CrouchDisabled());
                }
               
            }
        }

        protected virtual IEnumerator CrouchDisabled()
        {
            playerCollider.offset = originalOffset;
            yield return new WaitForSeconds(0.01f);
            playerCollider.size = originalCollider;
            yield return new WaitForSeconds(0.15f);
            character.isCrouching = false;
            anim.SetBool("Crouching", false);
        }
    }

}
