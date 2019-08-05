using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
     void OnTriggerEnter2D(Collider2D col) 
    {
        if (col.gameObject.name.Equals("Player"))
        {
            EnemyBehavior.isAttacking = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("Player"))
        {
            EnemyBehavior.isAttacking = false;
        }
    }
}
