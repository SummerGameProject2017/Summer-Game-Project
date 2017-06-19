using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TireRolling : MonoBehaviour {

    float Timer = 0.0f;
    public float RespawnTime;
    public Transform Tire;
    Vector3 TireStartPos;
    Quaternion TireStartRot;
    GameObject player;
    public bool canHitPlayer = true;

	// Use this for initialization
	void Start ()
    {
        TireStartPos = transform.position;
        TireStartRot = transform.rotation;
        player = GameObject.FindWithTag("Player");
    }
	
	// Update is called once per frame
	void Update ()
    {
        Timer += Time.deltaTime;

        if (Timer >= RespawnTime)
        {
            if (Player.Instance.lives > 0)
            {
                canHitPlayer = true;
            }
            Instantiate(Tire, TireStartPos, TireStartRot);
            Destroy(gameObject);
            Timer = 0.0f;
        }


       
            }
}
