# OSC-Unity5

Scripts to control unity components based on osc input. Each script reads the first value sent in the OSC message, and use it to set a single property.

- OSCAnimateLight.cs: attach this to a game object that contains a light. Allows to control light intensity, range, color
- OSCAnimateStandardMaterial.cs: attach this to a game object with a Renderer, and a standard material as first material. The script allows to control the material Albedo color (one channel at a time), Emission Color (one channel at a time), Emission multiplier, Metallic, Smoothness, main texture tiling and offset. 
- OSCChangeMaterial.cs: attach this to a game object with a Renderer. Insert a list of Materials in the script. Then, OSC events control which material is applied: 0 = first material, 1 = second material, 2 = third material, and so on.
- OSCAnimateSingleValue.cs: attach this to any game object. Set the component to control (e.g., Transform) and the field (e.g., position[0]). This script can also read more values in a single OSC message (e.g. three values) and use them to set a multi-value property (e.g., position).

To control more properties, attach more scripts. For example, to fully control a material albedo color, attach three OSCAnimateStandardMaterial scripts to the object, one for the red channel, one for the green, and one for the blue. 
