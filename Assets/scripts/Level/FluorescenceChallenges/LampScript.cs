using UnityEngine;
using System.Collections;

public class LampScript : MonoBehaviour {

	private Color _colorGreen = Color.green;
	private Color _colorRed = Color.red;
	private Color _colorBlue = Color.blue;
	private Color _colorReset = Color.white;

	void Start() {
		if (gameObject.name == "LampGreen")
			gameObject.light.color = _colorGreen;
		if (gameObject.name == "LampRed")
			gameObject.light.color = _colorRed;
		if (gameObject.name == "LampBlue")
			gameObject.light.color = _colorBlue;
	}

	void OnTriggerEnter(Collider col) {
		//Tbd: device condition to set the color.
		if (col.gameObject.name == "Hero" && gameObject.name == "LampBlue") {
			Hero hero = GameObject.Find("Hero").GetComponent<Hero>();
			hero.emitLight(true, _colorGreen);

			DoorScript door = GameObject.Find("DoorGreen").GetComponent<DoorScript>();
			door.openDoor();
		}
		if (col.gameObject.name == "Hero" && gameObject.name == "LampGreen") {
			Hero hero = GameObject.Find("Hero").GetComponent<Hero>();
			hero.emitLight(true, _colorRed);

			//	DoorScript door = GameObject.Find("DoorRed").GetComponent<DoorScript>();
			//	door.openDoor();
		}
	}

	void OnTriggerExit(Collider col) {
		if (col.gameObject.name == "Hero") {//	&& gameObject.name == "LampBlue") {
			Hero hero = GameObject.Find("Hero").GetComponent<Hero>();
			hero.emitLight(false, _colorReset);
			//Name the correct game object to make sure it doesn't open all the green doors.
			DoorScript door = GameObject.Find("DoorGreen").GetComponent<DoorScript>();
			door.closeDoor();
		}
	}

}