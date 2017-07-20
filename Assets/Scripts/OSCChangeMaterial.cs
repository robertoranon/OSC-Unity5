using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// script to animate a standard material through osc. we assume the message contains a single value, and
// we also assume that the material to be animated is the first one in the renderer 
//[RequireComponent (typeof (Renderer))]
public class OSCChangeMaterial : OSCAnimation {

	public Material[] materials;

    public bool alsoModifyDescendants;

    Component[] renderers;
    bool prevAlsoModifyDescendants;

	// Use this for initialization
	void Start () {

        Init();
        prevAlsoModifyDescendants = alsoModifyDescendants;
	}

	// Update is called once per frame
	void Update () {

		if (newMessage) {

            if ( prevAlsoModifyDescendants != alsoModifyDescendants) {

                prevAlsoModifyDescendants = alsoModifyDescendants;
                Init();
            }


            Debug.Log("received:" + localMsg.Address);

			try
			{

				int newMaterialIndex = Mathf.RoundToInt( float.Parse(localMsg.Values[0].ToString()));
                Debug.Log(newMaterialIndex);
                if (materials.Length > newMaterialIndex && materials[newMaterialIndex] != null)
                {

                    foreach (Component comp in renderers)
                    {
                        Renderer r = (Renderer)comp;
                        r.material = materials[newMaterialIndex];
                    }
                

				}

			}
			catch (System.Exception e)
			{
				Debug.Log ("Wrong propertyname, or missing component, or type mismatch between message value and property value");
			}

			newMessage = false;

		}

	}

    void Init() {

		if (alsoModifyDescendants)
		{
			renderers = GetComponentsInChildren<Renderer>();
		}
		else
		{
			renderers = GetComponents<Renderer>();

		}

    }


}
