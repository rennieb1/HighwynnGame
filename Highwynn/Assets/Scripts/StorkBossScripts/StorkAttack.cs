using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorkAttack : MonoBehaviour
{
    public float timer;
    public Animator anim;
    public float maxTime;
    public float startTime;
    private StorkBossMainScript storkBoss;
    public bool onTrig = false;

    void Start()
    {
        storkBoss = anim.GetComponent<StorkBossMainScript>();
    }
    void Update()
    {
        if (onTrig == true)
        {
            if (timer <= startTime)
            {
                storkBoss.isMed = true;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            onTrig = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            timer = maxTime;
            storkBoss.isMed = false;
            onTrig = false;
        }
    }
}
