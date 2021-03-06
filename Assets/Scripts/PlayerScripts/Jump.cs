using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PeteMetroidvania
{
    public class Jump : Abilities
    {
        [SerializeField] protected bool limitAirJumps;
        [SerializeField] protected int maxJumps;
        [SerializeField] protected float jumpForce;
        [SerializeField] protected float holdForce;
        [SerializeField] protected float buttonHoldTime;
        [SerializeField] protected float distanceToCollider;
        [SerializeField] protected float horizontalWallJumpForce;
        [SerializeField] protected float verticalWallJumpForce;
        [SerializeField] protected float maxJumpSpeed;
        [SerializeField] protected float maxFallSpeed;
        [SerializeField] protected float acceptedFallSpeed;
        [SerializeField] protected float glideTime;
        [SerializeField][Range(-2.2f, 2.2f)] protected float gravity;

        [SerializeField] protected float wallJumpTime;
        public LayerMask collisionLayer;

        private bool isJumping;
        private bool isWallJumping;
        private bool flipped; 
        private float jumpCountDown;
        private float fallCountDown;
        private int numOfJumpsLeft;

        protected override void Initialization()
        {
            base.Initialization();
            numOfJumpsLeft = maxJumps;
            jumpCountDown = buttonHoldTime;
            fallCountDown = glideTime;
            
        }


        // Update is called once per frame
        protected virtual void Update()
        {
            CheckForJump();
           
        }

        protected virtual bool CheckForJump()
        {
            if(input.JumpPressed())
            {

                if (!character.isGrounded && numOfJumpsLeft == maxJumps)
                {
                    isJumping = false;
                    return false;
                }

                if (limitAirJumps && Falling(acceptedFallSpeed))
                {
                    isJumping = false;
                    return false;
                }

                if(character.isWallsliding)
                {
                    isWallsliding = true;
                    isWallJumping = true;
                    return false;
                }

                numOfJumpsLeft--;
                if(numOfJumpsLeft >= 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                    jumpCountDown = buttonHoldTime;
                    isJumping = true;
                    fallCountDown = glideTime;  
                }

              
                return true;

            }
            else
            {
                return false;
            }
        }

       

        protected virtual void FixedUpdate()
        {
            IsJumping();
            Gliding();
            GroundCheck();
            WallSliding();
            WallJump();
        }

        protected virtual void IsJumping()
        {
            
            if(isJumping)
            {
               
                rb.AddForce(Vector2.up * jumpForce );
                AdditionalAir();
            }

            if(rb.velocity.y > maxJumpSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, maxJumpSpeed); 
            }
        }

        protected virtual void Gliding()
        {
            if(Falling(0) && input.JumpHeld())
            {
                fallCountDown -= Time.deltaTime;
                if(fallCountDown > 0 && rb.velocity.y > acceptedFallSpeed)
                {
                    anim.SetBool("Gliding", true);

                    FallSpeed(gravity);
                    return;
                }
            }
            anim.SetBool("Gliding", false);

        }

        protected virtual void AdditionalAir()
        {
            if (input.JumpHeld())
            {
                jumpCountDown -= Time.deltaTime;
                if (jumpCountDown <= 0)
                {
                    jumpCountDown = 0;
                    isJumping = false;
                }
                else
                    rb.AddForce(Vector2.up * holdForce);
            }
            else
                isJumping = false;

        }
        protected virtual void GroundCheck()
        {
            if(CollisionCheck(Vector2.down, distanceToCollider, collisionLayer) && !isJumping)
            {
                anim.SetBool("IsGrounded", true);
                character.isGrounded = true;
                numOfJumpsLeft = maxJumps;
                fallCountDown = glideTime;
            }
            else
            {
                anim.SetBool("IsGrounded", false);
                character.isGrounded = false;
            
                if(Falling(0) && rb.velocity.y < maxFallSpeed)
                {
                    rb.velocity = new Vector2(rb.velocity.x, maxFallSpeed);
                }
            }
            anim.SetFloat("VerticalSpeed", rb.velocity.y);
        }

        protected virtual bool WallCheck()
        {
            if((!character.isFacingLeft && CollisionCheck(Vector2.right, distanceToCollider, collisionLayer) || character.isFacingLeft && CollisionCheck(Vector2.left, distanceToCollider, collisionLayer)) && movement.MovementPressed() && !character.isGrounded)
            {
              
                return true; 
            }
            return false;
        }

        protected virtual bool WallSliding()
        {
            if(WallCheck())
            {

                if(!flipped)
                {
                    Flip();
                    flipped = true;
                }
                FallSpeed(gravity);
                character.isWallsliding = true;
                anim.SetBool("WallSliding", true);
                return true;
            }
            else
            {
                character.isWallsliding = false;
                anim.SetBool("WallSliding", false);
                if(flipped && !isWallJumping)
                {
                    Flip();
                    flipped= false;
                }
                return false;
            }
        }

        protected virtual void WallJump()
        {
            if(isWallJumping)
            {
                rb.AddForce( Vector2.up* verticalWallJumpForce);

                if(!character.isFacingLeft )
                {
                    rb.AddForce(Vector2.left * horizontalWallJumpForce);
                }

                if (character.isFacingLeft)
                {
                    rb.AddForce(Vector2.right * horizontalWallJumpForce);
                }

                StartCoroutine(WallJumped());
            }
        }


        protected virtual IEnumerator WallJumped()
        {
            movement.enabled = false;
            yield return new WaitForSeconds(wallJumpTime);
            movement.enabled = true;
            isWallJumping = false;
        }
    }
}

