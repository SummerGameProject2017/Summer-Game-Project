using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gear : Collectables {

    public bool collected = true;
    PlayerController playerScript;

    public override void OnStart()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    public override void OnUpdate()
    {
        if (playerScript.gear == this.gameObject)
        {
            playerScript.showCollectable = true;
            Player player = Player.Instance;
            player.CollectGear();


            par_pickup.transform.position = transform.position;
            par_pickup.GetComponent<ParticleSystem>().Emit(particleCount);
           

            gameObject.SetActive(false);
            collected = true;
            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Collectibles will disappear when player collides with them
        if (other.gameObject.CompareTag("Player"))
        {
           
        }
    }



}
