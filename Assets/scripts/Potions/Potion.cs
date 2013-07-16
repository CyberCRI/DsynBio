using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Potion : MonoBehaviour {
	private static string _normalSuffix = "Normal";
	private static string _hoverSuffix = "Hover";
	private static string _pressedSuffix = "Pressed";	
	private static string _prefix = "Textures/Potions/";	
	private static List<string> _spriteNames = new List<string>( new string[] {
		//works
		/*
		"Dark",
		"Light",
		"Button"
		*/
		//fails
		//TODO investigate in UISprite when mSpriteName is modified
		
		"greaterhealing"
		,"healing"
		,"lesserclarity"
		,"lesserinvulnerability"
		,"mana"
		,"minorrejuv"
		,"potionofclarity"
		,"potionofdivinity"
		,"potionofrestoration"
		,"rejuvpotion"
		});

	
	private static Vector3 _scale = new Vector3(0.2812413f, 1.103634f, 1.0f);
	
	
	private int _potionID;	
	public string _uri;
	
	
	public int getID() {
		return _potionID;
	}
	
	public static Object prefab = Resources.Load("GUI/screen1/Potions/PotionPrefab");
	public static Potion Create(Transform parentTransform, Vector3 localPosition, int potionID)
	{
		Debug.Log("create potion "+potionID);
	    GameObject newPotion = Instantiate(prefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity) as GameObject;
		newPotion.transform.parent = parentTransform;
		newPotion.transform.localPosition = localPosition;
		newPotion.transform.localScale = _scale;
		
	    Potion yourObject = newPotion.GetComponent<Potion>();
		yourObject._potionID = potionID;
	 
	    //do additional initialization steps here
	 
	    return yourObject;
	}
	
	public void Remove() {
		Destroy(gameObject);
	}
	
	public void Redraw(Vector3 newLocalPosition) {
		gameObject.transform.localPosition = newLocalPosition;
	}
	
	private void setSprite(string spriteUri) {
		UIImageButton imageButton = gameObject.GetComponent<UIImageButton>() as UIImageButton;
		imageButton.normalSprite = _prefix + spriteUri + _normalSuffix;
		imageButton.hoverSprite = _prefix + spriteUri + _hoverSuffix;
		imageButton.pressedSprite = _prefix + spriteUri + _pressedSuffix;
		
		Debug.Log("setSprite("+spriteUri+"): normalSprite="+imageButton.normalSprite
			+", imageButton.hoverSprite=" + imageButton.hoverSprite
			+", imageButton.pressedSprite" + imageButton.pressedSprite);			
	}
	
	//TODO clean
	private string getRandomSprite() {
		int randomIndex = Random.Range(0, _spriteNames.Count);
		return _spriteNames[randomIndex];
	}
	
	// Use this for initialization
	void Start () {
		Debug.Log("start potion "+_potionID);
		_uri = getRandomSprite();
		setSprite(_uri);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
