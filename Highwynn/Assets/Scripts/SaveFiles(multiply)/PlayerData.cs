using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int level;
   // public int health;
   // public float[] position;

    public int levelOne;
    public int c_LOneOne;
    public int c_LOneTwo;
    public int c_LOneThree;
    public int c_LOneFour;

    public int levelTwo;
    public int c_LTwoOne;
    public int c_LTwoTwo;
    public int c_LTwoThree;
    public int c_LTwoFour;

    public int levelThree;
    public int c_LThreeOne;
    public int c_LThreeTwo;
    public int c_LThreeThree;
    public int c_LThreeFour;

    public int levelFour;
    public int c_LFourOne;
    public int c_LFourTwo;
    public int c_LFourThree;
    public int c_LFourFour;

    public int levelFive;
    public int c_LFiveOne;
    public int c_LFiveTwo;
    public int c_LFiveThree;
    public int c_LFiveFour;

    public int levelSix;
    public int c_LSixOne;
    public int c_LSixTwo;
    public int c_LSixThree;
    public int c_LSixFour;


    public PlayerData(Player player)
    {
        level = player.level;
      /*  health = player.health;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
        */
        
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

}
