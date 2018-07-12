using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OSCEnableDisableScript : OSCAnimation {

	public string scriptName;


	// Use this for initialization
	void Start () {

		

	}

	// Update is called once per frame
	void Update () {

		if (newMessage) {

			//try
			//{

				Debug.Log(localMsg.Values[0]);
				float value = float.Parse(localMsg.Values[0].ToString());
				
				if (value > 0.0001) {

					MonoBehaviour sc = this.GetComponent(scriptName) as MonoBehaviour;
					sc.enabled = false;

				} else {

					MonoBehaviour sc = this.GetComponent(scriptName) as MonoBehaviour;
					sc.enabled = true;
					
				} 
				

			//}
			//catch (System.Exception e)
			//{
			//	Debug.Log ("Wrong propertyname, or missing component, or type mismatch between message value and property value");
			//}

			newMessage = false;

		}

	}
}
