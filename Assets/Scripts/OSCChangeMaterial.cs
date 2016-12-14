using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// script to animate a standard material through osc. we assume the message contains a single value, and
// we also assume that the material to be animated is the first one in the renderer 
[RequireComponent (typeof (Renderer))]
public class OSCChangeMaterial : OSCAnimation {

	public Material[] materials;


	// Use this for initialization
	void Start () {

		comp = this.GetComponent<Renderer>();

	}

	// Update is called once per frame
	void Update () {

		if (newMessage) {

			try
			{

				int newMaterialIndex = Mathf.RoundToInt( float.Parse(localMsg.Values[0].ToString()));
				if ( materials.Length > newMaterialIndex && materials[newMaterialIndex]!=null ) {

					Renderer r = (Renderer) comp;
					r.material = materials[newMaterialIndex];

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
