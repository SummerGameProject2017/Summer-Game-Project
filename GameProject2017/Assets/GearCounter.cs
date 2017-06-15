using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearCounter : MonoBehaviour {
    TextMesh textObject;


	// Use this for initialization
	void Start () {
        textObject = GetComponent<TextMesh>();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        Vector3 direction;
        direction = (GameObject.Find("PlayerCamera").transform.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
        textObject.text = Player.Instance.gear.ToString();

        if (gameObject.activeSelf)
        {
            StartCoroutine(turnOff());
        }
	}

    IEnumerator turnOff()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
}
