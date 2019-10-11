using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMove : MonoBehaviour
{
    
    private float moveSpeed = 4.0f;
   
    private float attackRange = 10f;
    private float stopRange = 1;
    public Transform spiderUp;
    public Transform SpiderDown;

    private int value = 3;
    [SerializeField]
    private int rand;
    public float timer = 2;
    private float minTime = 0.5f;
    private float maxTime = 1.5f;

    public Animator anim;


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (timer <= 0)
        {
            rand = Random.Range(0, 3);
            timer = Random.Range(minTime, maxTime);
        }

        if (rand == 0)
        {

            if (Vector3.Distance(spiderUp.position, transform.position) < attackRange)//range
            {
                moveSpeed = Random.Range(2, 4);
                anim.Play("SpiderCrawlUp");
                timer -= Time.deltaTime;
                transform.position += transform.up * moveSpeed * Time.deltaTime; //Movespeed to attack player
            }
            if (Vector3.Distance(spiderUp.position, transform.position) > stopRange)
            {

            }
            else
            {
                timer = 0;
            }
        }
        if (rand == 1)
        {

            if (Vector3.Distance(SpiderDown.position, transform.position) < attackRange)//range
            {
                moveSpeed = Random.Range(2, 4);
                Vector3 direction = SpiderDown.position - transform.position;//How it knows to attack
                anim.Play("SpiderCrawlDown");
                timer -= Time.deltaTime;
                transform.position += transform.up * -moveSpeed * Time.deltaTime; //Movespeed to attack player
                
            }
            if (Vector3.Distance(SpiderDown.position, transform.position) > stopRange)
            {

            }
            else
            {
                timer = 0;
            }
        }
        if (rand == 2)
        {
            anim.Play("SpiderIdle");
            timer -= Time.deltaTime;
        }





    }
}