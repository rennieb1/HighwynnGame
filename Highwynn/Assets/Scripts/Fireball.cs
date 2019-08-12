using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Highwynn {

    public class Fireball : MonoBehaviour
    {
        public float timeToLive = 1.0f;
        public float damage = 5.0f;
        private GameObject owner;

        // Update is called once per frame
        void Update()
        {
            if (timeToLive <= 0) {
                Destroy(gameObject);
            }

            timeToLive -= Time.deltaTime;
        }

            void OnCollisionEnter2D(Collision2D other) 
            {
                Debug.Log(other.gameObject.name);

                EnemyBehavior enemy = other.gameObject.GetComponent<EnemyBehavior>();
                if (enemy != null) 
                {
                    //Damage enemy
                    enemy.Hit(damage);
                }       

                if (other.gameObject != owner) 
                {
                    Destroy(gameObject);
                }
              }

          
        public GameObject SetOwner 
        {
            set { owner = value; }
        }
    }

}
