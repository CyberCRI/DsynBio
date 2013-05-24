using UnityEngine;
using System.Collections;

public class airFlowDown : MonoBehaviour {

	public Hero hero;

	private bool _canFlow;

	void Start () {
		_canFlow = false;
	}
	
	void Update () {
		if (_canFlow == true)
			hero.transform.Translate(0, 0, -10 * Time.deltaTime);
	}

	void OnTriggerEnter(){
		_canFlow = true;
	}

	void OnTriggerExit(){
		_canFlow = false;
	}
}
