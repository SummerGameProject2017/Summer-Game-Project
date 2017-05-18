using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoSingleton<InputManager> {


    static readonly float DEADZONE = 0.5f;

    // Read or not from player
    static bool active;// {

    //    get { return active; }
    //    set { active = value; }

    //}


    public override void OnStart(){ active = true;  }
    public override void OnUpdate(){ }


    //
    // Summary:
    //     ///
    //     Returns the value of the virtual axis identified by axisName.
    //     ///
    //
    // Parameters:
    //   axisName: Name of the axis on Unity's Input Manager
    //   deadzone (default: true): Should it consider a DeadZone? 
    public static float GetAxis(string axisName, bool deadzone = true)
    {
        if (active)
        {
            float axis = Input.GetAxis(axisName);

            if (deadzone)
            {
                return (axis > DEADZONE || axis < -DEADZONE) ? axis : 0.0f;
            }
            else
            {
                return axis;
            }
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
    public static bool GetButtonUp(string buttonName)
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

    //
    // Summary:
    //     ///
    //     Disables input readings for an amount of time
    //     ///
    //
    // Parameters:
    //   seconds: Amount of seconds the input should be disabled
    public static void DisableBytime(float seconds)
    {
        active = false;
        instance.StartCoroutine(instance.EnableInput(Time.time + seconds));

    }


    IEnumerator EnableInput(float time)
    {

        while (Time.time < time)
        {
            yield return new WaitForFixedUpdate();
        }

        active = true;

    }


}
