using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {


    public Vector3 offset = new Vector3(10, 10, 0);
    GameObject player;
    Camera camera;
    public bool followPlayer = true;
    // Use this for initialization
    void Start() {
        player = GameObject.FindWithTag("Player");
        transform.position = player.transform.position + offset;
        transform.LookAt(player.transform.position);
        camera = transform.FindChild("Min Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update() {
        float h = InputManager.GetAxis("Horizontal");
        float v = InputManager.GetAxis("Vertical");
        RaycastHit hit;

        Vector3 cameraCenter = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, camera.nearClipPlane));
        if (Physics.Raycast(cameraCenter, this.transform.forward, out hit, 1000))
        {
            Debug.Log(hit.transform.gameObject);
        }

    

            if (h != 0 || v != 0)
            {
                StartCoroutine(MoveCamera());
            }
        
        
        
            
        
    }

    IEnumerator MoveCamera()
    {
        yield return new WaitForSeconds(1);
        transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, Time.deltaTime * 2.0f);
    }
}
