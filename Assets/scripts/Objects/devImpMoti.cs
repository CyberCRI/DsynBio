using UnityEngine;
using System.Collections;

public class DevImpMoti : MonoBehaviour {

	public Hero hero;

	public Vector3 rotationVelocity;

	void Start(){
		gameObject.SetActive(true);
	}

	//The device rotates on itself.
	void Update(){
		transform.Rotate(rotationVelocity * Time.deltaTime);
	}

	void OnTriggerEnter(){
		hero.equipImpMoti();
		gameObject.SetActive(false);
	}
}
