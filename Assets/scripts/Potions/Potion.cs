using UnityEngine;
using System.Collections;

public class Potion : MonoBehaviour {
	private static string _normalSuffix = "Normal";
	private static string _hoverSuffix = "Hover";
	private static string _pressedSuffix = "Pressed";
	
	public string _normalSprite;
	public string _hoverSprite;
	public string _pressedSprite;
	
	public static string _prefix = "Textures/Potions/";
	
	public string spriteName1 = "greaterhealing";
	public string spriteName2 = "healing";
	public string spriteName3 = "lesserclarity";
	public string spriteName4 = "lesserinvulnerability";
	public string spriteName5 = "mana";
	public string spriteName6 = "minorrejuv";
	public string spriteName7 = "potionofclarity";
	public string spriteName8 = "potionofdivinity";
	public string spriteName9 = "potionofrestoration";
	public string spriteName0 = "rejuvpotion";
	
	private int _potionID;	
	
	public int getID() {
		return _potionID;
	}
	
	public static Object prefab = Resources.Load("GUI/screen1/Potions/PotionPrefab");
	public static Potion Create(Transform parentTransform, Vector3 localPosition, int potionID)
	{
	    GameObject newPotion = Instantiate(prefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity) as GameObject;
		newPotion.transform.parent = parentTransform;
		newPotion.transform.localPosition = localPosition;
		newPotion.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		
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
		_normalSprite = spriteUri + _normalSuffix;
		_hoverSprite = spriteUri + _hoverSuffix;
		_pressedSprite = spriteUri + _pressedSuffix;
	}
	
	//TODO clean
	private string getRandomSprite() {
		float random = Random.Range(1, 11);
		if(random == 1) {
			return spriteName1;
		} else if(random == 2) {
			return spriteName2;
		} else if(random == 3) {
			return spriteName3;
		} else if(random == 4) {
			return spriteName4;
		} else {
			return spriteName5;
		}
	}
	
	// Use this for initialization
	void Start () {
		
		initSprites();
		
		_potionIcon = transform.Find ("PotionIcon").GetComponent<UISprite>();
		_uri = getRandomSprite();
		setSprite(_uri);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
