#pragma warning disable

//Tutorial help 5:20 https://www.youtube.com/watch?v=JC59tDg4tmo also hurt wisp effect https://www.youtube.com/watch?v=uf0ZXDKDWrc
using System.Collections;
using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.

        public GameObject gameOverText, restartButton, hurtParticle;
        
        public GameObject wisp1, wisp2, wisp3, wisp4, wisp5;
        public int playerHealth = 4;
        int playerLayer, enemyLayer;
        bool coroutineAllowed = true;
        Color color;
        Renderer rend;
        
        void Start() {
            {
                gameOverText.SetActive (false);
                restartButton.SetActive (false);

                playerLayer = this.gameObject.layer;
                enemyLayer = LayerMask.NameToLayer ("Enemy");
                Physics2D.IgnoreLayerCollision (playerLayer, enemyLayer, false);
                wisp1 = GameObject.Find ("Wisp1");
                wisp2 = GameObject.Find ("Wisp2");
                wisp3 = GameObject.Find ("Wisp3");
                wisp4 = GameObject.Find ("Wisp4");
                wisp5 = GameObject.Find ("Wisp5");
                wisp1.gameObject.SetActive (true);
                wisp2.gameObject.SetActive (true);
                wisp3.gameObject.SetActive (true);
                wisp4.gameObject.SetActive (true);
                wisp5.gameObject.SetActive (true);
                rend = GetComponent<Renderer> ();
                color = rend.material.color;

    
            }
        }

        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
        }


        private void FixedUpdate()
        {
            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;
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
                move = (crouch ? move*m_CrouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
                m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);

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
            // If the player should jump...
            if (m_Grounded && jump && m_Anim.GetBool("Ground"))
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }
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

        void OnCollisionEnter2D (Collision2D col) // at the moment collision with the enemy = death/gameover
        {
            if (col.gameObject.tag.Equals ("Enemy"))
            {
                playerHealth -= 1;
                switch (playerHealth)
                {
                case 3:
                    wisp5.gameObject.SetActive (false);
                    if (coroutineAllowed)
                    StartCoroutine ("Immortal");
                    break;
                case 2:
                    wisp4.gameObject.SetActive (false);
                    if (coroutineAllowed)
                    StartCoroutine ("Immortal");
                    break;
                case 1:
                    wisp3.gameObject.SetActive (false);
                    if (coroutineAllowed)
                    StartCoroutine ("Immortal");
                    break;
                case 0:
                    wisp2.gameObject.SetActive (false);
                    if (coroutineAllowed)
                    StartCoroutine ("Immortal");
                    break;
                }
                
                if (playerHealth < 1) 
                {
                    gameOverText.SetActive (true);
                    restartButton.SetActive (true);
                    //Instantiate (hurtParticle, transform.position, Quaternion.identity);
                    gameObject.SetActive (false);
                }
                
            }
        }

        IEnumerator Immortal()
        {
            coroutineAllowed = false;
            Physics2D.IgnoreLayerCollision (playerLayer, enemyLayer, true);
            color.a = 0.5f;
            rend.material.color = color;
            yield return new WaitForSeconds (3f);
            Physics2D.IgnoreLayerCollision (playerLayer, enemyLayer, false);
            color.a = 1f;
            rend.material.color = color;
            coroutineAllowed = true;
        }
    }
}
