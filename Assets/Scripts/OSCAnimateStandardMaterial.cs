using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	Emission_Brightness,
	Tiling_X,
	Tiling_Y,
	Offset_X,
	Offset_Y
}


// script to animate a standard material through osc. we assume the message contains a single value, and
// we also assume that the material to be animated is the first one in the renderer 
[RequireComponent (typeof (Renderer))]
public class OSCAnimateStandardMaterial : OSCAnimation {

	private Material mat;

	

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

                animateMaterial( localMsg);
					
			}
			catch (System.Exception e)
			{
				Debug.Log ("Wrong propertyname, or missing component, or type mismatch between message value and property value");
			}

			newMessage = false;

		}
		
	}


    public void animateMaterial(OscMessage localMsg ) {

		Color prevColor;
		Vector2 oldValue;
		mat = this.GetComponent<Renderer>().material;

		switch (fieldToAnimate)
		{

			case STANDARD_MATERIAL_FIELDS.Albedo_Color_R:
				prevColor = mat.GetColor("_Color");
				prevColor.r = float.Parse(localMsg.Values[0].ToString());
				mat.SetColor("_Color", prevColor);
				break;

			case STANDARD_MATERIAL_FIELDS.Albedo_Color_G:
				prevColor = mat.GetColor("_Color");
				prevColor.g = float.Parse(localMsg.Values[0].ToString());
				mat.SetColor("_Color", prevColor);
				break;

			case STANDARD_MATERIAL_FIELDS.Albedo_Color_B:
				prevColor = mat.GetColor("_Color");
				prevColor.b = float.Parse(localMsg.Values[0].ToString());
				mat.SetColor("_Color", prevColor);
				break;

			case STANDARD_MATERIAL_FIELDS.Emission_Color_R:
				prevColor = mat.GetColor("_EmissionColor");
				prevColor.r = float.Parse(localMsg.Values[0].ToString());
				mat.SetColor("_EmissionColor", prevColor);
				break;

			case STANDARD_MATERIAL_FIELDS.Emission_Color_G:
				prevColor = mat.GetColor("_EmissionColor");
				prevColor.g = float.Parse(localMsg.Values[0].ToString());
				mat.SetColor("_EmissionColor", prevColor);
				break;

			case STANDARD_MATERIAL_FIELDS.Emission_Color_B:
				prevColor = mat.GetColor("_EmissionColor");
				prevColor.b = float.Parse(localMsg.Values[0].ToString());
				mat.SetColor("_EmissionColor", prevColor);
				break;

			case STANDARD_MATERIAL_FIELDS.Emission_Brightness:
				prevColor = mat.GetColor("_EmissionColor");
				float h, s, v;
				RGBToHSV2(prevColor, out h, out s, out v);
				v = float.Parse(localMsg.Values[0].ToString());
				Color newValue = HSVToRGB2(h, s, v);
				mat.SetColor("_EmissionColor", newValue);
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


	public static Color HSVToRGB2(float H, float S, float V)
	{
		if (S == 0f)
			return new Color(V,V,V);
		else if (V == 0f)
			return Color.black;
		else
		{
			Color col = Color.black;
			float Hval = H * 6f;
			int sel = Mathf.FloorToInt(Hval);
			float mod = Hval - sel;
			float v1 = V * (1f - S);
			float v2 = V * (1f - S * mod);
			float v3 = V * (1f - S * (1f - mod));
			switch (sel + 1)
			{
			case 0:
				col.r = V;
				col.g = v1;
				col.b = v2;
				break;
			case 1:
				col.r = V;
				col.g = v3;
				col.b = v1;
				break;
			case 2:
				col.r = v2;
				col.g = V;
				col.b = v1;
				break;
			case 3:
				col.r = v1;
				col.g = V;
				col.b = v3;
				break;
			case 4:
				col.r = v1;
				col.g = v2;
				col.b = V;
				break;
			case 5:
				col.r = v3;
				col.g = v1;
				col.b = V;
				break;
			case 6:
				col.r = V;
				col.g = v1;
				col.b = v2;
				break;
			case 7:
				col.r = V;
				col.g = v3;
				col.b = v1;
				break;
			}
			col.r = Mathf.Clamp(col.r, 0f, 1f);
			col.g = Mathf.Clamp(col.g, 0f, 1f);
			col.b = Mathf.Clamp(col.b, 0f, 1f);
			return col;
		}
	}


	public static void RGBToHSV2(Color rgbColor, out float H, out float S, out float V)
	{
		if (rgbColor.b > rgbColor.g && rgbColor.b > rgbColor.r)
		{
			RGBToHSVHelper(4f, rgbColor.b, rgbColor.r, rgbColor.g, out H, out S, out V);
		}
		else
		{
			if (rgbColor.g > rgbColor.r)
			{
				RGBToHSVHelper(2f, rgbColor.g, rgbColor.b, rgbColor.r, out H, out S, out V);
			}
			else
			{
				RGBToHSVHelper(0f, rgbColor.r, rgbColor.g, rgbColor.b, out H, out S, out V);
			}
		}
	}

	private static void RGBToHSVHelper(float offset, float dominantcolor, float colorone, float colortwo, out float H, out float S, out float V)
	{
		V = dominantcolor;
		if (V != 0f)
		{
			float num = 0f;
			if (colorone > colortwo)
			{
				num = colortwo;
			}
			else
			{
				num = colorone;
			}
			float num2 = V - num;
			if (num2 != 0f)
			{
				S = num2 / V;
				H = offset + (colorone - colortwo) / num2;
			}
			else
			{
				S = 0f;
				H = offset + (colorone - colortwo);
			}
			H /= 6f;
			if (H < 0f)
			{
				H += 1f;
			}
		}
		else
		{
			S = 0f;
			H = 0f;
		}
	}


}


