using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{

    public float timeToLive = 4.0f;
   

    // Update is called once per frame
    void Update()
    {
        {
            if (timeToLive <= 0)
            {
                Destroy(gameObject);
            }

            timeToLive -= Time.deltaTime;
        }
    }
}
