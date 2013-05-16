using UnityEngine;
using System.Collections;

public class mainCamera : MonoBehaviour {

	public float cameraDistanceMax;
	public float cameraDistanceMin;
	public float cameraDistance;
	public float scrollSpeed;
 
	void Update(){
	    // cameraDistance += Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
	    // cameraDistance = Mathf.Clamp(cameraDistance, cameraDistanceMin, cameraDistanceMax);
	    // Debug.Log("ca marcheeeee pas");

	}
}
