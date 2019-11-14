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
        private Wisp companion = null;
        public float airJumpForce = 200f;
        private List<Collider2D> currentColliders;
        private CircleCollider2D feet;
        [SerializeField]
        private BoxCollider2D body = null;
        [SerializeField]
        private float companionTravelDistance = 5.0f;
        // Player Mana members
        private float mana = 100.0f;
        [SerializeField]
        private float manaRechargeDelay = 1.0f;
        private float manaRechargeTimer = 0.0f;
        [SerializeField]
        private float manaRechargeRate = 2.0f;
        [SerializeField]
        private Slider manaBar = null;
        [SerializeField]
        private Slider manaRequirement = null;
        private Color manaReqColour;
        private float manaReqRevealTimer = 0.0f;
        private bool manaReqReveal = false;
        private float manaReqColourFadeTimer;
        [SerializeField]
        private GameObject fireCone = null;
        private bool fireConeTrigger = false;
        private float fireConeTimer = 0.0f;
        [SerializeField]
        private float fireConeCostPerSecond = 4.0f;
        private float fireConeTimeToCost = 0.05f;

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.

        public Vector2 normal;//Byron
        public Rigidbody2D rb;//Byroon
        public Vector2 upright;
        public float xDragonValue;
        public float yDragonValue;

        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            feet = gameObject.GetComponent<CircleCollider2D>();
            // body = gameObject.GetComponent<BoxCollider2D>();
            Debug.Log(feet);
            currentColliders = new List<Collider2D>();
            manaReqColour = manaRequirement.fillRect.GetComponent<Image>().color;
            manaReqColour.a = 0.0f;
            manaRequirement.fillRect.GetComponent<Image>().color = manaReqColour;
            rb = GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
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

            // The values i work from  the x n y can probz get removed from here as they where just more for understanding purposes.-----------------
          
            transform.up = normal;
            // Debug.DrawRay(transform.position, normal * 100, Color.red);
            xDragonValue = normal.x;
            yDragonValue = normal.y;
        
         

            // Mana Requirement bar indicator
            if (manaReqReveal) {
                
                manaReqColourFadeTimer += Time.deltaTime;
                
                manaReqColour.a = Mathf.Abs(Mathf.Sin(manaReqColourFadeTimer));
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

            if (fireConeTrigger) {
                fireConeTimer += Time.deltaTime;
                if (fireConeTimer >= fireConeTimeToCost) {
                    ReduceMana(fireConeCostPerSecond / (1 / fireConeTimeToCost));

                    fireConeTimer = 0.0f;
                }

                if (mana > (fireConeCostPerSecond / (1 / fireConeTimeToCost))) {
                    fireCone.SetActive(true);
                }
                else {
                    fireCone.SetActive(false);
                }
            }
            else {
                if (fireCone.activeSelf) {
                    fireCone.SetActive(false);
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

        public void TriggerFireCone(bool trigger) {
            fireConeTrigger = trigger;
        }

        public void Move(float move, bool crouch, bool jump)
        {
            // If crouching, check to see if the character can stand up
            if (!crouch && m_Anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "CrouchWalk")
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }
                        if (!crouch && m_Anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Crouchidle")
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
                m_Anim.Play("Jumping");//Added By Byron
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
                m_Anim.Play("Jumping");//Added By Byron
                normal = upright; // When the player jumps corrects the upright position of the player
            }
        }

        public void DropThrough() {
            // Loop through each collider, and for any that the player can fall through...
            foreach (Collider2D col in currentColliders) {
                if (col != null) {
                    if (col.tag == "dropThroughGround") {
                        // Fall through
                        StartCoroutine(Drop(col));
                    }
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
            // Used for drop down
            currentColliders.Add(other.collider);
            
            if (m_Grounded == true && other.gameObject.tag == "ground") //Byron Ground Shifting Body Check
            {
                
                if (xDragonValue <= 0.7f && xDragonValue >= -0.7f && yDragonValue >= 0.6f)
                {
                    normal = other.contacts[0].normal;
                }
                if (xDragonValue >= 0.7f && yDragonValue >= 0.6f)
                {                    
                    xDragonValue = 0.69f;
                    yDragonValue = other.contacts[0].normal.y;
                    normal = new Vector2(xDragonValue, yDragonValue);
                }
                if (xDragonValue >= 0.7f && yDragonValue <= 0.6f)
                {
                    xDragonValue = 0.69f;
                    yDragonValue = 0.61f;
                    normal = new Vector2(xDragonValue, yDragonValue);
                }

                if (xDragonValue <= -0.7f && yDragonValue >= 0.6f)
                {
                    xDragonValue = -0.69f;
                    yDragonValue = other.contacts[0].normal.y;
                    normal = new Vector2(xDragonValue, yDragonValue);
                }
                if (xDragonValue <= -0.7f && yDragonValue <= 0.6f)
                {
                    xDragonValue = -0.69f;
                    yDragonValue = 0.61f;
                    normal = new Vector2(xDragonValue, yDragonValue);
                }

            }
            else
            {
                normal = upright;
            }


            // if (other.gameObject.tag.Equals("Enemy"))
            // {
            //   //  gameOverText.SetActive(true);
            //   //  restartButton.SetActive(true);

            //     gameObject.SetActive(false);
            //     HighwynnGameManager.Instance().ResetPlayerToLastCheckpoint();
            // }
        }
       void OnCollisionStay2D(Collision2D collision)
        {
          //  xDragonValue = 0f;
           // yDragonValue = 0.6f;
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
                //if (!manaReqReveal) {
                    manaRequirement.value = Mathf.Abs(cost) / 100.0f;
                    manaReqReveal = true;
                    manaReqColourFadeTimer = 0.0f;
                //}

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