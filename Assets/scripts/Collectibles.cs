using UnityEngine;
using System.Collections;

public class Collectibles : MonoBehaviour {

	public Vector3 rotationVelocity;

	void Start(){
		gameObject.SetActive(true);
	}

	void Update(){
		transform.Rotate(rotationVelocity * Time.deltaTime);
	}

	void OnTriggerEnter(){
		Hero.Collect();
		gameObject.SetActive(false);
	}
}
