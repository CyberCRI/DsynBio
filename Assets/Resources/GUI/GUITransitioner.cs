using UnityEngine;
using System.Collections;

public class GUITransitioner : MonoBehaviour {
	
	private float _timeCounter = 0.0f;
	private float _timeDelta = 0.2f;
	private GameScreen _currentScreen = GameScreen.screen1;
	private enum GameScreen {
		screen1,
		screen2,
		screen3
	};
	
	
	public GameObject _mainCameraObject;
	private Camera _mainCamera;
	private cameraFollow _mainCameraFollow;
	
	public GameObject _worldScreen;
	public GameObject _craftScreen;
	
	// Use this for initialization
	void Start () {
		SetScreen2(false);
		SetScreen3(false);
		SetScreen1(true);
	}
	
	
	private void SetScreen1(bool active) {
		if(active) ZoomOut();
		_worldScreen.SetActive(active);
	}
	
	private void SetScreen2(bool active) {
		if(active) ZoomIn();
		_worldScreen.SetActive(active);
	}
	
	private void SetScreen3(bool active) {
		_craftScreen.SetActive(active);
	}
	
	private void checkCamera() {
		if(_mainCamera == null) {
			_mainCamera = _mainCameraObject.GetComponent<Camera>() as Camera;
		}
		if(_mainCameraFollow == null) {
			_mainCameraFollow = _mainCameraObject.GetComponent<cameraFollow>() as cameraFollow;
		}
	}
	
	private void ZoomIn() {
		checkCamera();
		_mainCameraFollow.zoom = false;
		_mainCamera.fov = 10;		
	}
	private void ZoomOut() {
		checkCamera();
		_mainCameraFollow.zoom = true;
		//_mainCamera.fov = 65;		
	}
		
	
	// Update is called once per frame
	void Update () {
		if(Time.time - _timeCounter > _timeDelta) {
			if (Input.GetKey(KeyCode.Alpha1)) {//GOTO screen1
				if(_currentScreen == GameScreen.screen2) {
					Debug.Log("2->1");
					//2 -> 1
					//set zoom1
					//remove catalog device, deviceID
					//add graphs
					//move devices and potions?
					
					//SetScreen2(false);
					//SetScreen1(true);
					ZoomOut();
					
					_currentScreen = GameScreen.screen1;
				} else if(_currentScreen == GameScreen.screen3) {
					Debug.Log("3->1");					
					//3 -> 1
					//set zoom1
					//remove craft screen
					//add graphs
					//add potions
					//add devices
					//add life/energy
					//add medium info
					SetScreen3(false);
					SetScreen1(true);
					_currentScreen = GameScreen.screen1;
				}
			} else if (Input.GetKey(KeyCode.Alpha2)) {//GOTO screen2
				if(_currentScreen == GameScreen.screen1) {
					Debug.Log("1->2");
					//1 -> 2
					//set zoom2
					//add catalog device, deviceID
					//remove graphs
					//move devices and potions?
					
					//SetScreen1(false);
					//SetScreen2(true);
					ZoomIn();
					
					_currentScreen = GameScreen.screen2;
				} else if(_currentScreen == GameScreen.screen3) {
					Debug.Log("3->2");					
					//3 -> 1
					//set zoom2
					//remove craft screen
					//add catalog device, deviceID
					//add potions
					//add devices
					//add life/energy
					//add medium info
					SetScreen3(false);
					SetScreen2(true);
					_currentScreen = GameScreen.screen2;
				}				
			} else if (Input.GetKey(KeyCode.Alpha3)) {//GOTO screen3
				if(_currentScreen == GameScreen.screen1) {
					Debug.Log("1->3");
					//1 -> 3
					//remove everything
					//add device catalog, parameters
					//remove graphs
					//move devices and potions?
					SetScreen1(false);
					SetScreen3(true);
					_currentScreen = GameScreen.screen3;
				} else if(_currentScreen == GameScreen.screen2) {
					Debug.Log("2->3");					
					//3 -> 1
					//remove everything
					//add craft screen
					SetScreen2(false);
					SetScreen3(true);
					_currentScreen = GameScreen.screen3;
				}				
			}
			_timeCounter = Time.time;
		}
	}
}
