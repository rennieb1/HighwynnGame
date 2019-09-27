using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Highwynn
{
    public class CapyDen : MonoBehaviour
    {
        [SerializeField]
        private int capyCount = 5;
        [SerializeField]
        private GameObject capy = null;
        private bool summoning = false;
        private CapyburraBehaviour summoner = null;
        private List<GameObject> activeCapys = null;

        void Start() {
            activeCapys = new List<GameObject>();
        }

        // Used to instantiate the summoner and start summoning their friends
        public void SummonFrens(CapyburraBehaviour summonerFren) {
            if (summoner == null) {
                summoner = summonerFren;
            }

            // If not currently summoning, to prevent capy calling this function many times a second
            if (!summoning) {
                summoning = true;
                StartCoroutine(SpawnWithDelay());
            }
        }

        // Used to recursively summon capys without calling the entry function SummonFrens
        private void SummonFrensQuietly() {
            StartCoroutine(SpawnWithDelay());
        }

        // Spawn one capy a second, and cancel collisions with each other capy
        private IEnumerator SpawnWithDelay() {
            // Create new capy and set it's den to this one
            GameObject newCapy = Instantiate(capy, transform.position, Quaternion.identity);
            newCapy.GetComponent<CapyburraBehaviour>().SetDen = this;

            // Get all of the new capy's colliders in all it's children
            Collider2D[] colliders = newCapy.GetComponentsInChildren<Collider2D>();
            // Get all of the summoning capy's colliders in all of it's children
            Collider2D[] summonerColliders = summoner.GetComponentsInChildren<Collider2D>();

            // Foreach of the new capy's colliders
            foreach (Collider2D col in colliders) {
                // Loop through each of the summoning capy's colliders
                foreach (Collider2D summonerCol in summonerColliders) {
                    // Disable collisions (so they can walk through eachother)
                    Physics2D.IgnoreCollision(col, summonerCol, true);
                }

                // Then do the same for each already spawned capy
                foreach (GameObject spawnedCapy in activeCapys) {
                    // Get already spawned capy colliders
                    Collider2D[] spawnedColliders = spawnedCapy.GetComponentsInChildren<Collider2D>();
                    // For each of these
                    foreach (Collider2D spawnedCol in spawnedColliders) {
                        // Disable collision with new capy colliders
                        Physics2D.IgnoreCollision(col, spawnedCol, true);
                    }
                }
            }

            // Add new capy to active capy list
            activeCapys.Add(newCapy);
            // Decrement available capys
            capyCount--;

            // Wait a second
            yield return new WaitForSeconds(1.0f);

            // If more capys to spawn
            if (capyCount > 0) {
                // Summon them
                SummonFrensQuietly();
            }
        }
    }
}
