using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : Collectables
{


    public override void OnStart()
    {
        SetRobotCountText();
    }

    public override void OnUpdate()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        //Collectibles will disappear when player collides with them
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = Player.Instance;

            player.CollectRobot();

            GameObject Collected = Instantiate(par_pickup, transform.position, Quaternion.identity) as GameObject;
            SetRobotCountText();
            gameObject.SetActive(false);
            Destroy(Collected, 2); //Deletes the particles after 2 seconds
        }
    }

    void SetRobotCountText()
    {
        RobotCountText.text = "Robots Collected: " + Player.Instance.robot;
    }

}
