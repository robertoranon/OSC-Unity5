using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// script to animate a standard material through osc. we assume the message contains a single value, and
// we also assume that the material to be animated is the first one in the renderer 
[RequireComponent (typeof (Renderer))]
public class OSCAnimateStandardMaterial : OSCAnimation {

	private Material mat;

	public enum STANDARD_MATERIAL_FIELDS
	{
		Albedo_Color_R,
		Albedo_Color_G,
		Albedo_Color_B,
		Metallic,
		Smoothness,
		Emission_Color_R,
		Emission_Color_G,
		Emission_Color_B,
		Emission_Multiplier,
		Tiling_X,
		Tiling_Y,
		Offset_X,
		Offset_Y
	}

	public STANDARD_MATERIAL_FIELDS fieldToAnimate;

	// Use this for initialization
	void Start () {

		comp = this.GetComponent<Renderer>();
		mat = this.GetComponent<Renderer> ().material;
		
	}
	
	// Update is called once per frame
	void Update () {

		if (newMessage) {

			try
			{

				Color prevColor;
				Vector2 oldValue;

				switch (fieldToAnimate) {

				case STANDARD_MATERIAL_FIELDS.Albedo_Color_R:
					prevColor = mat.GetColor("_Color");
					prevColor.r = float.Parse(localMsg.Values[0].ToString());
					mat.SetColor("_Color", prevColor );
					break;

				case STANDARD_MATERIAL_FIELDS.Albedo_Color_G:
					prevColor = mat.GetColor("_Color");
					prevColor.g = float.Parse(localMsg.Values[0].ToString());
					mat.SetColor("_Color", prevColor );
					break;

				case STANDARD_MATERIAL_FIELDS.Albedo_Color_B:
					prevColor = mat.GetColor("_Color");
					prevColor.b = float.Parse(localMsg.Values[0].ToString());
					mat.SetColor("_Color", prevColor );
					break;

				case STANDARD_MATERIAL_FIELDS.Emission_Color_R:
					prevColor = mat.GetColor("_EmissionColor");
					prevColor.r = float.Parse(localMsg.Values[0].ToString());
					mat.SetColor("_EmissionColor", prevColor );
					break;

				case STANDARD_MATERIAL_FIELDS.Emission_Color_G:
					prevColor = mat.GetColor("_EmissionColor");
					prevColor.g = float.Parse(localMsg.Values[0].ToString());
					mat.SetColor("_EmissionColor", prevColor );
					break;

				case STANDARD_MATERIAL_FIELDS.Emission_Color_B:
					prevColor = mat.GetColor("_EmissionColor");
					prevColor.b = float.Parse(localMsg.Values[0].ToString());
					mat.SetColor("_EmissionColor", prevColor );
					break;

				case STANDARD_MATERIAL_FIELDS.Emission_Multiplier:
					prevColor = mat.GetColor("_EmissionColor");
					prevColor = float.Parse(localMsg.Values[0].ToString()) * prevColor;
					mat.SetColor("_EmissionColor", prevColor );
					break;

				case STANDARD_MATERIAL_FIELDS.Metallic:
					mat.SetFloat("_Metallic", float.Parse(localMsg.Values[0].ToString()));
					break;

				case STANDARD_MATERIAL_FIELDS.Smoothness:
					mat.SetFloat("_Glossiness", float.Parse(localMsg.Values[0].ToString()));
					break;

				case STANDARD_MATERIAL_FIELDS.Offset_X:
					oldValue = mat.mainTextureOffset;
					oldValue.x = float.Parse(localMsg.Values[0].ToString());
					mat.mainTextureOffset = oldValue;
					break;

				case STANDARD_MATERIAL_FIELDS.Offset_Y:
					oldValue = mat.mainTextureOffset;
					oldValue.y = float.Parse(localMsg.Values[0].ToString());
					mat.mainTextureOffset = oldValue;
					break;

				case STANDARD_MATERIAL_FIELDS.Tiling_X:
					oldValue = mat.mainTextureScale;
					oldValue.x = float.Parse(localMsg.Values[0].ToString());
					mat.mainTextureScale = oldValue;
					break;

				case STANDARD_MATERIAL_FIELDS.Tiling_Y:
					oldValue = mat.mainTextureScale;
					oldValue.y = float.Parse(localMsg.Values[0].ToString());
					mat.mainTextureScale = oldValue;
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
