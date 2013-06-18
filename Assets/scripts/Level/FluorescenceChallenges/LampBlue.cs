using UnityEngine;
using System.Collections;

public class LampBlue : MonoBehaviour {

	public Color colorGreen = Color.green;

	void OnTriggerStay(Collider col) {
		if (col.gameObject.name == "Hero") {
			col.gameObject.light.enabled = true;
			col.gameObject.light.color = colorGreen;
		}
		GameObject door = GameObject.Find("DoorGreen");
		door.transform.Translate(0, 0, 1 * Time.deltaTime);
	}

	void OnTriggerExit(Collider col) {
		if (col.gameObject.name == "Hero")
			col.gameObject.light.enabled = false;
	}
}
