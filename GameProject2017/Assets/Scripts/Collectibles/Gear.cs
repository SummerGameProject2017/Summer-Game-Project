using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gear : Collectables {

    public bool collected;
    public override void OnStart()
    {
        //GearCountText.canvasRenderer.SetAlpha(0.0f);
        //SetGearCountText();
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
            player.CollectGear();

            GameObject CollectedParticle = Instantiate(par_pickup, transform.position, Quaternion.identity) as GameObject;
            gameObject.SetActive(false);
            //SetGearCountText();
            //gameObject.GetComponent<MeshRenderer>().enabled = false;
            //gameObject.GetComponent<BoxCollider>().enabled = false;
            //FadeIn();
            Destroy(CollectedParticle, 1); //Deletes the particles after 1 seconds
        }
    }

    //Display to the screen the amount of gears collected
    //void SetGearCountText()
    //{
    //    GearCountText.text = "Gears: " + Player.Instance.gear;
    //}

    //void FadeIn()
    //{
    //    GearCountText.CrossFadeAlpha(1.0f, 1.0f, false); //Fade in gear text
    //    StartCoroutine(FadeOutCoroutine());
    //}

    //IEnumerator FadeOutCoroutine()
    //{
    //    yield return new WaitForSeconds(15.0f); //Wait 15 seconds
    //    GearCountText.CrossFadeAlpha(0.01f, 1.0f, false); //Fade out gear text
    //    gameObject.SetActive(false);
    //}

}
