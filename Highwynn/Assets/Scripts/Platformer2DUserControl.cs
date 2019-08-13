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

            // On main click
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

            if (CrossPlatformInputManager.GetAxisRaw("Vertical") < 0.0f) {
                if (!downAxisInUse) {
                    downAxisInUse = true;

                    if (buttonCooldown > 0 && buttonCount == 1) {
                        StartCoroutine(m_Character.DropThrough());
                    }
                    else {
                        buttonCooldown = 0.5f;
                        buttonCount += 1;
                    }

                }
            }

            if (CrossPlatformInputManager.GetAxisRaw("Vertical") >= 0.0f) {
                downAxisInUse = false;
            }

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
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            // Pass all parameters to the character control script.
            m_Character.Move(h, crouch, m_Jump);
            m_Jump = false;
        }
    }
}
