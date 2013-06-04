using UnityEngine;
using System.Collections;

public class Collectibles : MonoBehaviour {

	public Hero hero;

	public Vector3 rotationVelocity;

	void Start(){
		gameObject.SetActive(true);
	}

	//The collectible rotates on itself.
	void Update(){
		transform.Rotate(rotationVelocity * Time.deltaTime);
	}

	void OnTriggerEnter(){
		hero.Collect();
		gameObject.SetActive(false);
	}
}
