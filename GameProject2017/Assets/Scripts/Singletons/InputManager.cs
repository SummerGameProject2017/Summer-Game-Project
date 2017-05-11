using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoSingleton<InputManager> {

    // Read or not from player
    static bool active = true;

    public override void OnStart(){ }
    public override void OnUpdate(){ }


    //
    // Summary:
    //     ///
    //     Returns the value of the virtual axis identified by axisName.
    //     ///
    //
    // Parameters:
    //   axisName:
    public static float GetAxis(string axisName)
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
    public static bool GetButton(string buttonName)
    {

        if (InputManager.active)
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
    public static bool GetButtonDown(string buttonName)
    {

        if (InputManager.active)
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
