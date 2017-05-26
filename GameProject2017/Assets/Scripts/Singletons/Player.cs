using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{

    public int lives;
    public int gear;
    public int robot;
    public short maxLives;

    private static Player instance;

    private Player()
    {
        lives = 3;
        gear = 0;
        robot = 0;
    }

    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Player();
            }
            return instance;
        }
    }

    public void CollectGear()
    {
       gear++;
    }

    public void CollectRobot()
    {
       robot++;
    }

    public void LoseLife()
    {
       lives--;
    }

    public void RestoreLife()
    {
       lives = maxLives;
    }

}
