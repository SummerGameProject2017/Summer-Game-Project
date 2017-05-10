using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IChildEvents
{




    /*
     *------------------------------------
       Pure virtual functions.
     *------------------------------------
     */
    public abstract void OnStart();
    public abstract void OnUpdate();
    //====================================


    /*
     *------------------------------------
       Virtual functions.
     *------------------------------------
     */
    public virtual void OnAwake() { }

    public virtual void OnFixedUpdate() { }
    //====================================


    // This is called on the Awake
    void Awake()
    {


        OnAwake();
    }

    // Use this for initialization
    void Start()
    {


        OnStart();
    }

    // Update is called once per frame
    void Update()
    {


        OnUpdate();

    }

    // Use it for Physics
    void FixedUpdate()
    {


        OnFixedUpdate();
    }
}
