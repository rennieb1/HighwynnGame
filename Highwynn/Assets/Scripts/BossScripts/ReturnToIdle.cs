using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToIdle : StateMachineBehaviour
{

    public float timer;
    public float minTime;
    public float maxTime;

    private Transform playerPos;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        timer = Random.Range(minTime, maxTime);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            animator.SetTrigger("Idle");
            animator.ResetTrigger("TailAttack");
            animator.ResetTrigger("SpitFire");
            animator.ResetTrigger("Bite");
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

