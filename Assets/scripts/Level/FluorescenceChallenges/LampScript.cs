using UnityEngine;
using System.Collections;

public class LampScript : MonoBehaviour {

	void Start() {
		if (gameObject.name == "lampGreen")
			gameObject.light.color = Color.green;
		if (gameObject.name == "lampRed")
			gameObject.light.color = Color.red;
		if (gameObject.name == "lampBlue")
			gameObject.light.color = Color.blue;
	}

	void OnTriggerEnter(Collider col) {
		//Tbd: device condition to set the color.
		if (col.gameObject.name == "Hero" && gameObject.name == "lampBlue") {
			Hero hero = GameObject.Find("Hero").GetComponent<Hero>();
			hero.emitLight(true);
			hero.changeColor(Color.green);

			DoorScript door = GameObject.Find("doorGreen").GetComponent<DoorScript>();
			door.openDoor();
		}
		if (col.gameObject.name == "Hero" && gameObject.name == "lampGreen") {
			Hero hero = GameObject.Find("Hero").GetComponent<Hero>();
			hero.emitLight(true);
			hero.changeColor(Color.red);

			DoorScript door = GameObject.Find("doorRed").GetComponent<DoorScript>();
			door.openDoor();
		}
		if (col.gameObject.name == "Hero" && gameObject.name == "lampBlack") {
			Hero hero = GameObject.Find("Hero").GetComponent<Hero>();
			hero.emitLight(true);
			hero.changeColor(Color.green);
		}
	}

	void OnTriggerExit(Collider col) {
		if (col.gameObject.name == "Hero") {//	&& gameObject.name == "LampBlue") {
			Hero hero = GameObject.Find("Hero").GetComponent<Hero>();
			hero.emitLight(false);
			//Name the correct game object to make sure it doesn't open all the green doors.
			DoorScript door = GameObject.Find("doorGreen").GetComponent<DoorScript>();
			door.closeDoor();
		}
	}

}