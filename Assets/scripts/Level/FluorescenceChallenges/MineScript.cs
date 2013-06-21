using UnityEngine;
using System.Collections;

public class MineScript : MonoBehaviour {

	public float duration = 1.0F;

	// Use this for initialization
	void Start () {
		light.enabled = false;
		light.color = Color.red;
	}

	void OnTriggerStay(Collider col) {
		if (col.gameObject.name == "Hero" && col.light.enabled == true) {
			light.enabled = true;
			float phi = Time.time / duration * 4 * Mathf.PI;
        	float amplitude = Mathf.Cos(phi) * .5f + .5f;
        	light.intensity = amplitude;
        }
	}

	void OnTriggerExit(Collider col) {
		if (col.gameObject.name == "Hero")
			light.enabled = false;
	}
}
