using UnityEngine;
using System.Collections;

public class Device : MonoBehaviour {
	
	static string _activeSuffix = "Active";
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
		_uri = "Textures/Backdrop 1";
			
		setActive();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
