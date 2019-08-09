    
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Highwynn {

    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Shoot : MonoBehaviour {
        public Rigidbody2D projectile;
        public Transform projectileSpawnPoint;
        public float projectileVelocity;
        public float timeBetweenShots;
        private float timeBetweenShotsCounter;
        private bool canShoot;
        private PlatformerCharacter2D character;

        // Use this for initialization
        void Start () {
            canShoot = false;
            timeBetweenShotsCounter = timeBetweenShots;
            character = GetComponent<PlatformerCharacter2D>();
        }
    
        // Update is called once per frame
        void Update () {
            if (Input.GetKeyDown(KeyCode.F) && canShoot)
            {
                Rigidbody2D bulletInstance = Instantiate(
                        projectile, 
                        projectileSpawnPoint.position, 
                        Quaternion.Euler(new Vector3(0, 0, transform.localEulerAngles.z))
                ) as Rigidbody2D;

                bulletInstance.velocity = new Vector2(character.GetComponent<Rigidbody2D>().velocity.x, 0.0f);

                if (character.FacingRight) {
                    bulletInstance.GetComponent<Rigidbody2D>().AddForce(projectileSpawnPoint.right * projectileVelocity);     
                }
                else {
                    bulletInstance.GetComponent<Rigidbody2D>().AddForce(-projectileSpawnPoint.right * projectileVelocity);
                }

                canShoot = false;
            }
            if (!canShoot)
            {
                timeBetweenShotsCounter -= Time.deltaTime;
                if(timeBetweenShotsCounter <= 0)
                {
                    canShoot = true;
                    timeBetweenShotsCounter = timeBetweenShots;
                }
            }
        }
    }

}