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
    private bool isAttacking = false;
    private float timeSinceLastSeen = 0.0f;
    private bool playerSeen = false;

    protected Animator anim;
    protected float distanceToTurnaround = 100.0f;
    protected bool stopped = false;
    protected bool movingRight = true;
    protected Rigidbody2D rb;
    
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

        RaycastHit2D seen = Physics2D.Raycast(eye.position, 
                movingRight ? Vector2.right + sinUp : -Vector2.right + sinUp, 
                viewDistance,
                mask);

        if (debug) {
            Debug.DrawRay(eye.position, 
                    movingRight ? viewDistance * (Vector2.right + sinUp) : viewDistance * (-Vector2.right + sinUp), 
                    Color.red, 
                    0.0f);
        }

        if (seen.collider != null) {
            if (seen.collider.tag == "Player") {
                timeSinceLastSeen = 0.0f;
                playerSeen = true;
            }
            OnSeen(seen.collider, Vector2.Distance(transform.position, seen.collider.transform.position));
        }
        else {
            if (playerSeen && timeSinceLastSeen > 0.5f) {
                PlayerLost();
                playerSeen = false;
            }
        }
    }

    protected virtual void OnSeen(Collider2D other, float distance) {}

    protected virtual void PlayerLost() {}

    protected virtual void UpdateBehaviour() {}

    protected virtual void Stop() {}

    protected virtual void OnDeath()
    {
        Destroy(gameObject);
    }

    public void Flip() {
        if (movingRight) {
            transform.localScale = localScale;
        }
        else {
            transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
        }
    }

    protected void Move(float moveSpeed) {
        if (movingRight) {
            rb.velocity = new Vector2(1 * moveSpeed, rb.velocity.y);
        }
        else {
            rb.velocity = new Vector2(-1 * moveSpeed, rb.velocity.y);
        }
    }

    public void Hit(float damage) {
        health -= damage;

        if (health <= 0) {
            OnDeath();
        }
    }

    public bool MoveRight {
        set { movingRight = value; }
        get { return movingRight; }
    }

}
