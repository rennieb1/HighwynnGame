using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceWalk : MonoBehaviour
{

    public Animator anim;

    private StorkBossMainScript storkBoss;

    void Start()
    {       
        storkBoss = anim.GetComponent<StorkBossMainScript>();
    }
    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            storkBoss.isRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           
            storkBoss.isRange = false;
        }
    }
}
