using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DevicesDisplayer : MonoBehaviour {
	
	private LinkedList<Device> _devices;
	
	// Use this for initialization
	void Start () {
		_devices = new LinkedList<Device>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
