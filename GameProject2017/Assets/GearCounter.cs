using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearCounter : MonoBehaviour {
    TextMesh textObject;
    PlayerController playerScript;

	// Use this for initialization
	void Start () {
        playerScript = GameObject.FindWithTag("Player").GetComponent < PlayerController>();
        textObject = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update() {
        Vector3 direction;
        direction = (GameObject.Find("PlayerCamera").transform.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(-direction);
        textObject.text = Player.Instance.gear.ToString();

        if (gameObject.activeSelf)
        {
            StartCoroutine(turnOff());
        }
	}

    IEnumerator turnOff()
    {
        yield return new WaitForSeconds(3);
        playerScript.showCollectable = false;
    }
}
