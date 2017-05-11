using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour, IChildEvents where T: MonoSingleton<T>, new() {

    static protected T instance;



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


   // public abstract void StartChild();



	// Use this for initialization
	void Start () {

        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }


        OnStart();

	}
}
