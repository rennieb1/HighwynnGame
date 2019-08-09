using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float timeToLive = 1.0f;
    public float damage = 5.0f;

    // Update is called once per frame
    void Update()
    {
        if (timeToLive <= 0) {
            Destroy(gameObject);
        }

        timeToLive -= Time.deltaTime;
    }
}
