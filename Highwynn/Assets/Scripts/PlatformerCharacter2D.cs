using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Highwynn
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround = ~10;                  // A mask determining what is ground to the character

        // Custom Variables
        [SerializeField] private bool ceilingCollide = false;
        private bool canAirJump = false;
        private bool hasAirJumped = false;
        [SerializeField] 
        private Wisp companion;
        public float airJumpForce = 200f;
        private List<Collider2D> currentColliders;
        private CircleCollider2D feet;
        private BoxCollider2D body;
        [SerializeField]
        private float companionTravelDistance = 5.0f;
        // Player Mana members
        public float mana = 100.0f;
        [SerializeField]
        private float manaRechargeDelay = 1.0f;
        public float manaRechargeTimer = 0.0f;
        [SerializeField]
        private float manaRechargeRate = 2.0f;
        [SerializeField]
        private Slider manaBar;
        [SerializeField]
        private Slider manaRequirement;
        private Color manaReqColour;
        private float manaReqRevealTimer = 0.0f;
        private bool manaReqReveal = false;

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.

        public GameObject fireRight, fireLef, /*gameOverText, restartButton,*/ hurtParticle;

        float t;

        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            feet = gameObject.GetComponent<CircleCollider2D>();
            body = gameObject.GetComponent<BoxCollider2D>();

            currentColliders = new List<Collider2D>();
            manaReqColour = manaRequirement.fillRect.GetComponent<Image>().color;
            manaReqColour.a = 0.0f;
            manaRequirement.fillRect.GetComponent<Image>().color = manaReqColour;
        }

        void Start()
        {
       //     gameOverText.SetActive(false);
        //    restartButton.SetActive(false);
        }

        private void Update() {
            // Mana recharge
            if (manaRechargeTimer < manaRechargeDelay) {
                manaRechargeTimer += Time.deltaTime;
            }
            else if (mana < 100.0f) {
                mana += Time.deltaTime * manaRechargeRate;
                mana = Mathf.Clamp(mana, 0.0f, 100.0f);
            }

            // Mana bar value
            manaBar.value = mana / 100.0f;

            // Mana Requirement bar indicator
            if (manaReqReveal) {
                
                t += Time.deltaTime;
                
                manaReqColour.a = Mathf.Abs(Mathf.Sin(t));
                manaRequirement.fillRect.GetComponent<Image>().color = manaReqColour;

                manaReqRevealTimer += Time.deltaTime;
                if (manaReqRevealTimer >= 6.0f) {
                    manaReqReveal = false;
                    manaReqRevealTimer = 0.0f;
                    manaReqColour.a = 0.0f;
                    manaRequirement.fillRect.GetComponent<Image>().color = manaReqColour;
                    manaRequirement.value = 0.0f;
                }
            }
        }

        private void FixedUpdate()
        {
            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // Also used to determine if air-jump is possible
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    m_Grounded = true;
                    canAirJump = false;
                    hasAirJumped = false;
                }
            }

            // If no ground colliders detected can air-jump
            if (colliders.Length == 0 && !hasAirJumped)
            {
                canAirJump = true;
            }

            // Check if the player hits ceiling
            if (ceilingCollide)
            {
                //This is still potentially useful for other effects/interactions, however the collider is best for handling collisions ;)

                // Collider2D[] ceilingColliders = Physics2D.OverlapCircleAll(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround);
                // foreach (Collider2D collider in ceilingColliders) {
                //     if (collider.gameObject != gameObject) {
                //         m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0.0f);
                //         m_Rigidbody2D.AddForce(new Vector2(0.0f, -300.0f));
                //     }
                // }
            }

            m_Anim.SetBool("Ground", m_Grounded);

            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
        }

        public void Move(float move, bool crouch, bool jump)
        {
            // If crouching, check to see if the character can stand up
            if (!crouch && m_Anim.GetBool("Crouch"))
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }

            // Set whether or not the character is crouching in the animator
            m_Anim.SetBool("Crouch", crouch);

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                move = (crouch ? move * m_CrouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
                m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }

            // Handle player air-jump. Must be checked before standard jump
            if (!m_Grounded && jump && canAirJump)
            {
                canAirJump = false;
                hasAirJumped = true;
                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0.0f);
                m_Rigidbody2D.AddForce(new Vector2(0f, airJumpForce));
            }

            // If the player should jump...
            if (m_Grounded && jump && m_Anim.GetBool("Ground"))
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                canAirJump = true;
                hasAirJumped = false;
                m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }
        }

        public void DropThrough() {
            // Loop through each collider, and for any that the player can fall through...
            foreach (Collider2D col in currentColliders) {
                if (col.tag == "dropThroughGround") {
                    // Fall through
                    StartCoroutine(Drop(col));
                }
            }
        }

        private IEnumerator Drop(Collider2D col) {
            // Disable collisions between player feet, body & dropDown collider
            Physics2D.IgnoreCollision(feet, col, true);
            Physics2D.IgnoreCollision(body, col, true);

            yield return new WaitForSeconds(1.0f);

            // Re-enable collisions between player feet, body & dropDown collider
            Physics2D.IgnoreCollision(feet, col, false);
            Physics2D.IgnoreCollision(body, col, false);
        }

        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            // Add current collider to list of colliders
            currentColliders.Add(other.collider);

            if (other.gameObject.tag.Equals("Enemy"))
            {
              //  gameOverText.SetActive(true);
              //  restartButton.SetActive(true);
                Instantiate(hurtParticle, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
                HighwynnGameManager.Instance().ResetPlayerToLastCheckpoint();
            }
        }

        void OnCollisionExit2D(Collision2D other) 
        {
            // Remove current collider from list
            currentColliders.Remove(other.collider);
        }

        public bool ReduceMana(float cost) {
            if (mana >= cost) {
                mana -= Mathf.Abs(cost);
                mana = Mathf.Clamp(mana, 0.0f, 100.0f);
                manaRechargeTimer = 0.0f;
                return true;
            }
            else {
                if (!manaReqReveal) {
                    manaRequirement.value = Mathf.Abs(cost) / 100.0f;
                    manaReqReveal = true;
                    t = 0.0f;
                }

                return false;
            }
        }

        public bool FacingRight {
            get { return m_FacingRight; }
        }

        public Wisp Companion {
            get { return companion; }
        }

        public float CompanionDistance {
            get { return companionTravelDistance; }
        }
    }
}