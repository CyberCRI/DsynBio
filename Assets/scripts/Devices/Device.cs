using UnityEngine;
using System.Collections;

public class Device : MonoBehaviour {
	
	private static string _activeSuffix = "Active";
	private UITexture _equipedDeviceIcon;
	private string _uri;
	private bool _isActive;
	
	public Device(string uri = "", bool isActive = false) {
		_equipedDeviceIcon = GameObject.Find ("EquipedDeviceIcon").GetComponent<UITexture>();
		_uri = uri;
		setActivity(isActive);
	}
	
	private void setTexture(string textureUri) {
		/*
		// "material" version 
		string materialUri = "Materials/Backdrop";		
		Material myMaterial = Resources.Load(materialUri, typeof(Material)) as Material;		
		_equipedDeviceIcon.material = myMaterial;
		_equipedDeviceIcon.material.mainTexture = myMaterial.mainTexture;
		*/
		
		// "texture" version
		_equipedDeviceIcon.mainTexture = Resources.Load(textureUri, typeof(Texture)) as Texture;
	}
	
	public void setActivity(bool activity) {
		_isActive = activity;
		if(activity) {
			setActive();
		} else {
			setInactive();
		}
	}
	
	public void setActive() {
		_isActive = true;
		setTexture(_uri + _activeSuffix);		
	}
	
	public void setInactive() {
		_isActive = false;
		setTexture(_uri);
	}
	
	// Use this for initialization
	void Start () {
		_equipedDeviceIcon = GameObject.Find ("EquipedDeviceIcon").GetComponent<UITexture>();
		int possibilities = 5;
		float random = Random.Range(0.0f, 1.0f);
		if(random > 1.0f/possibilities) {
			_uri = "Textures/Backdrop";
		} else if(random > 1.0f/possibilities) {
			_uri = "Textures/brick";
		} else if(random > 2.0f/possibilities) {
			_uri = "Textures/brickNM";
		} else if(random > 3.0f/possibilities) {
			_uri = "Textures/burlap";
		} else if(random > 4.0f/possibilities) {
			_uri = "Textures/sand";
		}
		setActive();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
