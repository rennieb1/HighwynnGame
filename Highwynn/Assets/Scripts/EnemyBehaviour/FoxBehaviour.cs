using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Highwynn
{
    public class FoxBehaviour : EnemyBehavior
    {
        private float timer = 0.0f;
        private float speed = 2.0f;
        private bool postBite = false;

        // Overrides base class OnSeen
        // Passes seen collider, and distance to collider
        protected override void OnSeen(Collider2D other, float distance) {
            string clip = anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;

            if (other.tag == "Player") {
                Debug.Log("Player");
                if (clip == "Walk_Continous" || // Misspelling - Continuous
                    (distance > attackDistance && postBite))
                {
                    anim.Play("WalkToRun_Start");
                    postBite = false;
                }

                if (distance <= attackDistance) {
                    if (clip != "BiteAttack") {
                        speed = 0.0f;
                        anim.Play("BiteAttack");
                    }
                }   
            }

            if (other.gameObject.layer == 13) {
                if (distance < turnDistance) {
                    movingRight = !movingRight;
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
            AnimatorClipInfo currentAnim = anim.GetCurrentAnimatorClipInfo(0)[0];

            switch(currentAnim.clip.name) {
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

            Move(speed);

            timer += Time.deltaTime;
        }

        // Overrides base class OnDeath
        protected override void OnDeath() {
            speed = 0.0f;
            anim.Play("Death");
            StartCoroutine(DelayDeath());
        }

        // Used by OnDeath function to delay base.OnDeath
        private IEnumerator DelayDeath() {

            Debug.Log(anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);
            yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0)[0].clip.length + 1.0f);

            base.OnDeath();

        }

        //// Animation Events
        // Event runs when Idle anim finishes (for Foxy, this is Walk_Continuous)
        // Foxy decides if it is time to change direction
        private void EndIdle() {
            if (timer > 5.0f) {
                DecideDirection();
                timer = 0.0f;
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

        // Used by EndIdle animation event to determine move direction
        private void DecideDirection() {
            movingRight = Random.value >= 0.5f;

            Flip();
        }
    }

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
