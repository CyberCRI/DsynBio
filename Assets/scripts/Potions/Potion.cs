using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Potion : MonoBehaviour {
	private static string _normalSuffix = "Normal";
	private static string _hoverSuffix = "Hover";
	private static string _pressedSuffix = "Pressed";
	private static List<string> _spriteNames = new List<string>( new string[] {
		"greaterhealing"
		,"healing"
		,"lesserclaritypotion"
		,"lesserinvulneralbility"
		,"lesserrejuvpotion"
		,"mana"
		,"minorrejuvpotion"
		,"potionofclarity"
		,"potionofdivinity"
		,"potionofrestoration"
		,"rejuvpotion"
		});

	private static float _scale = 0.6687689f;
	private static Vector3 _scaleVector = new Vector3(_scale, _scale, _scale);
	
	
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
		newPotion.transform.localScale = _scaleVector;
		
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
		imageButton.normalSprite =  spriteUri + _normalSuffix;
		imageButton.hoverSprite =   spriteUri + _hoverSuffix;
		imageButton.pressedSprite = spriteUri + _pressedSuffix;
		
		//ugly but necessary (current image is not updated)
		imageButton.target.spriteName = spriteUri + _normalSuffix;
		imageButton.target.MakePixelPerfect();
		
		Debug.Log("setSprite("+spriteUri+"): normalSprite="+imageButton.normalSprite
			+", imageButton.hoverSprite=" + imageButton.hoverSprite
			+", imageButton.pressedSprite=" + imageButton.pressedSprite);
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
