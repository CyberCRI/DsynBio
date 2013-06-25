using UnityEngine;
using System.Collections;

public class ToxicScript : MonoBehaviour {

	public const float toxicDrain = .3f;


	void OnTriggerStay(Collider col) {
		if (col.gameObject.name == "Hero") 
			col.gameObject.SendMessage("subLife", Time.deltaTime * toxicDrain);
	}

}
