using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// script to animate a standard material through osc. we assume the message contains a single value, and
// we also assume that the material to be animated is the first one in the renderer 
[RequireComponent (typeof (Animator))]
public class OSCChangeAnimator : OSCAnimation {

    public RuntimeAnimatorController[] animators;


	// Use this for initialization
	void Start () {

        comp = this.GetComponent<Animator>();

	}

	// Update is called once per frame
	void Update () {

		if (newMessage) {

            Debug.Log("received:" + localMsg.Address);

			try
			{

				int newAnimatorIndex = Mathf.RoundToInt( float.Parse(localMsg.Values[0].ToString()));
                Debug.Log(newAnimatorIndex);
				if ( animators.Length > newAnimatorIndex && animators[newAnimatorIndex]!=null ) {

					Animator r = (Animator) comp;
                    r.runtimeAnimatorController = animators[newAnimatorIndex];

				}

			}
			catch (System.Exception e)
			{
				Debug.Log ("Wrong propertyname, or missing component, or type mismatch between message value and property value");
			}

			newMessage = false;

		}

	}
}
