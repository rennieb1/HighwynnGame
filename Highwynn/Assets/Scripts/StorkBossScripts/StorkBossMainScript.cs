using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorkBossMainScript : MonoBehaviour
{
    public int health;
    // public int damage;
    //  private float timeBtwDamage = 1.5f;
    public int damageTakenPerHit = 10;


    public bool isClose = false;// Close attack range(GREEN BOX) and the triger for the breakable floor
    public bool isMed = false; // Medium distance (Yellow BOX) 
    public bool isFar = false; // FAR distance (RED BOX) 
    public bool isRange = false; // Is RANGE (GREY BOX) Cause walking
    public bool isWithin = false; // is within in the close/med/FAR area
    public bool isBreakable = false; //Collider ON breakable floor
    public bool isBroken = false; //Will be used to active breakingPlaftorm Animation

    public Animator FloorBreaking;
    private Animator anim;
    public bool isDead;


    private Rigidbody2D rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();


}

    void Update()
    {
        if (isBroken == true)
        {
            FloorBreaking.Play("Breaking");
            isBroken = false;
        }
     
        if (health >= 1000)
        {
            health = 2000;
        }
        if (health <= 0)
        {
            if (isDead == false)
            {
                anim.Play("Dead");
                gameObject.layer = 10;
            }        
        }

        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Vines"))
        {
            health = 50;
            anim.Play("Swark");
        }
        if(other.gameObject.CompareTag("Fire"))
        {
            health = health - damageTakenPerHit;
            Destroy(other);
        }
        
    }
 




}
