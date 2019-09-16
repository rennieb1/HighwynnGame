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
            base.OnDeath();
        }
    }
}
