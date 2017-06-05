using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    GameObject hitpoints1;
    GameObject hitpoints2;
    GameObject hitpoints3;

    GameObject player;


	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");

		hitpoints1 = (GameObject)Instantiate(Resources.Load("Hitpoint"));
        hitpoints2 = (GameObject)Instantiate(Resources.Load("Hitpoint"));
        hitpoints3 = (GameObject)Instantiate(Resources.Load("Hitpoint"));

        hitpoints1.transform.SetParent(this.gameObject.transform);
        hitpoints2.transform.SetParent(this.gameObject.transform);
        hitpoints3.transform.SetParent(this.gameObject.transform);

        hitpoints1.SetActive(false);
        hitpoints2.SetActive(false);
        hitpoints3.SetActive(false);


        hitpoints1.name = "Health1";
        hitpoints2.name = "Health2";
        hitpoints3.name = "Health3";

        HealthChange();

    }
	
	// Update is called once per frame
	void Update () {
        transform.position = player.transform.position + Vector3.up * 2;
        transform.rotation = Quaternion.LookRotation(Vector3.right);
        hitpoints1.transform.position = gameObject.transform.position + gameObject.transform.up * 3;
        hitpoints2.transform.position = gameObject.transform.position + gameObject.transform.up * 3 + (gameObject.transform.right * 2);
        hitpoints3.transform.position = gameObject.transform.position + gameObject.transform.up * 3 - (gameObject.transform.right * 2);
    }


    public void HealthChange()
    {

        if (Player.Instance.lives == 3)
        {
            hitpoints1.SetActive(true);
            hitpoints2.SetActive(true);
            hitpoints3.SetActive(true);
        }
        if (Player.Instance.lives == 2)
        {
            hitpoints1.SetActive(true);
            hitpoints2.SetActive(true);
            hitpoints3.SetActive(false);
        }
        if (Player.Instance.lives == 1)
        {
            hitpoints1.SetActive(true);
            hitpoints2.SetActive(false);
            hitpoints3.SetActive(false);
        }


    }


}
