using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Highwynn
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class EnemyShoot : MonoBehaviour
    {
        public float shootDelay = 1.0f;
        public float shootSpeed = 1.0f;
        public GameObject projectile;
        public BoxCollider2D shootTrigger;

        private Collider2D target = null;
        private float shootTimer = 0.0f;

        // Start is called before the first frame update
        void Start()
        {
            shootTrigger.isTrigger = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (target != null) {
                if (shootTimer <= 0.0f) {
                    GameObject p = Instantiate(projectile, transform.position, Quaternion.identity);
                    p.GetComponent<Rigidbody2D>().AddForce(Vector3.Normalize((target.gameObject.transform.position - p.gameObject.transform.position)) * shootSpeed);

                    shootTimer = shootDelay;
                }
            }

            if (shootTimer > 0.0f) {
                shootTimer -= Time.deltaTime;
            }
        }

        void OnTriggerEnter2D(Collider2D other) {
            if (other.tag == "Player") {
                target = other;
            }
        }

        void OnTriggerExit2D(Collider2D other) {
            if (other == target) {
                target = null;
            }
        }
    }
}
