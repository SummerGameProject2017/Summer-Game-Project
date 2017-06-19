using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoad : MonoSingleton<SaveLoad> //allows script to be activated when needed. Doesnt need to be attached to anything
{


    public static bool continueFromMain = false;
    static GameObject[] collectables;
    static GameObject[] dogs;
    static GameObject[] collectBot;
    static Player playerScript;
    // Use this for initialization


    public override void OnStart() {
        
            }

    // Update is called once per frame
    public override void OnUpdate() {
        
	}
  
    //create or open a save file and serialize the data being saved to binary then close the file
    public static void Save()   
    {
        Debug.Log("Save");
        collectables = GameObject.FindGameObjectsWithTag("Collectible");
        dogs = GameObject.FindGameObjectsWithTag("Enemy");
        collectBot = GameObject.FindGameObjectsWithTag("CollectBot");
        playerScript = Player.Instance;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SaveFile.dat");    //create a file or overwrite if exists to save data too
        
        PlayerData playerInfo = new PlayerData(playerScript.maxLives, playerScript.gear, playerScript.robot, GameObject.FindWithTag("Player").transform.position, GameObject.Find("PlayerCamera").transform.position, GameObject.Find("PlayerCamera").transform.eulerAngles);
        bf.Serialize(file, playerInfo);       //serialize and save the data
        
        file.Close();
    }

    //load the fila and desearialize from binary and give player information needed
    public static void Load()
    {
        if (continueFromMain == true)
        {
            continueFromMain = false;
            collectables = GameObject.FindGameObjectsWithTag("Collectible");
            dogs = GameObject.FindGameObjectsWithTag("Enemy");
            collectBot = GameObject.FindGameObjectsWithTag("CollectBot");
            }
        if (File.Exists(Application.persistentDataPath + "/SaveFile.dat"))
        {
            foreach (GameObject gear in collectables)
            {
                gear.SetActive(true);
            }
            foreach (GameObject enemy in dogs)
            {
                enemy.SetActive(true);
            }
            foreach (GameObject robot in collectBot)
            {
                robot.SetActive(true);
            }

            Debug.Log("Load");
            playerScript = Player.Instance;
            //if the file exists open and load
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SaveFile.dat", FileMode.Open);   
            PlayerData data = (PlayerData)bf.Deserialize(file);     //load the data from the class
            file.Close();
            
            playerScript.lives = data.maxLives;       //set health and collectibles to saved data
            playerScript.gear = data.collectibles;
            playerScript.robot = data.robotsCollected;
            GameObject.FindWithTag("Player").transform.position = new Vector3(data.positionX, data.positionY, data.positionZ);
            GameObject.Find("PlayerCamera").transform.position = new Vector3(data.cameraPositionX, data.cameraPositionY, data.cameraPositionZ);
            GameObject.Find("PlayerCamera").transform.rotation = Quaternion.Euler(data.cameraRotationX, data.cameraRotationY, data.cameraRotationZ);
        }
    }
}
[Serializable]      //serialize the data to be saved to file
public class PlayerData
{
    public int maxLives;        //player health value
    public int collectibles;      //collectibles gained value
    public int robotsCollected; //robots collected
    public float positionX;
    public float positionY;
    public float positionZ;
    public float cameraPositionX;
    public float cameraPositionY;
    public float cameraPositionZ;
    public float cameraRotationX;
    public float cameraRotationY;
    public float cameraRotationZ;
    public string[] collectedGears;

    public PlayerData(int _maxLives, int _collectibles, int _robotsCollected, Vector3 _position, Vector3 _cameraPosition, Vector3 _cameraRotation)
    {
        maxLives = _maxLives;
        collectibles = _collectibles;
        robotsCollected = _robotsCollected;
        positionX = _position.x;
        positionY = _position.y;
        positionZ = _position.z;
        cameraPositionX = _cameraPosition.x;
        cameraPositionY = _cameraPosition.y;
        cameraPositionZ = _cameraPosition.z;
        cameraRotationX = _cameraRotation.x;
        cameraRotationY = _cameraRotation.y;
        cameraRotationZ = _cameraRotation.z;


    }
}