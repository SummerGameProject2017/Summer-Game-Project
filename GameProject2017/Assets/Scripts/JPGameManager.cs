﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JPGameManager : MonoBehaviour {
    public static JPGameManager GM;

    public KeyCode jump { get; set;}
    public KeyCode forward { get; set; }
    public KeyCode backward { get; set; }
    public KeyCode left { get; set; }
    public KeyCode right { get; set; }
    public KeyCode joyJump { get; set;}
    public KeyCode joyForward { get; set; }
    public KeyCode attack { get; set; }
    public KeyCode joyAttack { get; set; }
    void Awake()
    {
        if (GM == null)
        {
            DontDestroyOnLoad(gameObject);
            GM = this;
        }
        else if (GM != this)
        {
            Destroy(gameObject);
        }

        jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpKey", "Space"));
        forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forwardKey", "W"));
        backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("backwardKey", "S"));
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey", "A"));
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey", "D"));
        joyJump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("joyJumpKey", "JoystickButton1"));
        attack = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("attackKey", "Return"));
        joyAttack = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("joyAttackKey", "JoystickButton0"));

    }
   
}
