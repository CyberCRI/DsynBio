using UnityEngine;
using System.Collections;

public class Collectibles : MonoBehaviour {

	public Hero hero;

	public Vector3 rotationVelocity;

	void Start(){
		gameObject.SetActive(true);
	}

	void Update(){
		transform.Rotate(rotationVelocity * Time.deltaTime);
	}

	void OnTriggerEnter(){
		hero.Collect();
		gameObject.SetActive(false);
	}
}
