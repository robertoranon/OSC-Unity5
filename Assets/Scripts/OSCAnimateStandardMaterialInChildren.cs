﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// script to animate a standard material through osc. we assume the message contains a single value, and
// we also assume that the material to be animated is the first one in the renderer 

public class OSCAnimateStandardMaterialInChildren : OSCAnimation {

    private List<OSCAnimateStandardMaterial> mats = new List<OSCAnimateStandardMaterial>();


	// Use this for initialization
	void Start () {

        Init();
		
	}

	void Init()
	{
        Component[] renderers = GetComponentsInChildren<OSCAnimateStandardMaterial>();
        mats.Clear();
        foreach (Component comp in renderers)
        {

            OSCAnimateStandardMaterial r = (OSCAnimateStandardMaterial)comp;
            mats.Add(r);
        }
	}
	
	// Update is called once per frame
	void Update () {

		if (newMessage) {

			try
			{

                foreach(OSCAnimateStandardMaterial o in mats) {

                    o.animateMaterial(localMsg);

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


