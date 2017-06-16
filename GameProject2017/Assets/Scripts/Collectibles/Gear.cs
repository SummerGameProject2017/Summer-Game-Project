using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gear : Collectables {

    public bool collected;
    PlayerController playerScript;
    AudioSource Chime;

    public override void OnStart()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        Chime = GetComponent<AudioSource>();
    }

    public override void OnUpdate()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        //Collectibles will disappear when player collides with them
        if (other.gameObject.CompareTag("Player"))
        {
            playerScript.showCollectable = true;
            Player player = Player.Instance;
            player.CollectGear();
            Chime.Play();

            par_pickup.transform.position = transform.position;
            par_pickup.GetComponent<ParticleSystem>().Emit(particleCount);

            collected = true;
            gameObject.SetActive(false); //Deletes the gameobject after 2 seconds
        }
    }



}
