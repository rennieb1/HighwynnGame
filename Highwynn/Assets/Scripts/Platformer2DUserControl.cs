using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Highwynn
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
        // Custom variables
        private Coroutine scoutRoutine = null; // Hold reference to started coroutine so it can be cancelled
        private bool downAxisInUse = false;
        private float buttonCooldown = 0.5f;
        private int buttonCount = 0;

        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

            // Companion scouting direction PC
            if (Input.GetMouseButtonDown(0)) {
                // Get mouse position
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // Start coroutine which moves the wisp, pass in mouse position
                if (scoutRoutine == null) {
                    scoutRoutine = StartCoroutine(m_Character.Companion.Scout(new Vector2(mousePosition.x, mousePosition.y)));
                }
                else {
                    StopCoroutine(scoutRoutine);
                    scoutRoutine = StartCoroutine(m_Character.Companion.Scout(new Vector2(mousePosition.x, mousePosition.y)));
                }
            }

            // Companion scouting direction for controllers
            // Get right-stick vertical & horizontal values
            float vRight = CrossPlatformInputManager.GetAxis("VerticalRight");
            float hRight = CrossPlatformInputManager.GetAxis("HorizontalRight");
            if (vRight != 0.0f || hRight != 0.0f) 
            {
                // Get player position
                // Add distance multiplied by axis input to the x or y values of position to calc new position
                Vector3 targetPosition = m_Character.gameObject.transform.position;
                targetPosition.x += m_Character.CompanionDistance * CrossPlatformInputManager.GetAxis("HorizontalRight");
                targetPosition.y += m_Character.CompanionDistance * CrossPlatformInputManager.GetAxis("VerticalRight");

                // Non-Coroutine scout function
                m_Character.Companion.ScoutController(new Vector2(targetPosition.x, targetPosition.y));
            }

            // Reset companion follow when no axis input
            if (vRight == 0.0f && hRight == 0.0f && scoutRoutine == null) 
            {
                // If companion isn't following, make it follow
                if (!m_Character.Companion.IsFollow()) {
                    m_Character.Companion.SetFollow(true);
                }
            }

            // Determine if player is trying to "drop-down"
            float v = CrossPlatformInputManager.GetAxisRaw("Vertical");
            if (v < 0.0f) {
                if (!downAxisInUse) {
                    downAxisInUse = true;

                    // If cooldown hasn't finished, and button count is high enough allow drop-through
                    // Otherwise reset cooldown and increment button count
                    if (buttonCooldown > 0 && buttonCount == 1) {
                        m_Character.DropThrough();
                    }
                    else {
                        buttonCooldown = 0.5f;
                        buttonCount += 1;
                    }

                }
            }

            // For "drop-down"
            if (v >= 0.0f) {
                downAxisInUse = false;
            }

            // Count "drop-down" cooldown timer. Reset button count if cooldown runs out
            if (buttonCooldown > 0) {
                buttonCooldown -= Time.deltaTime;
            }
            else {
                buttonCount = 0;
                buttonCooldown = 0.0f;
            }
        }


        private void FixedUpdate()
        {
            // Read the inputs.
            bool crouch = Input.GetButton("Crouch");
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            // Pass all parameters to the character control script.
            m_Character.Move(h, crouch, m_Jump);
            m_Jump = false;
        }
    }
}
