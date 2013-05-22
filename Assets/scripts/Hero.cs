using UnityEngine;
using System.Collections;


public class Hero : MonoBehaviour{

	public Transform mover;
	public Camera cam;
	private Vector3 _destination = Vector3.zero;
	
	// -------------- speed --------------
	private float _moveSpeed = 3.5f;
	public float getMoveSpeed() {return _moveSpeed;}
	public void setMoveSpeed(float moveSpeed) {if (moveSpeed < 0) moveSpeed = 0; _moveSpeed = moveSpeed;}
	//public int moveSpeed = 5;
	//public float smooth = .5f;

	// -------------- collect --------------
	private int _collected;
	public int getCollected() {return _collected;}
	public void setCollected(int collected) {_collected = collected;}
	
	// -------------- atp --------------
	private float _energy = 1f;
	public float getEnergy() {return _energy;}
	public void setEnergy(float energy) {if (energy > 1f) energy = 1f; if(energy < 0) energy = 0; _energy = energy;}

	// -------------- life --------------
	private float _life = 1f;
	public float getLife() {return _life;}
	public void setLife(float life) {if (life > 1f) life = 1; if (life < 0) life = 0; _life = life;}
  
	void Start (){
      	//_destination = mover.position;
	}
  
	void Update(){

		//	-------------- idle player energy drain --------------
		this._energy -= 0.0001f;
		
		// -------------- keys to move --------------
	 	transform.Translate((Input.GetAxis("Horizontal") * this._moveSpeed * Time.deltaTime), 0, (Input.GetAxis("Vertical") * this._moveSpeed * Time.deltaTime));

		// -------------- click to move --------------
		/*
		if (Input.GetMouseButtonDown(0)){
			Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)){
				if (hit.transform.name == "Ground")
				_destination = hit.point;
				//	moving player energy drain
				energy -= 0.02f;
			}
		}
		//mover.position = Vector3.MoveTowards(mover.transform.position, _destination, Time.deltaTime * moveSpeed);
		mover.position = Vector3.Lerp(mover.transform.position, _destination, Time.deltaTime * smooth);
		Debug.DrawRay(mover.transform.position, _destination, Color.red);
		*/
	}

	// -------------- biobricks collect --------------
	public void Collect(){
		this._collected += 1;
	}

	// -------------- glucose collect --------------
	public void winEnergy(){
		this._energy += 0.2f;
	}
}