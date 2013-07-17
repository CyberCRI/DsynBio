using UnityEngine;
using System.Collections;

public class MockupScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		UILabel label = gameObject.GetComponent<UILabel>() as UILabel;
		label.text = "craft screen";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
