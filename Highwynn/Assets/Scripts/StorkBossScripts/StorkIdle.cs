using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorkIdle : StateMachineBehaviour
{
    public float timer;
    public float maxTime;
  

    private StorkBossMainScript storkBoss;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        storkBoss = animator.GetComponent<StorkBossMainScript>();


    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            if (storkBoss.isWithin == true)
            {
                animator.SetTrigger("PrimeAttack");
                
            }
            if (storkBoss.isRange == true)
            {
                animator.SetTrigger("Walk");
                
            }
            else
            {
                timer = maxTime;

            }
        }
        
        
        else
        {
            timer -= Time.deltaTime;
        }
        

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = maxTime;
    }
}
