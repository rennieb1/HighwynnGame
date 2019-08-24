using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Highwynn 
{
    [RequireComponent(typeof (Light))]
    [RequireComponent(typeof (CompanionFollow))]
    public class Wisp : MonoBehaviour
    {   
        public float waitTime = 5.0f;
        public Color standard = Color.blue;
        public Color enemyAlert = Color.red;
        public Color saveAlert = Color.white;
        public Color lootAlert = Color.green;

        private Color currentColour;
        private Color lastColour;
        private float colourPercentage = 0.0f;

        private CompanionFollow follow;
        private Light glow;

        void Start()
        {
            follow = GetComponent<CompanionFollow>();
            glow = GetComponent<Light>();
            glow.color = standard;
            currentColour = standard;
            lastColour = standard;
        }

        void Update()
        {
            // Lerp colour if changed
            if (lastColour != currentColour) {
                glow.color = Color.Lerp(lastColour, currentColour, colourPercentage);

                if (colourPercentage < 1) {
                    colourPercentage += Time.deltaTime;
                }
                else {
                    lastColour = currentColour;
                }
            }
            else {
                colourPercentage = 0.0f;
            }
        }

        // Set the wisp's target, and determine it follows said target. Reset to player after time.
        public IEnumerator Scout(Vector3 endPosition) {

            Vector3 startPosition = transform.position;

            follow.followTarget = false;
            follow.targetPosition = endPosition;

            yield return new WaitForSeconds(waitTime);

            follow.followTarget = true;

        }

        public void ScoutController(Vector3 endPosition) {
            Vector3 startPosition = transform.position;

            follow.followTarget = false;
            follow.targetPosition = endPosition;
        }

        public bool IsFollow() {
            return follow.followTarget;
        }

        public void SetFollow(bool toFollow) {
            follow.followTarget = toFollow;
        }

        // Handle wisp colour changes (and other behaviours?) on trigger entry
        void OnTriggerEnter2D(Collider2D other) {

            switch (other.gameObject.tag) {
                case "Player":
                    currentColour = standard;
                    break;
                case "Enemy":
                    currentColour = enemyAlert;
                    break;
                case "SavePoint":
                    currentColour = saveAlert;
                    break;
                case "Loot":
                    currentColour = lootAlert;
                    break;
            }

        }
    }
}

