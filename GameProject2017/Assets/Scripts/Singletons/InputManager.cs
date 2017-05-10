using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoSingleton {

    // Read or not from player
    bool active;



    // Use this for initialization
    public override void StartChild() { }


    //
    // Summary:
    //     ///
    //     Returns the value of the virtual axis identified by axisName.
    //     ///
    //
    // Parameters:
    //   axisName:
    public float GetAxis(string axisName)
    {

        if (active)
        {
            return Input.GetAxis(axisName);
        }
        else
        {
            return 0.0f;
        }
    }


    //
    // Summary:
    //     ///
    //     Returns true while the virtual button identified by buttonName is held down.
    //     ///
    //
    // Parameters:
    //   buttonName:
    public bool GetButton(string buttonName)
    {

        if (active)
        {
            return Input.GetButton(buttonName);
        }
        else
        {
            return false;
        }

    }

    //
    // Summary:
    //     ///
    //     Returns true during the frame the user pressed down the virtual button identified
    //     by buttonName.
    //     ///
    //
    // Parameters:
    //   buttonName:
    public bool GetButtonDown(string buttonName)
    {

        if (active)
        {
            return Input.GetButtonDown(buttonName);
        }
        else
        {
            return false;
        }

    }

    //
    // Summary:
    //     ///
    //     Returns true during the frame the user releases the virtual button identified
    //     by buttonName.
    //     ///
    //
    // Parameters:
    //   buttonName:
    public bool GetButtonUp(string buttonName)
    {

        if (active)
        {
            return Input.GetButtonUp(buttonName);
        }
        else
        {
            return false;
        }

    }

}
