﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    public GameObject Winlevel;
    public int LevelCount = 1;
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
            Player.Updatec_Lcount(LevelCount);
            Player.SavePlayer();
            Winlevel.SetActive(true);
            Time.timeScale = 0;



        }
    }
}
