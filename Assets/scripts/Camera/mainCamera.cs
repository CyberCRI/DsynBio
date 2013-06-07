using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {

  //public float cameraDistanceMax;
  //public float cameraDistanceMin;
  //public float scrollSpeed;

  public float cameraDistance;
  public Transform target;

	void LateUpdate(){
  //Zoom in & out with the scrollwheel.
  //  cameraDistance += Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
  //  cameraDistance = Mathf.Clamp(cameraDistance, cameraDistanceMin, cameraDistanceMax);

    Vector3 temp = target.position;
    temp.y += cameraDistance;
    transform.position = temp;
	}
}
