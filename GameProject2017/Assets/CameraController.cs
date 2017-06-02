using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {


    public int offsetX;
    public int offsetY;
    public int offsetz;
    float angle;
    GameObject player;

    // Use this for initialization
    void Start() {
        player = GameObject.FindWithTag("Player");
        transform.position = new Vector3(player.transform.position.x + offsetX, player.transform.position.y + offsetY, player.transform.position.z + offsetz);
        transform.LookAt(player.transform.position);
    }

    // Update is called once per frame
    void Update() {
        if (InputManager.GetButton("Horizontal") || InputManager.GetButton("Vertical"))
        {
            StartCoroutine(MoveCamera());
        }
            
        
    }

    IEnumerator MoveCamera()
    {
        yield return new WaitForSeconds(1);
        transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x + offsetX, player.transform.position.y + offsetY, player.transform.position.z + offsetz), Time.deltaTime * 2.0f);
    }
}
