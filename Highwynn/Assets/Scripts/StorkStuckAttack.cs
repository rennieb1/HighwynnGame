using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorkStuckAttack : StateMachineBehaviour
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
            if (storkBoss.fallenRange == true)
            {
                animator.SetTrigger("StuckAttack");
                timer = maxTime;
            }
        
            
            else 
            {
                    timer = maxTime;
                
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

        animator.ResetTrigger("StuckAttack");
    }
}
