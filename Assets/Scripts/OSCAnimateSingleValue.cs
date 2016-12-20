using UnityEngine;
using System.Collections;
using System.Reflection;

/*
 * unity script to animate a single value - works both for single-value properties (e.g., light intensity),
 * and for changing one value in a multi-valued property (currently, works for Vector3 and Color)
 */

public class OSCAnimateSingleValue : OSCAnimation {

	public string Component;
	public string Property;

	void Start () {

		componentName = Component;
		propertyName = Property;
		Init ();

	}

	// Update is called once per frame
	void Update () {

		if (newMessage) {

			try
			{
				if (!isMultiValue) { // we directly set the value if the property is single-value
					if (property == null) {
						field.SetValue( comp, localMsg.Values[0]);
					} else {
						property.SetValue (comp, localMsg.Values[0], null);
					}
				} else {
					if (property == null) {
						object val = field.GetValue(comp);
						if ( val.GetType() == typeof(Color) ) {
							Color v = (Color)val;
							v[index] = (float)localMsg.Values[0];
							field.SetValue (comp, v);
						}
						if ( val.GetType() == typeof(Vector3) ) {
							Vector3 v = (Vector3)val;
							v[index] = (float)localMsg.Values[0];
							field.SetValue (comp, v);
						}
					} else {
					object val = property.GetValue(comp,null);
					if ( val.GetType() == typeof(Color) ) {
						Color v = (Color)val;
						v[index] = (float)localMsg.Values[0];
						property.SetValue (comp, v,null);
					}
					if ( val.GetType() == typeof(Vector3) ) {
						Vector3 v = (Vector3)val;
						v[index] = (float)localMsg.Values[0];
						property.SetValue (comp, v,null);
					}
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
