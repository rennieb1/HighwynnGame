using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        switch (other.tag) {
            case "Vines":
                Destroy(other.gameObject);
                break;
            case "Enemy":
                Debug.Log("Damage Enemy");
                break;
        }
    }
}
