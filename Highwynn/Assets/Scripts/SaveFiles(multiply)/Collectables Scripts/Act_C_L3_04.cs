using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Act_C_L3_04 : MonoBehaviour
{
    private int ThreeFour = 1;
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
            Player.Updatec_LThreeFour(ThreeFour);
            Destroy(gameObject);


        }
    }
}
