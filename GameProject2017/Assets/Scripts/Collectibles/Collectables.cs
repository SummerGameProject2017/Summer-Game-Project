using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectables : MonoBehaviour, IChildEvents
{
    public float rotationSpeed = 1;

    /*
     *------------------------------------
       Pure virtual functions.
     *------------------------------------
     */
    public abstract void OnStart();
    public abstract void OnUpdate();
    //====================================


    /*
     *------------------------------------
       Virtual functions.
     *------------------------------------
     */
    public virtual void OnAwake() { }

    public virtual void OnFixedUpdate() { }
    //====================================



    // Use this for initialization
    void Start () {

        // Calls Start method on Childs
        OnStart();
	}
	
	// Update is called once per frame
	void Update () {

        

        transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed, Space.Self);

        // Calls Update method on Childs
        OnUpdate();
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
