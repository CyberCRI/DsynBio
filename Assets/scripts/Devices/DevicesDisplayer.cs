using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DevicesDisplayer : MonoBehaviour {
	
	private List<Device> _devices = new List<Device>();
	public GameObject _devicePrefab;
	private float _timeCounter;
	private float _timeDelta = 0.2f;
	
	//TODO use real device width
	static private float _width = 90.0f;
	static private Vector3 _positionOffset = new Vector3(-486.7966f, 62.19043f, 0.0f);
	
	
	public void addDevice(int deviceID) {
		Debug.Log("addDevice("+deviceID+")");
		if(!_devices.Exists(device => device.getID() == deviceID)) { 
			Vector3 localPosition = _positionOffset + new Vector3(_devices.Count*_width, 0.0f, 0.0f);
			Device device = Device.Create (gameObject.transform, localPosition, deviceID);
			_devices.Add(device);
		}
	}
	
	public void removeDevice(int deviceID) {
		Debug.Log("removeDevice("+deviceID+")");
		Device toRemove = _devices.Find(device => device.getID() == deviceID);
		if(toRemove != null) {
			toRemove.Remove();
			_devices.Remove(toRemove);
			for(int i = 0; i < _devices.Count; i++) {
				Vector3 newLocalPosition = _positionOffset + new Vector3(i*_width, 0.0f, 0.0f);
				_devices[i].Redraw(newLocalPosition);
			}
		}
	}
	
	// Use this for initialization
	void Start () {		
		/*
		for(int i = 0; i < 5; i++) {
			addDevice (i);
		}
		*/
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - _timeCounter > _timeDelta) {
			if (Input.GetKey(KeyCode.C)) {//CREATE
				int randomID = Random.Range(0, 12000);
	        	addDevice(randomID);
			}
	        if (Input.GetKey(KeyCode.R)) {//REMOVE
				if(_devices.Count > 0) {
					int randomIdx = Random.Range(0, _devices.Count);
					Device randomDevice = _devices[randomIdx];
		        	removeDevice(randomDevice.getID());
				}
			}
			_timeCounter = Time.time;
		}
	}
}
