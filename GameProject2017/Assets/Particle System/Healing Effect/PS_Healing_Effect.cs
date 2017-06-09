using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_Healing_Effect : MonoBehaviour
{
    public float frequency = .2f; //repeate rate
    public float resolution = 20f; //the amount of keys used to make a curve
    public float amplitude = 1.0f; //the height of values -1f to 1f
    PlayerController playerScript;
    GameObject player;
    //public float Zvalue = 1f;

    void createcircle()
    {
        ParticleSystem PS = GetComponent<ParticleSystem>(); //Grabs the Particle System
        var vel = PS.velocityOverLifetime; //Grabs the Velocity over life time
        vel.enabled = true; //enables the velocity over lifetime 
        vel.space = ParticleSystemSimulationSpace.Local; //set the partical system to local
        PS.startSpeed = 0f;
        //vel.z = new ParticleSystem.MinMaxCurve(10.0f, Zvalue);

        AnimationCurve curveX = new AnimationCurve();//create a new curve
        for (int i = 0; i < resolution; i++)//loop though the amount of resolution
        {
            float newtime = (i / (resolution - 1)); //calculate the time where the point is set
            float myvalue = amplitude * Mathf.Sin(newtime * (frequency * 2) * Mathf.PI);//depending on the time calculate the value of the point

            curveX.AddKey(newtime, myvalue);//add the calculated point to the curve
        }
        vel.x = new ParticleSystem.MinMaxCurve(1.0f, curveX); //now create the actual curve (10.0f is the value multiplier)

        AnimationCurve curveY = new AnimationCurve();
        for (int i = 0; i < resolution; i++)
        {
            float newtime = (i / (resolution - 1));
            float myvalue = amplitude * Mathf.Cos(newtime * (frequency * 2) * Mathf.PI);

            curveY.AddKey(newtime, myvalue);
        }
        vel.y = new ParticleSystem.MinMaxCurve(1.0f, curveY);

        AnimationCurve curveZ = new AnimationCurve();
        for (int i = 0; i < resolution; i++)
        {
            float newtime = (i / (resolution - 1));
            float myvalue = amplitude * Mathf.Cos(newtime * (frequency * 2) * Mathf.PI);

            curveZ.AddKey(newtime, myvalue);
        }
        vel.z = new ParticleSystem.MinMaxCurve(1.0f, curveZ);
    }

   

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        if (playerScript.healingEffect == null)
        {
            playerScript.healingEffect = this.gameObject;
        }
        else
        {
            DestroyImmediate(this.gameObject);
        }
        createcircle();
	}

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 4, player.transform.position.z);

        if (playerScript.destroyHealingParticle == true)
        {
            DestroyImmediate(this.gameObject);
        }
    }
}



    

