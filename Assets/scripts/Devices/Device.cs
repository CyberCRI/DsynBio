using UnityEngine;
using System.Collections;

public class Device : MonoBehaviour {
	
	private UITexture _equipedDeviceIcon;
	private string _uri;
	
	//TODO
	/*
	public Device(string uri = "") {
		_equipedDeviceIcon = GameObject.Find ("EquipedDeviceIcon").GetComponent<UITexture>();
		_uri = uri;
	}
	*/

	// Use this for initialization
	void Start () {		
		_equipedDeviceIcon = GameObject.Find ("EquipedDeviceIcon").GetComponent<UITexture>();
		
		/*
		// "material" version 
		string materialUri = "Materials/Backdrop";		
		Material myMaterial = Resources.Load(materialUri, typeof(Material)) as Material;		
		_equipedDeviceIcon.material = myMaterial;
		_equipedDeviceIcon.material.mainTexture = myMaterial.mainTexture;
		*/
		
		// "texture" version
		string textureUri = "Textures/Backdrop 1";		
		Texture myTexture = Resources.Load(textureUri, typeof(Texture)) as Texture;		
		_equipedDeviceIcon.mainTexture = myTexture;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
