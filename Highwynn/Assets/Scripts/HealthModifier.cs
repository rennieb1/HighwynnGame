using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthModifier : MonoBehaviour
{
    public int ModifyHealth;
    public Player Player;
    public float timer;
    public Rigidbody2D rb;
    [SerializeField]
    private float forcex = 0.0f;
    [SerializeField]
    private float forcey = 0.0f;
    // public Vector2 moveDirection;
    // public float launchForce;
    // public GameObject playercontroller;






    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.FindObjectOfType<Player>();
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
       






    }
    void Update()
    {
        timer -= Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            if (ModifyHealth >= 0)
            {
               Player.Update_Health(ModifyHealth);
               Destroy(gameObject);
               Debug.Log ("health " + Player.health);
               
            }
            if (ModifyHealth <= 0)
            {
                if (timer <= 0)
                {
                    
                    Player.Update_Health(ModifyHealth);
                    timer = 3f;
                    Debug.Log("health "+ Player.health);
                    //  moveDirection = rb.transform.position - rb.transform.position;
                    // rb.AddForce(moveDirection.normalized * launchForce);
                    // Vector2 direction = (transform.position - collision.transform.position).normalized;
                    //rb.AddForce(direction * forcex);
                    rb.velocity = new Vector2(forcex, forcey);



                }
            }
       } 
    }
}
