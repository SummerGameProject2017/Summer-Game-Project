using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Robot : Collectables {


    public override void OnStart()
    {
        RobotCountText.canvasRenderer.SetAlpha(0.0f);
        //SetRobotCountText();
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

            GameObject CollectedParticle = Instantiate(par_pickup, transform.position, Quaternion.identity) as GameObject;
            //SetRobotCountText();
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            //FadeIn();
            Destroy(CollectedParticle, 1); //Deletes the particles after 1 seconds
        }
    }

    //void SetRobotCountText()
    //{
    //    RobotCountText.text = "Robots Collected: " + Player.Instance.robot;
    //}

    //void FadeIn()
    //{
    //    RobotCountText.CrossFadeAlpha(1.0f, 1.0f, false); //Fade in robot text
    //    StartCoroutine(FadeOutCoroutine());
    //}

    //IEnumerator FadeOutCoroutine()
    //{
    //    yield return new WaitForSeconds(15.0f); //Wait 15 seconds
    //    RobotCountText.CrossFadeAlpha(0.01f, 1.0f, true); //Fade out robot text
    //    gameObject.SetActive(false);
    //}

}
