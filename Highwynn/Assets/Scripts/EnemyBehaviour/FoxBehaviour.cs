using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Highwynn
{
    public class FoxBehaviour : EnemyBehavior
    {
        private float timer = 0.0f;
        private float speed = 0.5f;
        protected override void OnSeen(Collider2D other, float distance) {
            if (other.tag == "Player") {
                speed = 4.0f;
            }
        }

        protected override void PlayerLost() {
            speed = 0.5f;
        }

        protected override void UpdateBehaviour() {
            Move(speed);

            if (timer >= 5.0f) {
                movingRight = !movingRight;
                Flip();
                timer = 0.0f;
            }
            else {
                timer += Time.deltaTime;
            }
        }

        protected override void Stop() {

        }

        protected override void OnDeath() {
            anim.Play("Death");
            StartCoroutine(DelayDeath());

            // base.OnDeath();
        }

        private IEnumerator DelayDeath() {

            Debug.Log(anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);
            yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0)[0].clip.length + 1.0f);

            base.OnDeath();

        }
    }

    /*
    Idle:
        Walk_Continuous

    See Player:
        Walk_Continuous --> WalkToRun_Start
        WalkToRun_Start --> Run_Continuous
        
    Lose Player:
        Run_Continuous --> Run_End
        
    If Player Within Range:
        [Any_State] --> BiteAttack
        
    On Death
        [Any_State] --> Death
    */
}
