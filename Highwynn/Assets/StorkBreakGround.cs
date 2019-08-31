﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorkBreakGround : StateMachineBehaviour
{
    public float timer;

    private StorkBossMainScript storkBoss;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        storkBoss = animator.GetComponent<StorkBossMainScript>();


    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        {
            if (storkBoss.isBreakable == true)
            {
                storkBoss.isBroken = true;
                animator.SetTrigger("Swark");
            }
            else
            {
                animator.SetTrigger("Idle");
            }
        }   
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
