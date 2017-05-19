using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceGate : MonoBehaviour {
    //Antonio Quesnel code

    public Object lAeftGate;
    public Object rightGate;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (lAeftGate)
        {
           transform.Rotate(new Vector3(0, Time.deltaTime * 300, 0));
        }

        if (rightGate)
        {
            transform.Rotate(new Vector3(0, Time.deltaTime * 100, 0));
        }


    }
}
