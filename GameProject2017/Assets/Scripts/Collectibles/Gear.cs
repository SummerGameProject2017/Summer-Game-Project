using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : Collectables {



    public override void OnStart()
    {
        SetGearCountText();
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

            GameObject Collected = Instantiate(par_pickup, transform.position, Quaternion.identity) as GameObject;
            SetGearCountText();
            gameObject.SetActive(false);
            Destroy(Collected, 1); //Deletes the particles after 2 seconds
        }
    }

    //Display to the screen the amount of gears collected
    void SetGearCountText()
    {
        GearCountText.text = "Gears: " + Player.Instance.gear;
    }

}
