using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour{

  public Transform mover;
  public int moveSpeed = 5;
  public float smooth = 0.5f;
  private Vector3 _destination = Vector3.zero;
  
  public static int collected;
  public static float energy = 1f;
  public static float life = 1f;
  public Camera _cam;
  
	void Start (){
      	_destination = mover.position;
	}
  
	void Update(){
		energy -= 0.0001f;
		if (Input.GetMouseButtonDown(0)){
			Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			// did we hit something?
			if (Physics.Raycast(ray, out hit)){
			 	// did we hit the ground?
				if (hit.transform.name == "Ground")
			 	//set the destinatin to the vector3 where the ground was contacted
				_destination = hit.point;
				energy -= 0.02f;
			}
		}
		// move toward destination
		//mover.position = Vector3.MoveTowards(mover.transform.position, _destination, Time.deltaTime * moveSpeed);

		// move toward destination with smooth
		mover.position = Vector3.Lerp(mover.transform.position, _destination, Time.deltaTime * smooth);
		// debug rayon
		Debug.DrawRay(mover.transform.position, _destination, Color.red);
	}

	public static void Collect(){
		collected +=1;
	}

	public static void winEnergy(){
		energy += 0.2f;
		if (energy > 1.0f)
			energy = 1.0f;
		if (energy < 0f)
			energy = 0f;
	}
}