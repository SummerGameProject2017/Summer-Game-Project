/********************************************************
Programmer: Eric Sellitto

Function: Easily adjustable to create different and multiple
kinds of rotating platforms.

In Inspector - Center
Center of what axis you want to rotate

In Inspector - Direction
0, 1, 0  Up
0, -1, 0 Down
1, 0, 0 Right
-1, 0, 0 Left

In Inspector - Speed Adjustment

In Inspector - Combos

X axis 
Center = 2, 0, 0 (2 can be bigger or smaller depending on desired rotation)
Dir    = 0, 1, 0 (negative for opposite direction)

Y axis 
Gives a flip 
Center = 0, 4, 0
Dir    = 0, 0, 1

********************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Vector3 center;
    public Vector3 direction;
    public float speed;

    void Update()
    {
        transform.RotateAround(center, direction, speed * Time.deltaTime);
    }
}
