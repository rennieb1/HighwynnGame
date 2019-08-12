using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public int level = 1;
    public int health = 3;

  
    public int c_LOneOne;
    public int c_LOneTwo;
    public int c_LOneThree;
    public int c_LOneFour;

    
    public int c_LTwoOne;
    public int c_LTwoTwo;
    public int c_LTwoThree;
    public int c_LTwoFour;

    
    public int c_LThreeOne;
    public int c_LThreeTwo;
    public int c_LThreeThree;
    public int c_LThreeFour;

   
    public int c_LFourOne;
    public int c_LFourTwo;
    public int c_LFourThree;
    public int c_LFourFour;

   
    public int c_LFiveOne;
    public int c_LFiveTwo;
    public int c_LFiveThree;
    public int c_LFiveFour;

    
    public int c_LSixOne;
    public int c_LSixTwo;
    public int c_LSixThree;
    public int c_LSixFour;

    public GameObject C_L1_1;
    public GameObject C_L1_2;
    public GameObject C_L1_3;
    public GameObject C_L1_4;

    public GameObject LevelTwoButton;
    public GameObject C_L2_1;
    public GameObject C_L2_2;
    public GameObject C_L2_3;
    public GameObject C_L2_4;

    public GameObject LevelThreeButton;
    public GameObject C_L3_1;
    public GameObject C_L3_2;
    public GameObject C_L3_3;
    public GameObject C_L3_4;

    public GameObject LevelFourButton;
    public GameObject C_L4_1;
    public GameObject C_L4_2;
    public GameObject C_L4_3;
    public GameObject C_L4_4;

    public GameObject LevelFiveButton;
    public GameObject C_L5_1;
    public GameObject C_L5_2;
    public GameObject C_L5_3;
    public GameObject C_L5_4;

    public GameObject LevelSixButton;
    public GameObject C_L6_1;
    public GameObject C_L6_2;
    public GameObject C_L6_3;
    public GameObject C_L6_4;

    void Awake()
    {
        LoadPlayer();
    }

    void Update()
    {

        if (c_LOneOne >= 1)
        {
            C_L1_1.SetActive(true);
        }
        if (c_LOneTwo >= 1)
        {
            C_L1_2.SetActive(true);
        }
        if (c_LOneThree >= 1)
        {
            C_L1_3.SetActive(true);
        }
        if (c_LOneFour >= 1)
        {
            C_L1_4.SetActive(true);
        }
        if (c_LOneOne >= 1)
        {
            C_L1_1.SetActive(true);
        }

        
        if (level >= 2)
        {
            LevelTwoButton.SetActive(true);
        }
        if (c_LTwoOne >= 1)
        {
            C_L2_1.SetActive(true);
        }
        if (c_LTwoTwo >= 1)
        {
            C_L2_2.SetActive(true);
        }
        if (c_LTwoThree >= 1)
        {
            C_L2_3.SetActive(true);
        }
        if (c_LTwoFour >= 1)
        {
            C_L2_4.SetActive(true);
        }

        
        if (level >= 3)
        {
            LevelThreeButton.SetActive(true);
        }
        if (c_LThreeOne >= 1)
        {
            C_L3_1.SetActive(true);
        }
        if (c_LThreeTwo >= 1)
        {
            C_L3_2.SetActive(true);
        }
        if (c_LThreeThree >= 1)
        {
            C_L3_3.SetActive(true);
        }
        if (c_LThreeFour >= 1)
        {
            C_L3_4.SetActive(true);
        }


        if (level >= 4)
        {
            LevelFourButton.SetActive(true);
        }
        if (c_LFourOne >= 1)
        {
            C_L4_1.SetActive(true);
        }
        if (c_LFourTwo >= 1)
        {
            C_L4_2.SetActive(true);
        }
        if (c_LFourThree >= 1)
        {
            C_L4_3.SetActive(true);
        }
        if (c_LFourFour >= 1)
        {
            C_L4_4.SetActive(true);
        }


        if (level >= 5)
        {
            LevelFiveButton.SetActive(true);
        }
        if (c_LFiveOne >= 1)
        {
            C_L5_1.SetActive(true);
        }
        if (c_LFiveTwo >= 1)
        {
            C_L5_2.SetActive(true);
        }
        if (c_LFiveThree >= 1)
        {
            C_L5_3.SetActive(true);
        }
        if (c_LFiveFour >= 1)
        {
            C_L5_4.SetActive(true);
        }


        if (level >= 6)
        {
            LevelSixButton.SetActive(true);
        }
        if (c_LSixOne >= 1)
        {
            C_L6_1.SetActive(true);
        }
        if (c_LSixTwo >= 1)
        {
            C_L6_2.SetActive(true);
        }
        if (c_LSixThree >= 1)
        {
            C_L6_3.SetActive(true);
        }
        if (c_LSixFour >= 1)
        {
            C_L6_4.SetActive(true);

        }
        
    }

 
    public void SavePlayer()
    {
        SaveLoad.SavePlayer(this);
    }
    public void LoadPlayer()
    {
        PlayerData player = SaveLoad.LoadPlayer();

        level = player.level;
        health = player.health;

        Vector3 position;
        position.x = player.position[0];
        position.y = player.position[1];
        position.z = player.position[2];


        c_LOneOne = player.c_LOneOne;
        c_LOneTwo = player.c_LOneTwo;
        c_LOneThree = player.c_LOneThree;
        c_LOneFour = player.c_LOneFour;


        c_LTwoOne = player.c_LTwoOne;
        c_LTwoTwo = player.c_LTwoTwo;
        c_LTwoThree = player.c_LTwoThree;
        c_LTwoFour = player.c_LTwoFour;


        c_LThreeOne = player.c_LThreeOne;
        c_LThreeTwo = player.c_LThreeTwo;
        c_LThreeThree = player.c_LThreeThree;
        c_LThreeFour = player.c_LThreeFour;


        c_LFourOne = player.c_LFourOne;
        c_LFourTwo = player.c_LFourTwo;
        c_LFourThree = player.c_LFourThree;
        c_LFourFour = player.c_LFourFour;


        c_LFiveOne = player.c_LFiveOne;
        c_LFiveTwo = player.c_LFiveTwo;
        c_LFiveThree = player.c_LFiveThree;
        c_LFiveFour = player.c_LFiveFour;


        c_LSixOne = player.c_LSixOne;
        c_LSixTwo = player.c_LSixTwo;
        c_LSixThree = player.c_LSixThree;
        c_LSixFour = player.c_LSixFour;
    }
     public void NewLoadPlayer()
     {
            PlayerData player = SaveLoad.LoadPlayer();

            level = 1;
            health = 5;

            Vector3 position;
            position.x = player.position[0];
            position.y = player.position[1];
            position.z = player.position[2];


        c_LOneOne = 0;
        c_LOneTwo = 0;
        c_LOneThree = 0;
        c_LOneFour = 0;


        c_LTwoOne = 0;
        c_LTwoTwo = 0;
        c_LTwoThree = 0;
        c_LTwoFour = 0;


        c_LThreeOne = 0;
        c_LThreeTwo = 0;
        c_LThreeThree = 0;
        c_LThreeFour = 0;


        c_LFourOne = 0;
        c_LFourTwo = 0;
        c_LFourThree = 0;
        c_LFourFour = 0;


        c_LFiveOne = 0;
        c_LFiveTwo = 0;
        c_LFiveThree = 0;
        c_LFiveFour = 0;


        c_LSixOne = 0;
        c_LSixTwo = 0;
        c_LSixThree = 0;
        c_LSixFour = 0;


     }
    public void Updatec_LOneOne(int OneOne)
    {
        c_LOneOne = OneOne;
    }
    public void Updatec_LOneTwo(int OneTwo)
    {
        c_LOneTwo = OneTwo;
    }
    public void Updatec_LOneThree(int OneThree)
    {
        c_LOneThree = OneThree;
    }
    public void Updatec_LOneFour(int OneFour)
    {
        c_LOneFour = OneFour;
    }


    public void Updatec_LTwoOne(int TwoOne)
    {
        c_LTwoOne = TwoOne;
    }
    public void Updatec_LTwoTwo(int TwoTwo)
    {
        c_LTwoTwo = TwoTwo;
    }
    public void Updatec_LTwoThree(int TwoThree)
    {
        c_LTwoThree = TwoThree;
    }
    public void Updatec_LTwoFour(int TwoFour)
    {
        c_LTwoFour = TwoFour;
    }


    public void Updatec_LThreeOne(int ThreeOne)
    {
        c_LThreeOne = ThreeOne;
    }
    public void Updatec_LThreeTwo(int ThreeTwo)
    {
        c_LThreeTwo = ThreeTwo;
    }
    public void Updatec_LThreeThree(int ThreeThree)
    {
        c_LThreeThree = ThreeThree;
    }
    public void Updatec_LThreeFour(int ThreeFour)
    {
        c_LThreeFour = ThreeFour;
    }


    //Player Colliding with Finish

    public void Updatec_Lcount(int levelcount)
    {
        if(level >= levelcount)
        {
            level = level;
        }
        else 
        {
            level = levelcount;
        }
        
        
    }


}
