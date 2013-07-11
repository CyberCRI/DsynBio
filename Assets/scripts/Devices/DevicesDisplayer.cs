using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DevicesDisplayer : MonoBehaviour {
	
	private LinkedList<Device> _devices;
	public GameObject _devicePrefab;
	private Vector3 _positionOffset;
	private float _width;
	
	// Use this for initialization
	void Start () {
		/*
		_devices = new LinkedList<Device>();
		Device newDevice = new Device("Backdrop 1", true);
		_devices.AddLast(newDevice);
		*/
		
		//deactivate
		//TODO use real device width
		_width = 90.0f;
		_positionOffset = new Vector3(-486.7966f, 62.19043f, 0.0f);
		
		for(int i = 0; i < 5; i++) {
			Vector3 localPosition = _positionOffset + new Vector3(i*_width, 0.0f, 0.0f);
			GameObject newDevice = Instantiate(_devicePrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity) as GameObject;
			newDevice.transform.parent = gameObject.transform;
			newDevice.transform.localPosition = localPosition;
			newDevice.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
