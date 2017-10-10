using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// script to change an animation using legacy unity animations
[RequireComponent (typeof (Animation))]
public class OSCChangeAnimationLegacy : OSCAnimation {

    public List<AnimationClip> clips;

	public float crossFadeTime = 0.3f;

	// Use this for initialization
	void Start () {

        comp = this.GetComponent<Animation>();

	}

	// Update is called once per frame
	void Update () {

		if (newMessage) {

            Debug.Log("received:" + localMsg.Address);

			try
			{

				int newAnimatorIndex = Mathf.RoundToInt( float.Parse(localMsg.Values[0].ToString()));
                Debug.Log(newAnimatorIndex);
				if ( clips.Count > newAnimatorIndex ) {
					//clips[newAnimatorIndex].b
					((Animation)comp).CrossFade( clips[newAnimatorIndex].name, crossFadeTime );

				}

			}
			catch (System.Exception e)
			{
				Debug.Log ("Wrong propertyname, or missing component, or type mismatch between message value and property value");
			}

			newMessage = false;

		}

	}

	AnimationClip GetClipByIndex(int index)
 {
     index = ((Animation)comp).GetClipCount() - (index + 1); // due to unity wrong indexing of animations (it is kinda reversed)
	 Debug.Log("new index: " + index);
	 int i = 0;
     foreach (AnimationState animationState in (Animation)comp)
     {
         if (i == index)
             return animationState.clip;
         i++;
     }
     return null;
 }
}
