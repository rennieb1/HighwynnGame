using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Highwynn 
{
    public class StagBehaviour : EnemyBehavior
    {
        private float speed = 0.0f;
        //////////////////////////////////////////////// Overridden Classes /////////////////////////////////////////////////////////
        protected override void OnSeen(Collider2D other, float distance) {
            // Get name of current animation
            string name = anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;

            if (other.tag == "Player") {
                if (name == "Stag_Idle" ||  name == "Walk_Start")
                {
                    anim.Play("Attack_Charge");
                    anim.ResetTrigger("idleWalk");
                    anim.ResetTrigger("walkIdle");
                }

                // IF IDLE ANIMATION
                // THEN RUN OR ATTACK ANIMATION
                // DEPENDING ON DISTANCE
                // AND STYLE OF ENEMY ATTACK
                anim.SetBool("lostPlayer", false);
            }

            if (other.gameObject.layer == 13) {
                // IF TURNAROUND IS [DISTANCE] AWAY
                if (distance < shortViewDistance)
                {
                    stopped = true;
                    if (name == "Stag_Idle" || name == "Walk_Start")
                    {
                        movingRight = !movingRight;
                        Flip();
                        stopped = false;
                    }
                    else if (name == "Attack_Charge" || name == "Walk_End")
                    {
                        Stop();
                    }

                }
            }
        }

        protected override void UpdateBehaviour() {
            // Get current animation name
            string name = anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;

            switch(name) {

                // case "ANIMATION_NAME":
                //      SET SPEED
                //      break;

                case "Walk_Start":
                    Move(1.0f);
                    break;

                case "Walk_Continuous":
                    Move(3.0f);
                    break;

                case "Attack_Charge":
                    anim.SetBool("seePlayer", false);
                    Move(8.0f);
                    break;
                case "Walk_End":
                    Move(1.0f);
                    break;
                case "Stag_Idle":
                    Move(0.0f);
                    break;
                case "Stag_Death":
                    Move(0.0f);
                    break;

                   
            }

           

            // PERHAPS USE TIMER HERE TO HANDLE IDLE LENGTH
        }

        protected override void PlayerLost() {
            if (!anim.GetBool("lostPlayer"))
            {
                anim.SetBool("lostPlayer", true);
            }
            // PLAY IDLE ANIMATION
            // ~ OR TRANSITION TO ~
        }

        protected override void OnDeath() {
            dying = true;
           speed = 0.0f;
            // PLAY DEATH ANIMATION
            anim.Play("Stag_Death");
        }
        protected override void Stop()
        {
            anim.Play("Stag_Idle");
        }
        void OnStopped()
        {
            if (stopped)
            {
                movingRight = !movingRight;
                Flip();
            }

            stopped = false;
        }

        private void DecideDirection()
        {
            movingRight = Random.value >= 0.5f;
            // Flip is a base-class function
            Flip();
        }

        // Used by EndIdle to determine if moving or idling
        private void DecideMove()
        {
            bool move = (Random.value >= 0.5f);
            if (move)
            {
                anim.SetBool("idleWalk", true);
                anim.SetBool("walkIdle", false);
            }
            else
            {
                anim.SetBool("walkIdle", true);
                anim.SetBool("idleWalk", false);
            }
        }
        void OnColliderEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "EnemyTurnaround")
            {
                Stop();
            }
        }
        //////////////////////////////////////////////////// Animation Events /////////////////////////////////////////////////////
        // EACH OF THESE SUGGESTIONS IS DEPENDANT ON WHAT ANIMATIONS STAG HAS

        // SUGGEST AN END-OF-IDLE-ANIMATION EVENT

        // SUGGEST AN END-OF-DEATH-ANIMATION EVENT

        // SUGGEST AN END-OF-RUN-ANIMATION EVENT

        // SUGGEST MID-OR-END-OF-ATTACK-ANIMATION EVENTS

        //////////////////////////////////////////////////// Helpers /////////////////////////////////////////////////////

        // SUGGEST FUNCTION TO HANDLE DELAYED DEATH

        // STRONGLY SUGGEST FUNCTION TO DECIDE DIRECTION

        // STRONGLY SUGGEST FUNCTION TO DECIDE IF MOVING OR IDLING

        // COLLISION FUNCTIONS HERE

        // PUBLIC ACCESSORS HERE
    }

    // Description of animation logic
    /*
    DESCRIBE ANIMATION LOGIC
    */
}
