using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Highwynn 
{
    [RequireComponent(typeof (Light))]
    [RequireComponent(typeof (CompanionFollow))]
    public class Wisp : MonoBehaviour
    {   
        public float speed = 5.0f;
        public Color standard = Color.blue;
        public Color enemyAlert = Color.red;
        public Color saveAlert = Color.white;
        public Color pickupAlert = Color.green;

        private CompanionFollow follow;


        // Start is called before the first frame update
        void Start()
        {
            follow = GetComponent<CompanionFollow>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public IEnumerator Scout(Vector3 endPosition) {

            Vector3 startPosition = transform.position;

            follow.followTarget = false;
            follow.targetPosition = endPosition;

            yield return new WaitForSeconds(10.0f);

            follow.followTarget = true;

        }
    }
}

