using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMovement : MonoBehaviour
{
    public Vector2 uvAnimationRate = new Vector2(1.0f, 0.0f);
    public string textureName = "";
    public bool playerCanFallIn = true;
    Vector2 uvOffset = Vector2.zero;

    void LateUpdate()
    {
        uvOffset += (uvAnimationRate * Time.deltaTime);
        if (GetComponent<Renderer>().enabled && GetComponent<Renderer>().sharedMaterial != null)
        {
            GetComponent<Renderer>().sharedMaterial.SetTextureOffset(textureName, uvOffset);
        }
    }

    private void Update()
    {
        if (playerCanFallIn == false)
        {
            StartCoroutine(Reset());
        }
    }
    IEnumerator Reset()
    {
        yield return new WaitForSeconds(1);
        playerCanFallIn = true;
    }
}