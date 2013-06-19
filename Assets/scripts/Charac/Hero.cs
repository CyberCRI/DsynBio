using UnityEngine;
using System.Collections;


public class Hero : MonoBehaviour{

	public Transform mover;
	public Camera mainCam;
	
	//Getter & setter for the move speed.
	private float _moveSpeed = 3f;
	public float getMoveSpeed() {
		return _moveSpeed;
	}
	public void setMoveSpeed(float moveSpeed) {
		if (moveSpeed < 0)
			moveSpeed = 0; 
			_moveSpeed = moveSpeed;
	}
	
	//References for the click to move controler.
	//	private float _moveSmooth = .5f;
	//	private Vector3 _destination = Vector3.zero;

	//Getter & setter for the inventory.
	private int _collected;
	public int getCollected() {
		return _collected;
	}
	public void setCollected(int collected) {
		_collected = collected;
	}
	
	//Getter & setter for the energy.
	private float _energy = 1f;
	public float getEnergy() {
		return _energy;
	}
	public void setEnergy(float energy) {
		if (energy > 1f) {energy = 1f;}
		if(energy < 0) 
			energy = 0; 
			_energy = energy;
	}

	//Getter & setter for the life.
	private float _life = 1f;
	public float getLife() {
		return _life;
	}
	public void setLife(float life) {
		if (life > 1f)
			life = 1;
		if (life < 0) {
			life = 0;
			gameObject.SetActive(false);
			Debug.Log("Game Over!");
		}
		_life = life;
	}

	public void subLife(float life) {
		_life -= life;
	}
  
	void Start (){
		//Click to move variable.
      	//	_destination = mover.position;
      	gameObject.SetActive(true);
      	
      	light.enabled = false;
	}
  
	void Update(){

		//When the player is idle his energy gets drained.
		setEnergy(getEnergy() - Time.deltaTime * .01f);
		setLife(getLife() + Time.deltaTime * .1f);
		
		//Keyboard controls.
	 	transform.Translate((Input.GetAxis("Horizontal") * _moveSpeed * Time.deltaTime), 0, (Input.GetAxis("Vertical") * _moveSpeed * Time.deltaTime));

		//Click to move controls.
		//	if (Input.GetMouseButtonDown(0)){
		//		Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
		//		RaycastHit hit;
		//		if (Physics.Raycast(ray, out hit)) {if (hit.transform.name == "Ground") _destination = hit.point;}
		//	}
		//	mover.position = Vector3.Lerp(mover.transform.position, _destination, Time.deltaTime * _moveSmooth);
		//	Debug.DrawRay(mover.transform.position, _destination, Color.red);
	}

	//When the player collects a biobrick.
	public void Collect() {
		setCollected(getCollected() + 1);
	}

	//When the player collects glucose.
	public void winEnergy() {
		setEnergy(getEnergy() + .2f);
	}

	//When the player equiped the improved motility device.
	public void equipImpMoti() {
		setMoveSpeed(getMoveSpeed() + 3f);
	}

	//When the player reacts to the light and emits colors.
	public void emitLight(bool boolean) {
		light.enabled = boolean;
	}

	public void changeColor(Color color) {
		light.color = color;
	}

}