using UnityEngine;
using System.Collections;

public class Glucose : MonoBehaviour {

	public Hero hero;

	public Vector3 rotationVelocity;

	void Start(){
		gameObject.SetActive(true);
	}

	void Update(){
		transform.Rotate(rotationVelocity * Time.deltaTime);
	}

	void OnTriggerEnter(){
		hero.winEnergy();
		gameObject.SetActive(false);
	}
}
