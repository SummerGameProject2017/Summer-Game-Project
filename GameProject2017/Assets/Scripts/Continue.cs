using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continue : MonoBehaviour
{
    public GameObject other;
	// Use this for initialization
	void Start ()
    {
        SaveLoad.Load();
        Destroy(other);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
