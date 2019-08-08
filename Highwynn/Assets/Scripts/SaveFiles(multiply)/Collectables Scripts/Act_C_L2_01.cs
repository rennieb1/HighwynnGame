using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Act_C_L2_01 : MonoBehaviour
{
    private int TwoOne = 1;
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
            Player.Updatec_LTwoOne(TwoOne);
            Destroy(gameObject);


        }
    }
}


