using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehav : StateMachineBehaviour
{
    public int Rand;
    [SerializeField]
    private float timer;
    public float minTime;
    public float maxTime;
    

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      
        timer = Random.Range(minTime, maxTime);
        Rand = Random.Range(0  , 5);
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
                animator.SetTrigger("SpitFire");

            }

            if (Rand == 2)
            {
                animator.SetTrigger("TailAttack");

            }
            if (Rand == 3)
            {               
                animator.SetTrigger("Bite");

            }
            if (Rand == 4)
            {
                animator.SetTrigger("Hiss");
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
