using UnityEngine;
using System.Collections;

public class Toxic : MonoBehaviour {

	public static bool drainLife;

	void Start () {
		drainLife = false;
	}
	
	void Update () {
		if (drainLife == true)
			Hero.life -= Time.deltaTime * .3f;
	}

	void OnTriggerEnter(){
		drainLife = true;
		Debug.Log("Enter");
	}

	void OnTriggerExit(){
		drainLife = false;
		Debug.Log("Exit");
	}
}
