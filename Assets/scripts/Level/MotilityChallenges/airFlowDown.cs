using UnityEngine;
using System.Collections;

public class AirFlowDown : MonoBehaviour {

	public Hero hero;

	private bool _canFlow;
	private bool _flowPlayer;

	void Start () {
		_canFlow = true;
		_flowPlayer = false;
	}
	
	//When the player colides with the flow block, he's propulsed downward.
	void Update () {
		if (_canFlow == true)
			if (_flowPlayer == true)
				hero.transform.Translate(0, 0, -5 * Time.deltaTime);
	}

	//Set-up an authorization for each gameObjects that might be in collision with the flow block.
	void OnTriggerEnter(Collider col) {
		if (col.gameObject.name == "Hero") {_flowPlayer = true;}
		//If the player blocks the flow block with a rock, it disables it and its particles.
		if (col.gameObject.name == "Rock") {
			_canFlow = false;
			particleSystem.Stop();
		}
	}

	void OnTriggerExit(Collider col) {
		if (col.gameObject.name == "Hero") {_flowPlayer = false;}
		if (col.gameObject.name == "Rock") {
			_canFlow = true;
			particleSystem.Play();
		}
	}

}