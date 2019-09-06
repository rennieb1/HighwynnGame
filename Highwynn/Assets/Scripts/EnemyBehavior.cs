using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float health = 5.0f;
    public float moveSpeed = 3.0f;
    public float viewDistance = 5.0f;
    [Range(0.1f, 1.5f)]
    public float viewHeight = 0.25f;
    public Transform eye;
    public LayerMask mask;
    public bool debug = false;
    Transform leftWayPoint, rightWayPoint;
    Vector3 localScale;
    bool movingRight = true;
    Rigidbody2D rb;
    public static bool isAttacking = false;
    Animator anim;
    float timeSinceLastSeen = 0.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator> ();
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D> ();
        //leftWayPoint = GameObject.Find ("LeftWayPoint").GetComponent<Transform> ();
        //rightWayPoint = GameObject.Find ("RightWayPoint").GetComponent<Transform> ();
    }

    void FixedUpdate() 
    {

        Vector2 sinUp = (Vector2.up * viewHeight) * Mathf.Sin(Time.time * 10.0f);

        RaycastHit2D seen = Physics2D.Raycast(eye.position, 
                movingRight ? Vector2.right + sinUp : -Vector2.right + sinUp, 
                viewDistance,
                mask);

        if (debug) {
            Debug.DrawRay(eye.position, 
                    movingRight ? viewDistance * (Vector2.right + sinUp) : viewDistance * (-Vector2.right + sinUp), 
                    Color.red, 
                    1.0f);
        }

        if (seen.collider != null) {
            if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "BoarIdle") {
                anim.SetTrigger("seePlayer");
            }
            anim.SetBool("lostPlayer", false);
            timeSinceLastSeen = 0.0f;
        }
        else {
            if (!anim.GetBool("lostPlayer") && timeSinceLastSeen > 0.5f) {
                anim.SetBool("lostPlayer", true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "BoarSeePlayer") {
            anim.ResetTrigger("seePlayer");
        }
        timeSinceLastSeen += Time.deltaTime;
        /*
        if (transform.position.x > rightWayPoint.position.x)
        {
            movingRight = false;
        }
        if (transform.position.x < leftWayPoint.position.x)
        {
            movingRight = true;
        }
        if (movingRight)
        {
            MoveRight();
        }
        else
        {
            MoveLeft();
        }

        if (isAttacking)
        {
            anim.SetBool ("isAttacking", true);
        }
        else
        {
            anim.SetBool ("isAttacking", false);
        }
        */
    }

    void MoveRight()
    {
        movingRight = true;
        //localScale.x = localScale;
        transform.localScale = localScale;
        if (!isAttacking)
        {
            rb.velocity = new Vector2 (1 * moveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    void MoveLeft()
    {
        movingRight = false;
        //localScale.x =;
        transform.localScale = new Vector2(-localScale.x, localScale.y);
        if (!isAttacking)
        {
            rb.velocity = new Vector2 (-1 * moveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        
    }

    /*
    void OnTriggerEnter2D(Collider2D col) 
    {
        Debug.Log("Fire Collision");

        if (col.gameObject.name.Equals("Fire"))
        {
            enemyHealth -= 1;
        }
        if (enemyHealth < 1)
        {
            Die();
        }
    }
    */

    public void Hit(float damage) {
        Debug.Log(name + " takes " + damage + " damage");

        health -= damage;

        if (health <= 0) {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
