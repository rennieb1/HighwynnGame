using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorkAttackFall : MonoBehaviour
{

    public Animator anim;


    private StorkBossMainScript storkBoss;

    void Start()
    {
        storkBoss = anim.GetComponent<StorkBossMainScript>();


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           storkBoss.fallenRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            storkBoss.fallenRange = false;
        }
    }
}