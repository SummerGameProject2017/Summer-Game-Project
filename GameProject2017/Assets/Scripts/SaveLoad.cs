using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoad : MonoBehaviour {

    public float health;
    public float collectibles;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 100, 100, 30), "Save"))
        {
            Save();
        }
        if (GUI.Button(new Rect(10, 200, 100, 30), "Load"))
        {
            Load();
        }
        GUI.Label(new Rect(10, 300, 100, 30), health.ToString());
        GUI.Label(new Rect(10, 400, 100, 30), collectibles.ToString());

    }
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SaveFile.dat");

        PlayerData data = new PlayerData();
        data.health = health;
        data.collectibles = collectibles;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/SaveFile.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SaveFile.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            health = data.health;
            collectibles = data.collectibles;
        }
    }
    
}
[Serializable]
class PlayerData
{
    public float health;
    public float collectibles;
}