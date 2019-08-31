using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorkAttackIdle : StateMachineBehaviour
{
    public float timer;
    public float maxTime;
    public float timerReset;
    public float timerOverLoadValue;
    

    private StorkBossMainScript storkBoss;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        storkBoss = animator.GetComponent<StorkBossMainScript>();


    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            if (storkBoss.isClose == true)
            {
                animator.SetTrigger("Attack2");
                timer = maxTime;
            }
            if (storkBoss.isMed == true)
            {
                animator.SetTrigger("Attack");
                timer = maxTime;
            }
            if (storkBoss.isFar == true)
            {
                animator.SetTrigger("Attack3");
                timer = maxTime;
            }
            
            else 
            {
                timer = maxTime;
                if (timerReset <= 0)
                {
                    animator.SetTrigger("Attack3");
                    timer = maxTime;
                }
            }
        }
        else
        {
            timer -= Time.deltaTime;
            timerReset -= Time.deltaTime;
        }

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timerReset = timerOverLoadValue;
        timer = maxTime;
        animator.ResetTrigger("Attack3");
        animator.ResetTrigger("Attack2");
        animator.ResetTrigger("Attack");
    }
}
