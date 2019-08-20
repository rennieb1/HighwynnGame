using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehav : StateMachineBehaviour
{
    public int Rand;
    private float timer;
    public float minTime;
    public float maxTime;
    

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      
        timer = Random.Range(minTime, maxTime);
        Rand = Random.Range(0  , 3);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       

        if (timer <= 0)
        {
            

            if (Rand == 0)
            {
                animator.SetTrigger("SpitFire");

            }
            if (Rand == 1)
            {
                animator.SetTrigger("TailAttack");

            }
            if (Rand == 2)
            {               
                animator.SetTrigger("Bite");

            }

        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }


}
