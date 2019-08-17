using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthModifier : MonoBehaviour
{
    public int ModifyHealth;
    public Player Player;

    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.FindObjectOfType<Player>();

    }
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player.Update_Health(ModifyHealth);

            if (ModifyHealth >= 0)
            {
                Destroy(gameObject);
            }
       } 
    }
}
