using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

	public Hero hero;

	private bool _isBlocking;

	void Start () {
		_isBlocking = false;
	}
	
	void Update () {
		if (_isBlocking == true)
			hero.transform.Translate(0, 0, 0);
	}

	void OnTriggerEnter(){
		_isBlocking = true;
	}

	void OnTriggerExit(){
		_isBlocking = false;
	}
}
