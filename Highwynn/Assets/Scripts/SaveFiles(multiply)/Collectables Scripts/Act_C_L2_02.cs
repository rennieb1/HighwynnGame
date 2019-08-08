using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Act_C_L2_02 : MonoBehaviour
{
    private int TwoTwo = 1;
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
            Player.Updatec_LTwoTwo(TwoTwo);
            Destroy(gameObject);


        }
    }
}
