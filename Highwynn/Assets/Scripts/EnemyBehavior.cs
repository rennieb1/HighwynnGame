using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public int enemyHealth = 5;
    public float moveSpeed = 3f;
    Transform leftWayPoint, rightWayPoint;
    Vector3 localScale;
    bool movingRight = true;
    Rigidbody2D rb;
    public static bool isAttacking = false;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator> ();
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D> ();
        leftWayPoint = GameObject.Find ("LeftWayPoint").GetComponent<Transform> ();
        rightWayPoint = GameObject.Find ("RightWayPoint").GetComponent<Transform> ();
    }

    // Update is called once per frame
    void Update()
    {
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

    void OnTriggerEnter2D(Collider2D col) 
    {
              if (col.gameObject.name.Equals("Fire"))
        {
            enemyHealth -= 1;
        }
        if (enemyHealth < 1)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
