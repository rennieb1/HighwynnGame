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
                // IF IDLE ANIMATION
                // THEN RUN OR ATTACK ANIMATION
                // DEPENDING ON DISTANCE
                // AND STYLE OF ENEMY ATTACK
            }

            if (other.gameObject.layer == 13) {
                // IF TURNAROUND IS [DISTANCE] AWAY

                // movingRight = !movingRight;
                // Flip();
            }
        }

        protected override void UpdateBehaviour() {
            // Get current animation name
            string name = anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;

            switch(name) {
                // case "ANIMATION_NAME":
                //      SET SPEED
                //      break;
            }

            Move(speed);

            // PERHAPS USE TIMER HERE TO HANDLE IDLE LENGTH
        }

        protected override void PlayerLost() {
            // PLAY IDLE ANIMATION
            // ~ OR TRANSITION TO ~
        }

        protected override void OnDeath() {
            dying = true;
            speed = 0.0f;
            // PLAY DEATH ANIMATION
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
