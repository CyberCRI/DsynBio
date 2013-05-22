using UnityEngine;
using System.Collections;

public class Toxic : MonoBehaviour {

	public Hero hero;

	private bool _drainLife;

	void Start () {
		_drainLife = false;
	}
	
	void Update () {
		if (_drainLife == true)
			hero.setLife(hero.getLife() - Time.deltaTime * .8f);
		if (_drainLife == false) 
			hero.setLife(hero.getLife() + Time.deltaTime * .1f);
	}

	void OnTriggerEnter(){
		_drainLife = true;
	}

	void OnTriggerExit(){
		_drainLife = false;
	}
}
