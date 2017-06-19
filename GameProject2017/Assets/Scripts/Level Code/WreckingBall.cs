using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WreckingBall : MonoBehaviour {

    public bool canHitPlayer = true;

    void Update()
    {
        if (canHitPlayer == false)
        {
            StartCoroutine(hitPlayer());
        }

    }


    IEnumerator hitPlayer()
    {
        yield return new WaitForSeconds(1.5f);
        if (Player.Instance.lives > 0)
        {
            canHitPlayer = true;
        }
    }
}
