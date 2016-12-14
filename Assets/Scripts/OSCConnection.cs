using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/*
 * unity script to establish OSC Connection. Attach it to any object in the scene, and set 
 * listening port. Sending of OSC events is (for now) disabled. Set also, in the script,
 * the array of gameobjects that will receive OSC events through this connection
 */

public class OSCConnection : MonoBehaviour {

	[HideInInspector]public string RemoteIP = "127.0.0.1"; //127.0.0.1 signifies a local host 
	[HideInInspector] public int SendToPort = 9000; //the port you will be sending from
	public int ListenerPort = 8050; //the port you will be listening on
	public GameObject[] receiverGameObjects; // game objects that will receive OSC events

	private Osc handler; 
	private UDPPacketIO udp;

	private List<OSCAnimation> scriptsToCall = new List<OSCAnimation>();

	// Use this for initialization
	void Start () {

		//Initializes on start up to listen for messages
		udp = new UDPPacketIO ();
		udp.init(RemoteIP, SendToPort, ListenerPort);
		handler = new Osc ();
		handler.init(udp);
		handler.SetAllMessageHandler(AllMessageHandler);
		Debug.Log ("OSC Connection initialized");

		// builds a list of OSCAnimation scripts that are attached to objects in receiverGameObjects array
		for (int i = 0; i< receiverGameObjects.Length; i++) {

			OSCAnimation[] scripts = receiverGameObjects [i].GetComponents<OSCAnimation> ();
			scriptsToCall.AddRange(scripts );

		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDisable() {

		udp.Close ();

	}


	//These functions are called when messages are received
	//Access values via: oscMessage.Values[0], oscMessage.Values[1], etc

	public void AllMessageHandler(OscMessage oscMessage ){


		string msgString = Osc.OscMessageToString(oscMessage); //the message and value combined
		string msgAddress = oscMessage.Address; //the message address
		Debug.Log(msgString); //log the message and values coming from OSC
		bool sentMessage = false;

		foreach( OSCAnimation animator in scriptsToCall ) {
			// send the message to the right OSCAnimator scripts
			if ( animator.messageAddress.Equals( msgAddress )) {
				animator.Animate (oscMessage);
				sentMessage = true;
			}


		}
		if (!sentMessage) {
			Debug.Log ("OSC message with address " + msgAddress + " was not sent to any object");

		}


	}


}
