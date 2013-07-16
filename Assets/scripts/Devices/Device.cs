using UnityEngine;
using System.Collections;

public class Device : MonoBehaviour {
	
	private static string _activeSuffix = "Active";
	public UITexture _equipedDeviceIcon;
	
	public Texture texture;
	public Texture texture1;
	public Texture texture2;
	public Texture texture3;
	public Texture texture4;
	public Texture texture5;
	public string textureName1 = "Textures/Backdrop";
	public string textureName2 = "Textures/brick";
	public string textureName3 = "Textures/brickNM";
	public string textureName4 = "Textures/burlap";
	public string textureName5 = "Textures/sand";
	
	private string _uri;
	private bool _isActive;
	private int _deviceID;
	
	
	
	public int getID() {
		return _deviceID;
	}
	
	public static Object prefab = Resources.Load("GUI/screen1/EquipedDevices/EquipedDeviceSlotPrefab");
	public static Device Create(Transform parentTransform, Vector3 localPosition, int deviceID)
	{
	    GameObject newDevice = Instantiate(prefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity) as GameObject;
		newDevice.transform.parent = parentTransform;
		newDevice.transform.localPosition = localPosition;
		newDevice.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		
	    Device yourObject = newDevice.GetComponent<Device>();
		yourObject._deviceID = deviceID;
	 
	    //do additional initialization steps here
	 
	    return yourObject;
	}
	
	public void Remove() {
		Destroy(gameObject);
	}
	
	private void initTextures() {
		if(texture1 == null) {
			texture1 = Resources.Load(textureName1, typeof(Texture)) as Texture;
		}
		if(texture2 == null) {
			texture2 = Resources.Load(textureName2, typeof(Texture)) as Texture;
		}
		if(texture3 == null) {
			texture3 = Resources.Load(textureName3, typeof(Texture)) as Texture;
		}
		if(texture4 == null) {
			texture4 = Resources.Load(textureName4, typeof(Texture)) as Texture;
		}
		if(texture5 == null) {
			texture5 = Resources.Load(textureName5, typeof(Texture)) as Texture;
		}
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
		texture = _equipedDeviceIcon.mainTexture;
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
	
	//TODO clean
	private string getRandomTexture() {
		float random = Random.Range(1, 6);
		if(random == 1) {
			return textureName1;
		} else if(random == 2) {
			return textureName2;
		} else if(random == 3) {
			return textureName3;
		} else if(random == 4) {
			return textureName4;
		} else {
			return textureName5;
		}
	}
	
	// Use this for initialization
	void Start () {
		
		initTextures();
		
		_equipedDeviceIcon = transform.Find ("EquipedDeviceIcon").GetComponent<UITexture>();
		_uri = getRandomTexture();
		setActive();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
