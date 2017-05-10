using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {


    // This is called on the Awake
    public abstract void AwakeChild();
    // Use this for initialization
    public abstract void StartChild();
    // Update is called once per frame
    public abstract void UpdateChild();
    // Use it for Physics
    public abstract void FixedUpdateChild();



    // This is called on the Awake
    void Awake()
    {


        AwakeChild();
    }

	// Use this for initialization
	void Start () {


        StartChild();
    }
	
	// Update is called once per frame
	void Update () {


        UpdateChild();

    }

    // Use it for Physics
    void FixedUpdate()
    {


        FixedUpdateChild();
    }
}
