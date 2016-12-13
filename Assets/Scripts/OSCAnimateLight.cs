using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Light))]
public class OSCAnimateLight : OSCAnimation {

	private Light l;

	public enum LIGHT_FIELDS
	{
		Color_R,
		Color_G,
		Color_B,
		Range,
		Intensity,
	}

	public LIGHT_FIELDS fieldToAnimate;

	// Use this for initialization
	void Start () {

		l = this.GetComponent<Light>();

	}

	// Update is called once per frame
	void Update () {

		if (newMessage) {

			try
			{

				Color prevColor;

				switch (fieldToAnimate) {

				case LIGHT_FIELDS.Color_R:
					prevColor = l.color;
					prevColor.r = float.Parse(localMsg.Values[0].ToString());
					l.color = prevColor;
					break;

				case LIGHT_FIELDS.Color_G:
					prevColor = l.color;
					prevColor.g = float.Parse(localMsg.Values[0].ToString());
					l.color = prevColor;
					break;

				case LIGHT_FIELDS.Color_B:
					prevColor = l.color;
					prevColor.b = float.Parse(localMsg.Values[0].ToString());
					l.color = prevColor;
					break;

				case LIGHT_FIELDS.Intensity:
					l.intensity = float.Parse(localMsg.Values[0].ToString());
					break;

				case LIGHT_FIELDS.Range:
					l.range = float.Parse(localMsg.Values[0].ToString());
					break;

				default:
					break;
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
