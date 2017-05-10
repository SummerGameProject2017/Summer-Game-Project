using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleton : MonoBehaviour {

    static MonoSingleton instance;




    public abstract void StartChild();



	// Use this for initialization
	void Start () {

        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


        StartChild();

	}
}
