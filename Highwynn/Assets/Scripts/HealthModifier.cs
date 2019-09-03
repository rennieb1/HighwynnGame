using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthModifier : MonoBehaviour
{
    public int ModifyHealth;
    public Player Player;
    public float timer;
  //  public float forcex;
   // public float forcey;

    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.FindObjectOfType<Player>();
        
         

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
            }
            if (ModifyHealth <= 0)
            {
                if (timer <= 0)
                {
                    Player.Update_Health(ModifyHealth);
                    timer = 3f;
              //      collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (forcey, forcex);


                }
            }
       } 
    }
}
