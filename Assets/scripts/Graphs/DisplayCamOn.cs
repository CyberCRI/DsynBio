using UnityEngine;
using Vectrosity;

public class DisplayCamOn: MonoBehaviour {
    
  public Camera camToPrint = null;
  public Camera UICamera = null;
  private int hsize, vsize, hloc, vloc;
    
  public Material _mate;

  void Wake (){
    AdjustCamera ();
  }

  void Start()
  {
  }

  void Update (){
    //FIXME : find a better way de faire ca
    GameObject go = GameObject.Find("VectorCam");
    Camera c = VectorLine.GetCamera();
//     Camera c = go.GetComponent<Camera>();
    c.depth = 5;
    
    camToPrint = c;
    AdjustCamera();
  }
  
  void AdjustCamera(){
    Vector3 size = gameObject.transform.localScale;
    Vector3 loc = UICamera.WorldToScreenPoint(gameObject.transform.position);
//     Vector2 max = UICamera.WorldToScreenPoint(gameObject.transform.position + gameObject.transform.position);
//     camToPrint.aspect = size.x / size.y;
    camToPrint.pixelRect = new Rect(loc.x, loc.y, size.x , size.y);
    
    Debug.Log("Size : " + size);
  }
}