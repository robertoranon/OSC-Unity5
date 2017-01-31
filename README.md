# OSC-Unity5

Scripts to control unity components based on osc input. Each script reads the first value sent in the OSC message, and use it to set a single property. They are quite limited, but do not require any coding to be used.

The following video, made by [Fede Deltaprocess](https://www.facebook.com/search/top/?q=fede%20deltaprocess%20processo), shows how to use them:

[![IMAGE ALT TEXT HERE](https://img.youtube.com/vi/1Jo4PvfU8Tw/0.jpg)](https://www.youtube.com/watch?v=1Jo4PvfU8Tw&feature=youtu.be)

- OSCAnimateLight.cs: attach this to a game object that contains a light. Allows to control light intensity, range, color
- OSCAnimateStandardMaterial.cs: attach this to a game object with a Renderer, and a standard material as first material. The script allows to control the material Albedo color (one channel at a time), Emission Color (one channel at a time), Emission multiplier, Metallic, Smoothness, main texture tiling and offset (one component at a time). 
- OSCChangeMaterial.cs: attach this to a game object with a Renderer. Insert a list of Materials in the script. Then, OSC events control which material is applied: 0 = first material, 1 = second material, 2 = third material, and so on.
- OSCAnimateTransform.cs: attach this to any game object. Controls position, rotation, scale, one component at a time.
- OSCAnimateSingleValue.cs: attach this to any game object. Set the component to control (e.g., Transform) and the field (e.g., position[0]).  

To control more properties, attach more scripts. For example, to fully control a material albedo color, attach three OSCAnimateStandardMaterial scripts to the object, one for the red channel, one for the green, and one for the blue. To control a transform x, y, and z position, attach three OSCAnimateTransform scripts.

Furthermore, one needs to add the OSCConnection.cs script to some gameobject in the scene, and set in it:
- the port for osc input (ListenerPort)
- the gameobjects that contain some OSC script to control some property. If a game object is not listed here, OSC events will not be sent to it.

Current Limitations:

- scripts that act on materials (OSCAnimateStandardMaterial and OSCChangeMaterial) only operate on the first material of the game object.
- the OSCAnimateStandardMaterial only works with the Unity Standard Material (might work limitedly with others)
- we just use the first value in the OSC message, the others are ignored. 

# Credits

Based on osc/networking code found in [this thread](https://forum.unity3d.com/threads/midi-or-osc-for-unity-indie-users.16882/) by Dave Pentecost.
