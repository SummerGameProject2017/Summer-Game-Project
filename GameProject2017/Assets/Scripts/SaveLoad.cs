using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoad : MonoSingleton<SaveLoad> //allows script to be activated when needed. Doesnt need to be attached to anything
{
    
    public bool saveGame = false;
    static Player playerScript;
    // Use this for initialization
    public override void OnStart () {
		
	}

    // Update is called once per frame
    public override void OnUpdate () {
		
	}
  
    //create or open a save file and serialize the data being saved to binary then close the file
    public static void Save()   
    {
        playerScript = Player.Instance;
        Debug.Log("Saved Game");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SaveFile.dat");    //create a file or overwrite if exists to save data too

        PlayerData player = new PlayerData(playerScript.lives, playerScript.gear, playerScript.robot);
        bf.Serialize(file, player);       //serialize and save the data
        file.Close();
    }

    //load the fila and desearialize from binary and give player information needed
    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/SaveFile.dat"))
        {
            playerScript = Player.Instance;
            //if the file exists open and load
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SaveFile.dat", FileMode.Open);   
            PlayerData data = (PlayerData)bf.Deserialize(file);     //load the data from the class
            file.Close();

            playerScript.lives = data.health;       //set health and collectibles to saved data
            playerScript.gear = data.collectibles;
            playerScript.robot = data.robotsCollected;
        }
    }
}
[Serializable]      //serialize the data to be saved to file
public class PlayerData
{
    public int health;        //player health value
    public int collectibles;      //collectibles gained value
    public int robotsCollected; //robots collected

    public PlayerData(int _health, int _collectibles, int _robotsCollected)
    {
        health = _health;
        collectibles = _collectibles;
        robotsCollected = _robotsCollected;
    }
}