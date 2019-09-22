using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Highwynn 
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyBehavior : MonoBehaviour
    {
        [SerializeField]
        private float health = 5.0f;

        [Header("View Distances")]
        [SerializeField]
        private float longViewDistance = 5.0f;
        [SerializeField]
        protected float mediumViewDistance = 2.5f;
        [SerializeField]
        protected float shortViewDistance = 1.0f;
        [SerializeField]
        [Range(0.1f, 1.5f)]
        private float viewHeight = 0.25f;
        [SerializeField]
        private Transform eye = null;
        [SerializeField]
        private LayerMask mask = (1 << 12) | (1 << 13);

        [Header("Debug Options")]
        [SerializeField]
        private bool debug = false;

        private Transform leftWayPoint, rightWayPoint;
        private Vector3 localScale;
        private bool isAttacking = false;
        private float timeSinceLastSeen = 0.0f;
        private bool playerSeen = false;

        protected Animator anim;
        protected float distanceToTurnaround = 100.0f;
        protected bool stopped = false;
        protected bool movingRight = true;
        protected Rigidbody2D rb;
        
        void Start()
        {
            anim = GetComponent<Animator> ();
            localScale = transform.localScale;
            rb = GetComponent<Rigidbody2D> ();
        }

        void Update()
        {
            timeSinceLastSeen += Time.deltaTime;
            // Update behaviour implemented in children
            UpdateBehaviour();
        }

        // Physics update
        void FixedUpdate() 
        {
            // Use sine wave to determine raycast angle
            Vector2 sinUp = (Vector2.up * viewHeight) * Mathf.Sin(Time.time * 20.0f);

            //Fire raycast, right if movingRight = true, left otherwise
            RaycastHit2D seen = Physics2D.Raycast(eye.position, 
                    movingRight ? Vector2.right + sinUp : -Vector2.right + sinUp, 
                    longViewDistance,
                    mask);

            // Draws debug rays for the long, medium, and short view distances
            if (debug) {
                Debug.DrawRay(eye.position, 
                        movingRight ? longViewDistance * (Vector2.right + sinUp) : longViewDistance * (-Vector2.right + sinUp), 
                        Color.red, 
                        0.5f);

                Debug.DrawRay(eye.position, 
                        movingRight ? mediumViewDistance * (Vector2.right + sinUp) : mediumViewDistance * (-Vector2.right + sinUp), 
                        Color.blue, 
                        0.5f);

                Debug.DrawRay(eye.position, 
                        movingRight ? shortViewDistance * (Vector2.right + sinUp) : shortViewDistance * (-Vector2.right + sinUp), 
                        Color.green, 
                        0.5f);
            }

            // If the raycast hit something on the viewable layers, this will not be null
            if (seen.collider != null) {
                if (seen.collider.tag == "Player") {
                    timeSinceLastSeen = 0.0f;
                    playerSeen = true;
                }
                // OnSeen implemented in children
                OnSeen(seen.collider, Vector2.Distance(transform.position, seen.collider.transform.position));
            }
            else {
                if (playerSeen && timeSinceLastSeen > 0.5f) {
                    // PlayerLost implemented in children
                    PlayerLost();
                    playerSeen = false;
                }
            }
        }

        // Implemented in children
        protected virtual void OnSeen(Collider2D other, float distance) {}

        // Implemented in children
        protected virtual void PlayerLost() {}

        // Implemented in children
        protected virtual void UpdateBehaviour() {}

        // Implemented in children
        protected virtual void Stop() {}

        // Implemented in children, with default destroy behaviour
        protected virtual void OnDeath()
        {
            Destroy(gameObject);
        }

        // Used to change facing, left or right
        public void Flip() {
            if (movingRight) {
                transform.localScale = localScale;
            }
            else {
                transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
            }
        }

        // Available to children only, used to move in faced direction. Negative values could be used to reverse enemies
        protected void Move(float moveSpeed) {
            if (movingRight) {
                rb.velocity = new Vector2(1 * moveSpeed, rb.velocity.y);
            }
            else {
                rb.velocity = new Vector2(-1 * moveSpeed, rb.velocity.y);
            }
        }

        // Standard damage function, calls OnDeath when health is low enough
        public void Hit(float damage) {
            health -= Mathf.Abs(damage);

            if (health <= 0) {
                // Implemented by children
                OnDeath();
            }
        }

        // Attribute used to determine move direction for non-children
        public bool MoveRight {
            set { movingRight = value; }
            get { return movingRight; }
        }

    }
}
