using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMovement : MonoBehaviour
{
    public Vector2 uvAnimationRate = new Vector2(1.0f, 0.0f);
    public string textureName = "";

    Vector2 uvOffset = Vector2.zero;

    void LateUpdate()
    {
        uvOffset += (uvAnimationRate * Time.deltaTime);
        if (GetComponent<Renderer>().enabled)
        {
            GetComponent<Renderer>().sharedMaterial.SetTextureOffset(textureName, uvOffset);
        }
    }
}