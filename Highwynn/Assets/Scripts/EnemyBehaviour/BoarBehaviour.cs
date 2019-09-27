using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Highwynn
{
    public class BoarBehaviour : EnemyBehavior
    {
        //////////////////////////////////////////////// Overridden Classes /////////////////////////////////////////////////////////
        protected override void OnSeen(Collider2D other, float distance) {
            // Get name of current animation
            string name = anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;

            if (other.tag == "Player") {
                // If boar idling or walking, they have seen player so play BoarSeePlayer
                if (name == "BoarIdle" ||
                    name == "Walk") 
                {
                    anim.Play("BoarSeePlayer");
                    anim.ResetTrigger("idleWalk");
                    anim.ResetTrigger("walkIdle");
                }
                anim.SetBool("lostPlayer", false);
            }

            // Layer 13 is EnemyTurnaround, this means boar has seen it and must turn
            if (other.gameObject.layer == 13) {
                // If the object is within the short view distance (so very close)
                if (distance < shortViewDistance) {
                    stopped = true;

                    if (name == "Walk" || 
                        name == "BoarIdle") 
                    {
                        movingRight = !movingRight;
                        Flip();
                        stopped = false;
                    }
                    else if (name == "BoarRun" ||
                             name == "RunContinue") 
                    {
                        Stop();
                    }
                }
            }
        }

        // Overrides base class UpdateBehaviour
        protected override void UpdateBehaviour() {
            // Get name of current animation
            string name = anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;

            // Dynamically set boar move speed based on animation name
            switch(name) {
                case "BoarSeePlayer":
                    anim.SetBool("seePlayer", false);
                    break;

                case "Walk":
                    Move(10.0f);
                    break;

                case "BoarRun":
                    Move(20.0f);
                    break;

                case "RunContinue":
                    Move(40.0f);
                    break;
                case "BoarStopping":
                    Move(5.0f);
                    break;
            }
        }

        // Overrides base class PlayerLost
        protected override void PlayerLost() {
            if (!anim.GetBool("lostPlayer")) {
                anim.SetBool("lostPlayer", true);
            }
        }

        // Overrides base class OnDeath
        protected override void OnDeath() {
            // "dying" prevents the "sight" update loop
            dying = true;
            // Play death animation/effect

            base.OnDeath();
        }

        // Overrides base class Stop
        // Called within script to stop boar
        protected override void Stop() {
            anim.Play("BoarStopping");
        }

        //////////////////////////////////////////////////// Animation Events /////////////////////////////////////////////////////
        // End of idle & walking animations, to decide if moving or idling again
        void EndIdle() {
            if (!anim.GetBool("seePlayer")) {
                DecideDirection();
                DecideMove();
            }
        }

        // End of stop animation
        void OnStopped() {
            if (stopped) {
                movingRight = !movingRight;
                Flip();
            }

            stopped = false;
        }

        //////////////////////////////////////////////////// Helpers /////////////////////////////////////////////////////
        // Used by EndIdle to determine move direction (left or right)
        private void DecideDirection() {
            movingRight = Random.value >= 0.5f;
            // Flip is a base-class function
            Flip();
        }

        // Used by EndIdle to determine if moving or idling
        private void DecideMove() {
            bool move = (Random.value >= 0.5f);
            if (move) {
                anim.SetBool("idleWalk", true);
                anim.SetBool("walkIdle", false);
            }
            else {
                anim.SetBool("walkIdle", true);
                anim.SetBool("idleWalk", false);
            }
        }

        // Collision detection to ensure rushing boar can't pass EnemyTurnaround markers
        void OnColliderEnter2D(Collider2D other) {
            if (other.gameObject.tag == "EnemyTurnaround") {
                Stop();
            }
        }

        // Description of animation logic
        /*
        Idle:
            Walk_Continuous

        See Player:
            Walk_Continuous --> WalkToRun_Start
            [Player not in attack range, just pounced] --> WalkToRun_Start
            WalkToRun_Start --> Run_Continuous
            
        Lose Player:
            Run_Continuous --> Run_End
            
        If Player Within Range:
            [Any_State] --> BiteAttack

        During BiteAttack:
            0 speed until pounce
            Pounce fast [16.0f]
            0 speed when landed
            
        On Death
            [Any_State] --> Death
        */
    }
}
