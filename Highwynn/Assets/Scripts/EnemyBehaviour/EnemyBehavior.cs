using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    private float health = 5.0f;
    [SerializeField]
    private float viewDistance = 5.0f;
    [SerializeField]
    [Range(0.1f, 1.5f)]
    private float viewHeight = 0.25f;
    [SerializeField]
    private Transform eye = null;
    [SerializeField]
    private LayerMask mask = (1 << 12) | (1 << 13);
    [SerializeField]
    private bool debug = false;

    private Transform leftWayPoint, rightWayPoint;
    private Vector3 localScale;
    protected bool movingRight = true;
    protected Rigidbody2D rb;
    private bool isAttacking = false;
    private float timeSinceLastSeen = 0.0f;
    private bool playerSeen = false;

    protected Animator anim;
    protected RaycastHit2D seen;
    protected float distanceToTurnaround = 100.0f;
    protected bool stopped = false;
    
    void Start()
    {
        anim = GetComponent<Animator> ();
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D> ();
    }

    void Update()
    {
        timeSinceLastSeen += Time.deltaTime;
        UpdateBehaviour();
    }

    void FixedUpdate() 
    {

        Vector2 sinUp = (Vector2.up * viewHeight) * Mathf.Sin(Time.time * 20.0f);

        seen = Physics2D.Raycast(eye.position, 
                movingRight ? Vector2.right + sinUp : -Vector2.right + sinUp, 
                viewDistance,
                mask);

        if (debug) {
            Debug.DrawRay(eye.position, 
                    movingRight ? viewDistance * (Vector2.right + sinUp) : viewDistance * (-Vector2.right + sinUp), 
                    Color.red, 
                    0.5f);
        }

        if (seen.collider != null) {
            if (seen.collider.tag == "Player") {
                PlayerSeen();
                timeSinceLastSeen = 0.0f;
                playerSeen = true;
            }
            else {
                if (!stopped) {
                    distanceToTurnaround = Vector2.Distance(transform.position, seen.collider.transform.position);
                }
            }
        }
        else {
            if (playerSeen && timeSinceLastSeen > 0.5f) {
                PlayerLost();
                playerSeen = false;
            }
        }
    }

    protected virtual void PlayerSeen() {
        Debug.Log("Default PlayerSeen()");
    }

    protected virtual void PlayerLost() {
        Debug.Log("Default PlayerLost()");
    }

    protected virtual void UpdateBehaviour() {
        Debug.Log("Default UpdateBehaviour()");
    }

    public void Flip() {
        if (movingRight) {
            transform.localScale = localScale;
        }
        else {
            transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
        }
    }

    public virtual void Stop() {
        Debug.Log("Default Stop()");
    }

    /*
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
    */

    protected void Move(float moveSpeed) {
        if (movingRight) {
            rb.velocity = new Vector2(1 * moveSpeed, rb.velocity.y);
        }
        else {
            rb.velocity = new Vector2(-1 * moveSpeed, rb.velocity.y);
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
        health -= damage;

        if (health <= 0) {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    public bool MoveRight {
        set { movingRight = value; }
        get { return movingRight; }
    }

}
