using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OSCEnableDisableGameObject : OSCAnimation {

	
	// Use this for initialization
	void Start () {

		

	}

	// Update is called once per frame
	void Update () {

		if (newMessage) {

			try
			{

				float value = float.Parse(localMsg.Values[0].ToString());
				
				if (value > 0) {

					MeshRenderer[] allRenderers = this.GetComponentsInChildren<MeshRenderer>();
					foreach (MeshRenderer r in allRenderers) {
						r.enabled = false;
					}

					SkinnedMeshRenderer[] allSkinnedRenderers = this.GetComponentsInChildren<SkinnedMeshRenderer>();
					foreach (SkinnedMeshRenderer r in allSkinnedRenderers) {
						r.enabled = false;
					}

				} else {

					MeshRenderer[] allRenderers = this.GetComponentsInChildren<MeshRenderer>();
					foreach (MeshRenderer r in allRenderers) {
						r.enabled = true;
					}

					SkinnedMeshRenderer[] allSkinnedRenderers = this.GetComponentsInChildren<SkinnedMeshRenderer>();
					foreach (SkinnedMeshRenderer r in allSkinnedRenderers) {
						r.enabled = true;
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
}
