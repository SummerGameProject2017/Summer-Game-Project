using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraView : MonoBehaviour
{


    //public GameObject player;


    //[Header("Camera Stick"), Range(10, 30)]
    //public float angle;
    //[Range(1.0f, 30.0f)]
    //public byte size;

    //private short actualHeight;
    //private byte actualSize;

    //Vector3 d;
    //Vector3 nd;


    //Vector3 oldPosition;

    //Vector3 playerOldposition;


    //// Use this for initialization
    //void Start()
    //{

    //    if (player == null)
    //    {
    //        player = GameObject.FindGameObjectWithTag("Player");

    //    }

    //    oldPosition = gameObject.transform.position;
    //    playerOldposition = player.transform.position;

    //    //d = gameObject.transform.position - player.transform.position;
    //    //nd = d.normalized;

    //    //size = (byte)d.magnitude;

    //}


    //float horizontal;
    //float vertical;


    //// Update is called once per frame
    //void Update()
    //{

    //    //newd = nd * size;


    //    gameObject.transform.position = oldPosition + (player.transform.position - playerOldposition);


    //    AdjustCamera();

    //    horizontal = InputManager.GetAxis("Camera_X", false);
    //   // vertical = InputManager.GetAxis("Camera_Y", false);

    //    if (horizontal != 0.0f || vertical != 0.0f)
    //    {

    //        angle+= vertical;

    //        angle = Mathf.Clamp(angle, 10.0f, 30.0f);

    //        gameObject.transform.RotateAround(player.transform.position, Vector3.up, horizontal);

    //        AdjustCamera();

    //        horizontal = 0.0f;
    //        vertical = 0.0f;
    //    }
    //    else
    //    {

    //    }

    //    oldPosition = gameObject.transform.position;
    //    playerOldposition = player.transform.position;

    //    //Quaternion q = new Quaternion(0, 0, Mathf.Sin(angle / 2) * Mathf.Rad2Deg, Mathf.Cos(angle / 2) * Mathf.Rad2Deg); 



    //}

    //private void OnGUI()
    //{


    //    //Vector3 dir = player.transform.position - gameObject.transform.position;
    //    //GUI.Label(rect, (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg).ToString());
    //    Rect rect = new Rect(0, 0, 300, 100);
    //    //GUI.Label(rect, "Distance = " + d.ToString() + " Mag = " + d.magnitude);
    //    GUI.Label(rect, InputManager.GetAxis("Camera_Y", false).ToString());

    //    Rect rect2 = new Rect(0, 15, 300, 100);
    //    GUI.Label(rect2, "Unit " + nd.ToString());

    //    //Rect rect3 = new Rect(0, 30, 300, 100);
    //    //GUI.Label(rect3, "New Distance = " + newd.ToString());



    //    Rect rect4 = new Rect(0, 60, 300, 100);
    //    GUI.Label(rect4, "Angle = " + Vector3.Angle(gameObject.transform.position, player.transform.position).ToString());

    //    Rect rect5 = new Rect(0, 75, 300, 100);
    //    GUI.Label(rect5, "Rotated = " + horizontal.ToString());

    //}



    //private void AdjustCamera()
    //{

    //    //Vector3 cameraGroundPos = gameObject.transform.position;
    //    //Vector3 playerGroundPos = player.transform.position;

    //    //float groundDistance;
    //    //float angle;


    //    //cameraGroundPos.y = playerGroundPos.y = 0;

    //    //groundDistance = (cameraGroundPos - playerGroundPos).magnitude;


    //    //angle = Mathf.Atan2(gameObject.transform.position.y, groundDistance) * Mathf.Rad2Deg;
    //    //angle = Mathf.Clamp(angle, 10, 70);

    //    //gameObject.transform.LookAt(player.transform);







    //    Vector3 cameraPos = gameObject.transform.position;

    //    /**
    //     *   C 
    //     *    |\
    //     *    | \
    //     *    |  \ h
    //     * C.y|   \
    //     *    |    \
    //     *    |_____\ P
    //     */
    //    float hipotenusa = (cameraPos - player.transform.position).magnitude;

    //    float angleRad = angle * Mathf.Deg2Rad;

    //    cameraPos.y = Mathf.Sin(angleRad) * hipotenusa;

    //    gameObject.transform.position = cameraPos;


    //    Vector3 distanceVector = (cameraPos - player.transform.position).normalized;

    //    gameObject.transform.position = (distanceVector * size) + player.transform.position;


    //    gameObject.transform.LookAt(player.transform);




    //    //float angleRad = angle * Mathf.Deg2Rad;

    //    float distance = size;


    //    Vector3 newPosition = new Vector3();

    //    newPosition.y = Mathf.Sin(angleRad) * distance;



    //    float groundAngle = (90 - angle) * Mathf.Deg2Rad;

    //    newPosition.x = Mathf.Sin(groundAngle) * Mathf.Sqrt(Mathf.Pow(newPosition.y, 2) + Mathf.Pow(distance, 2));
    //    newPosition.z = Mathf.Cos(groundAngle) * Mathf.Sqrt(Mathf.Pow(newPosition.y, 2) + Mathf.Pow(distance, 2));


    //    gameObject.transform.position = player.transform.position + newPosition;

    //    gameObject.transform.LookAt(player.transform);

    //}

    public Transform target;

    Vector3 offset;

    public float damping = 1;
    public bool ChangeCameraPositionForDevPurposes;
    public Vector3 turnOffset;
    public float turnSpeed;
    // Use this for initialization
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;

        if (ChangeCameraPositionForDevPurposes == false)
        {
            transform.localPosition = new Vector3(7.9f, 7.9f, -10.7f);
            transform.LookAt(target);
        } 
        offset = transform.position - target.transform.position;
            }

    // Update is called once per frame
    void Update()
    {

        


    }

    void LateUpdate()
    {
        offset = Quaternion.AngleAxis(InputManager.GetAxis("RHorizontal") * turnSpeed, Vector3.up) * offset;

        Vector3 desiredPosition = target.transform.position + offset;
        





        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * damping);
        //transform.LookAt(target);

    }

}

