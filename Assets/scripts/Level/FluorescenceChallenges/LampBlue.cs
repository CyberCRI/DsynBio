using UnityEngine;
using System.Collections;

public class LampBlue : MonoBehaviour {

	public Color colorGreen = Color.green;

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.name == "Hero") {
			col.gameObject.light.enabled = true;
			col.gameObject.light.color = colorGreen;
		}
		//GameObject door = GameObject.Find("DoorGreen");
		//door.transform.Translate(0, 0, 2 * Time.deltaTime);
		DoorScript door = GameObject.Find("DoorGreen").GetComponent<DoorScript>();
		//door.toggleState();
		door.openDoor();
	}

	void OnTriggerExit(Collider col) {
		if (col.gameObject.name == "Hero")
			col.gameObject.light.enabled = false;
		DoorScript door = GameObject.Find("DoorGreen").GetComponent<DoorScript>();
		//door.toggleState();
		door.closeDoor();
	}
}
