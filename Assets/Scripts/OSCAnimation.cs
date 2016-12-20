using UnityEngine;
using System.Collections;
using System.Reflection;

/*
 * unity script to animate objects based on OSC events. This is like an abstract class,
 * do not attach it to objects - use instead one of its subclasses
 */

public class OSCAnimation : MonoBehaviour {


	public string messageAddress; // OSC message address
	protected string componentName; // type of the component to animate (must be attached to this object)
	protected string propertyName; // name of the property of the component to be animated 

	protected PropertyInfo property; // we use C# reflection to get the actual property from its name
	protected FieldInfo field; // we use C# reflection to get the actual property from its name
	protected Component comp;
	protected OscMessage localMsg;
	protected bool newMessage = false;
	protected bool isMultiValue = false; // true if the property is multi-valued (e.g., Color, Vector3, ...)
	protected int index; // if the property is multi-valued (e.g., Color, Vector3, ...), index of the value to change 

	// Use this for initialization
	void Start () {

		Init ();

	}

	protected void Init() {

		comp = this.GetComponent (componentName);
		if (!propertyName.Contains ("[")) {  // i.e. the property is a single value, not an array
			property = comp.GetType ().GetProperty (propertyName);
			if (property == null) {
				field = comp.GetType ().GetField (propertyName);
			}
		} else { // we mean to change a single value inside an array-type value
			property = comp.GetType ().GetProperty (propertyName.Substring (0, propertyName.IndexOf ('[')));
			if (property == null) {
				field = comp.GetType ().GetField (propertyName.Substring (0, propertyName.IndexOf ('[')));
			}
			index = System.Int32.Parse (propertyName.Substring (propertyName.IndexOf ('[')+1, 1));
			isMultiValue = true;
		}
	}
	

	public void Animate( OscMessage value ) {

		localMsg = value; // we just set the localMsg variable and signal that there is a new message
		newMessage = true;

	}


}
