using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Collectables : MonoBehaviour, IChildEvents
{
    public float rotationSpeed = 1;
    public float amplitude = 0.5f;
    public float frequency = 1f;

    public GameObject par_pickup;
    public Text GearCountText;
    public Text RobotCountText;

    Vector3 PosOffset = new Vector3();
    Vector3 TempPos = new Vector3();

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
    void Start ()
    {
        //Store starting position of collectible object
        PosOffset = transform.position;

        // Calls Start method on Childs
        OnStart();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Rotate collectibles on y axis
        transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed, Space.Self);
   
        //Collectibles will float up and down
        TempPos = PosOffset;
        TempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = TempPos;

        // Calls Update method on Childs
        OnUpdate();
	}

}
