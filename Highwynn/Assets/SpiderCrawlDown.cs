using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderCrawlDown : StateMachineBehaviour
{
    public int Rand;
    private float timer;
    public float minTime;
    public float maxTime;
    public float speed;
    public GameObject SpiderUpLimit;
    private Transform down;

    


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        timer = Random.Range(minTime, maxTime);
        Rand = Random.Range(0, 3);
        down = SpiderUpLimit.GetComponent<Transform>();

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       

        if (timer <= 0)
        {

            if (Rand == 0)
            {
                animator.SetTrigger("Up");
            }
            if (Rand == 1)
            {
                animator.SetTrigger("Idle");
            }

        }
        else
        {
            timer -= Time.deltaTime;
            Vector2 target = new Vector2(down.position.y, animator.transform.position.x);
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, target, speed * Time.deltaTime);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


}
