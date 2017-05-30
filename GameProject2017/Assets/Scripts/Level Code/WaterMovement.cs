using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMovement : MonoBehaviour {

    public float scroolX = 0.5f;
    public float scrollY = 0.5f;



	void Update () {
        float offsetX = Time.time * scroolX;
        float offsetY = Time.time * scrollY;
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offsetX, offsetY);

	}
}
