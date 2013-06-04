using UnityEngine;
using System.Collections;

public class AirFlowUp : MonoBehaviour {

	public Hero hero;

	private bool _canFlow;

	void Start () {
		_canFlow = false;
	}
	
	//When the player colides with the flow block, he's propulsed upward.
	void Update () {
		if (_canFlow == true) {hero.transform.Translate(0, 0, 10 * Time.deltaTime);}
	}

	//Set-up an authorization for each gameObjects that might be in collision with the flow block.
	void OnTriggerEnter(Collider col) {
		if (col.gameObject.name == "Hero") {_canFlow = true;}
	}

	void OnTriggerExit() {
		_canFlow = false;
	}

}
