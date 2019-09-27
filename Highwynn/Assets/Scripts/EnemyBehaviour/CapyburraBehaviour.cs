using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Highwynn
{
    public class CapyburraBehaviour : EnemyBehavior
    {
        [Header("Capyburra Attributes")]
        [SerializeField]
        private CapyDen capyDen;
        private float idleTimer = 0.0f;
        private float speed  = 2.0f;

        //////////////////////////////////////////////// Overridden Classes /////////////////////////////////////////////////////////
        // Overrides base class OnSeen
        // Passes seen collider, and distance to collider
        protected override void OnSeen(Collider2D other, float distance) {
            // Get name of current animation
            string name = anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;

            if (other.tag == "Player") {
                capyDen.SummonFrens(this);

                if (distance > shortViewDistance) {
                    // If idling or walking 
                    if (name == "Idle" ||
                        name == "Walk" &&
                        name != "AttackOne")
                    {
                        anim.Play("Run");
                    }
                }
                // If Capy is within the short (attack) distance of the player
                else {
                    if (name != "AttackOne") {
                        speed = 0.0f;
                        anim.Play("AttackOne");
                    }
                }
            }

            // Layer 13 is the EnemyTurnaround layer. This marks where an enemy should turn
            if (other.gameObject.layer == 13) {
                if (distance < mediumViewDistance) {
                    // Debug.Log(distance);
                    movingRight = !movingRight;
                    // Flip is a base-class function
                    Flip();
                }
            }
        }

        // Overrides base class PlayerLost
        protected override void PlayerLost() {
            anim.Play("Idle");
        }

        // Overrides base class UpdateBehaviour
        protected override void UpdateBehaviour() {
            // Get current animation name
            string name = anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;

            // Dynamically set enemy speed based on current animation
            switch(name) {
                case "Idle":
                case "Sniff":
                    speed = 0.0f;
                    break;
                case "Walk":
                    speed = 2.0f;
                    break;
                case "Run":
                    speed = 8.0f;
                    break;
            }

            // Move is a base-class function
            Move(speed);

            // Increment timer
            idleTimer += Time.deltaTime;
        }

        // Overrides base class OnDeath
        protected override void OnDeath() {
            // "dying" prevents the "sight" update loop
            dying = true;
            speed = 0.0f;
            anim.Play("Dead");
        }

        //////////////////////////////////////////////////// Animation Events /////////////////////////////////////////////////////
        // Event runs when Idle or Walk anims finish
        // Capy stops to think deep and meaningful thoughts about the world
        private void EndIdle() {
            if (idleTimer > 5.0f) {
                anim.Play("Sniff");
            }
        }

        // Event runs when Sniff anim finishes
        // Capy determines if it is time to move
        private void EndSniff() {
            DecideDirection();
            DecideMove();
            idleTimer = 0.0f;
        }

        private void EndDeath() {
            base.OnDeath();
        }

        //////////////////////////////////////////////////// Helpers /////////////////////////////////////////////////////
        // Used by EndSniff animation event to determine move direction
        private void DecideDirection() {
            movingRight = Random.value >= 0.5f;

            Flip();
        }

        // Used by EndSniff animation event to determine if Idling or Moving
        private void DecideMove() {
            bool move = Random.value >= 0.5f;
            if (move) {
                anim.Play("Walk");
            }
            else {
                anim.Play("Idle");
            }
        }

        public CapyDen SetDen {
            set { capyDen = value; }
        }
    }

    // Description of animation logic
    /*
    Idle:
        Idle
        [Change_Idle] Idle/Walk --> Sniff
        [Change_Idle] Sniff --> Idle/Walk

    See Player:
        Idle --> Run

    Player in Range:
        Idle --> AttackOne
        Walk --> AttackOne
        Run --> AttackOne

    On Death:
        [Any_State] --> Dead
    */
}
