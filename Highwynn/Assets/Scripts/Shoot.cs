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
        public float shootCost = 30.0f;
        private float timeBetweenShotsCounter;
        private bool canShoot;
        private PlatformerCharacter2D character;
        public bool addPlayerVelocity = true;
        private Animator m_Anim;
        private bool fire;


        // Use this for initialization
        void Start () {
            canShoot = false;
            timeBetweenShotsCounter = timeBetweenShots;
            character = GetComponent<PlatformerCharacter2D>();
            m_Anim = GetComponent<Animator>();
        }
    
        // Update is called once per frame
        void Update () {
            if ((Input.GetButtonDown("Fire1") || Input.GetAxisRaw("Fire1") != 0.0f) 
                && canShoot 
                && character.ReduceMana(shootCost))
            {
                Rigidbody2D bulletInstance = Instantiate(
                        projectile, 
                        projectileSpawnPoint.position, 
                        Quaternion.Euler(new Vector3(0, 0, transform.localEulerAngles.z))
                ) as Rigidbody2D;

                if (fire == false)
                {
                    fire = true;
                    m_Anim.Play("HeadAttack");
                    
                }
                

                bulletInstance.GetComponent<Projectile>().SetOwner = gameObject;

                // Determine if player's current velocity is added to the fireball
                if (addPlayerVelocity) {
                    // bulletInstance.velocity = new Vector2(character.GetComponent<Rigidbody2D>().velocity.x, 0.0f);
                    bulletInstance.velocity = character.GetComponent<Rigidbody2D>().velocity;
                }

                if (character.FacingRight) {
                    bulletInstance.GetComponent<Rigidbody2D>().AddForce(projectileSpawnPoint.right * projectileVelocity);
                    bulletInstance.GetComponent<Rigidbody2D>().SetRotation(180);
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
            if (Input.GetButtonUp("Fire1"))
            {
                m_Anim.Play("FinishAttack");
                fire = false;
            }

            if (Input.GetButtonDown("Fire2")) //|| Input.GetAxisRaw("Fire2") != 0.0f) 
            {
                character.TriggerFireCone(true);
                m_Anim.Play("HeadAttack");
            }
            if (Input.GetButtonUp("Fire2")) //|| Input.GetAxisRaw("Fire2") == 0.0f) 
            {
                character.TriggerFireCone(false);
                m_Anim.Play("FinishAttack");
            }
        }
    }

}