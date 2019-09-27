using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Highwynn
{
    public class FoxBehaviour : EnemyBehavior
    {
        private float idleTimer = 0.0f; // Determines how long to idle before deciding on direction
        private float speed = 2.0f; // Default movement speed
        private bool postBite = false; // Determines if Foxy has just pounced and bitten

        //////////////////////////////////////////////// Overridden Classes /////////////////////////////////////////////////////////
        // Overrides base class OnSeen
        // Passes seen collider, and distance to collider
        protected override void OnSeen(Collider2D other, float distance) {
            // Get name of current animation
            string name = anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;

            if (other.tag == "Player") {
                // If walking, or far from player and just bit
                if (name == "Walk_Continous" || // Misspelling - Continuous
                    (distance > mediumViewDistance && postBite))
                {
                    anim.Play("WalkToRun_Start");
                    postBite = false;
                }

                // If Foxy is within the medium (attack) distance of the player
                if (distance <= mediumViewDistance) {
                    if (name != "BiteAttack") {
                        speed = 0.0f;
                        anim.Play("BiteAttack");
                    }
                }   
            }

            // Layer 13 is the EnemyTurnaround layer. This marks where an enemy should turn
            if (other.gameObject.layer == 13) {
                if (distance < shortViewDistance) {
                    movingRight = !movingRight;
                    // Flip is a base-class function
                    Flip();
                }
            }
        }

        // Overrides base class PlayerLost
        protected override void PlayerLost() {
            anim.SetTrigger("losePlayer");
        }

        // Overrides base class UpdateBehaviour
        protected override void UpdateBehaviour() {
            // Get current animation name
            string name = anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;

            // Dynamically set enemy speed based on current animation
            switch(name) {
                case "WalkToRun_Start":
                    speed = 2.5f;
                    break;
                case "Run_Continuous":
                    speed = 4.0f;
                    break;
                case "Run_End":
                    speed = 3.0f;
                    break;
            }

            // Move the enemy with the current speed -- Move is a base-class function
            Move(speed);

            // Increment timer
            idleTimer += Time.deltaTime;
        }

        // Overrides base class OnDeath
        protected override void OnDeath() {
            speed = 0.0f;
            anim.Play("Death");
            // Delay gameObject destruction so anim can finish playing
            StartCoroutine(DelayDeath());
        }

        //////////////////////////////////////////////////// Animation Events /////////////////////////////////////////////////////
        // Event runs when Idle anim finishes (for Foxy, this is Walk_Continuous)
        // Foxy decides if it is time to change direction
        private void EndIdle() {
            if (idleTimer > 5.0f) {
                DecideDirection();
                idleTimer = 0.0f;
            }
        }

        // Event runs when Foxy is starting the pounce portion of BiteAttack
        // Increases Foxy's speed dramatically
        private void BitePounce() {
            speed = 16.0f;
        }

        // Event runs when Foxy has landed after the pounce in BiteAttack
        // Sets Foxy's speed to 0
        private void BiteLand() {
            speed = 0.0f;
        }

        // Event runs at end of BiteAttack
        // Sets boolean used to determine if attack has finished
        private void BiteEnd() {
            postBite = true;
        }

        //////////////////////////////////////////////////// Helpers /////////////////////////////////////////////////////
        // Used by OnDeath function to delay base.OnDeath
        private IEnumerator DelayDeath() {
            // Delay by animation length + 1 second
            yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0)[0].clip.length + 1.0f);

            // Call base-class OnDeath to destroy gameObject
            base.OnDeath();
        }

        // Used by EndIdle animation event to determine move direction
        private void DecideDirection() {
            movingRight = Random.value >= 0.5f;
            // Flip is a base-class function
            Flip();
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
