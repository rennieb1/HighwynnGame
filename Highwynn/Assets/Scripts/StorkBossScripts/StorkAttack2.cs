using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorkAttack2 : MonoBehaviour
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
                storkBoss.isClose = true;
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
            timer -= Time.deltaTime;
                onTrig = true;
            }
        if (other.CompareTag("BreakableGround"))
        {
            storkBoss.isBreakable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            timer = maxTime;
            storkBoss.isClose = false;
                onTrig = false;
        }
    }
}
