using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour{

  public Transform mover;
  public float moveSpeed = .5f;
  //public float smooth = .5f;
  private Vector3 _destination = Vector3.zero;
  
  public static int collected;
  public static float energy = 1f;
  public static float life = 1f;
  public Camera _cam;
  
	void Start (){
      	_destination = mover.position;
	}
  
	void Update(){
		//	idle player energy drain
		energy -= 0.0001f;
		
	 	/* 
		-------------- keys to move --------------
		*/
	 	transform.Translate((Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime), 0, (Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime));

		/*
		-------------- click to move --------------
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

	// executed when the player collect a biobrick
	public static void Collect(){
		collected +=1;
	}

	// executed when the player collects glucose
	public static void winEnergy(){
		energy += 0.2f;
		if (energy > 1.0f)
			energy = 1.0f;
		if (energy < 0f)
			energy = 0f;
	}
}