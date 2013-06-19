using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

	public Vector2 _closePosition;
	public Vector2 _openPosition;

	//	private bool _isOpen;
	private Vector2 _direction;
	private Vector2 _endPosition;
	private Vector2 _startPosition;

	private float _doorSpeed;

	// Use this for initialization
	void Start () {
		_direction = new Vector2();
		_endPosition.x = transform.position.x;
		_endPosition.y = transform.position.z;
		_startPosition.x = transform.position.x;
		_startPosition.y = transform.position.z;
		_doorSpeed = .5f;
		//	_isOpen = true;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate((_endPosition.x - transform.position.x) * Time.deltaTime * _doorSpeed, 0, (_endPosition.y - transform.position.z) * Time.deltaTime * _doorSpeed);
	}

	public void openDoor() {
		_direction.x = _closePosition.x - _openPosition.x;
		_direction.y = _closePosition.y - _openPosition.y;
		_endPosition = _closePosition;
		_startPosition = _openPosition;
		//	_isOpen = false;
	}

	public void closeDoor() {
		_direction.x = _openPosition.x - _closePosition.x;
		_direction.y = _openPosition.y - _closePosition.y;
		_startPosition = _closePosition;
		_endPosition = _openPosition;
		//	_isOpen = true;
	}

}