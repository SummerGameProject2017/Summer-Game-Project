using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TireRolling : MonoBehaviour {

    float Timer = 0.0f;
    public float RespawnTime;
    public Transform Tire;
    Vector3 TireStartPos;
    Quaternion TireStartRot;

	// Use this for initialization
	void Start ()
    {
        TireStartPos = transform.position;
        TireStartRot = transform.rotation;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Timer += Time.deltaTime;

        if (Timer >= RespawnTime)
        {
            Instantiate(Tire, TireStartPos, TireStartRot);
            Destroy(gameObject);
            Timer = 0.0f;
        }
    }
}
