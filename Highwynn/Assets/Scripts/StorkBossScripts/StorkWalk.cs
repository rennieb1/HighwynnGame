using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorkWalk : StateMachineBehaviour
{
    public float timer;
    public float maxTime;
    private Transform playerPos;
    public float speed;

    private StorkBossMainScript storkBoss;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        storkBoss = animator.GetComponent<StorkBossMainScript>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            if (storkBoss.isWithin == true)
            {
                animator.SetTrigger("PrimeAttack");
                timer = maxTime;
            }
            if (storkBoss.isRange == true)
            {
                animator.SetTrigger("Walk");
                timer = maxTime;
                
            }
            
            else
            {
                animator.SetTrigger("Idle");
                timer = maxTime;
            }
        }
        else
        {
            timer -= Time.deltaTime;
        }
        Vector2 target = new Vector2(playerPos.position.x, animator.transform.position.y);
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, target, speed * Time.deltaTime);

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Walk");
    }
}
