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
		if (drainLife == false && Hero.life < 1f)
			Hero.life += Time.deltaTime * .1f;
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
