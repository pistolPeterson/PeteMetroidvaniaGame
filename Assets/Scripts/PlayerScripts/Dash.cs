using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PeteMetroidvania
{
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class Dash : Abilities
    {
       


        [SerializeField] protected float dashForce; 

        [SerializeField] protected float dashCooldownTime; 

        [SerializeField] protected float dashAmountTime; 
        [SerializeField] protected LayerMask dashingLayers;

        private bool canDash;
        private float dashCountDown;
        private CapsuleCollider2D capsuleCollider2D;

        // Start is called before the first frame update
        void Start()
        {
            Initialization();
        }

        protected override void Initialization()
        {
            base.Initialization();
            capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            Dashing();
        }

       

        protected virtual void Dashing()
        {
          
            if (input.DashPressed() && canDash)
            {
               
                dashCountDown = dashCooldownTime;
                character.isDashing = true;
                capsuleCollider2D.direction = CapsuleDirection2D.Horizontal;
                capsuleCollider2D.size = new Vector2(capsuleCollider2D.size.y, capsuleCollider2D.size.x);
                anim.SetBool("Dashing", true);
                StartCoroutine(FinishedDashing());
            }
            
        }

        protected virtual void FixedUpdate()
        {
            DashMode();
            ResetDashCounter();
        }

        protected virtual void DashMode()
        {
           
            if(character.isDashing)
            {
                FallSpeed(0);
                movement.enabled = false;

                if(!character.isFacingLeft)
                {
                    DashCollision(Vector2.right, .5f, dashingLayers);
                    rb.AddForce(Vector2.right * dashForce);
                }
                else
                {
                    DashCollision(Vector2.left, .5f, dashingLayers);
                    rb.AddForce(Vector2.left * dashForce);
                }
            }
        }


        protected virtual void DashCollision(Vector2 direction, float distance, LayerMask collision)
        {

            RaycastHit2D[] hits = new RaycastHit2D[10];
            int numHits = col.Cast(direction, hits, distance);
            for (int i = 0; i < numHits; i++)
            {
                if ((1 << hits[i].collider.gameObject.layer & collision) != 0)
                {
                   hits[i].collider.enabled = false;
                    StartCoroutine(TurnColliderBackOn(hits[i].collider.gameObject));
                }
            }
           
        }

        protected virtual void ResetDashCounter()
        {
            if(dashCountDown > 0)
            {
                canDash = false; 
                dashCountDown -= Time.deltaTime;
            }
            else 
                canDash = true;
        }

        protected virtual IEnumerator FinishedDashing()
        {
            yield return new WaitForSeconds(dashAmountTime);
            capsuleCollider2D.direction = CapsuleDirection2D.Vertical;
            capsuleCollider2D.size = new Vector2(capsuleCollider2D.size.y, capsuleCollider2D.size.x);
            anim.SetBool("Dashing", false);
            character.isDashing = false;
            FallSpeed(1);
            movement.enabled = true;
            rb.velocity = new Vector2(0, rb.velocity.y);
        }


        protected virtual IEnumerator TurnColliderBackOn(GameObject obj)
        {
            yield return new WaitForSeconds(dashAmountTime);
            obj.GetComponent<Collider2D>().enabled = true;  
        }


    }
}

