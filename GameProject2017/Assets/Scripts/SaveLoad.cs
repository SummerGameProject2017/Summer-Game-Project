using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoad : MonoBehaviour {

    public float health;        //change to health and collectibles of actual player
    public float collectibles;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
  
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SaveFile.dat");    //create a file or overwrite if exists to save data too

        PlayerData data = new PlayerData();
        data.health = health;       //change the serialized file data for health and collectibles to what the current health and collectibles is
        data.collectibles = collectibles;
        
        bf.Serialize(file, data);       //serialize and save the data
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/SaveFile.dat"))
        {
            //if the file exists open and load
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SaveFile.dat", FileMode.Open);   
            PlayerData data = (PlayerData)bf.Deserialize(file);     //load the data from the class
            file.Close();

            health = data.health;       //set health and collectibles to saved data
            collectibles = data.collectibles;
        }
    }
    
}
[Serializable]      //serialize the data to be saved to file
class PlayerData
{
    public float health;        //player health value
    public float collectibles;      //collectibles gained value
}