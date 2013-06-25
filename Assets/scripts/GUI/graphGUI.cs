using UnityEngine;
using System.Collections;
using Vectrosity;

public class graphGUI : MonoBehaviour
{
  
  private VectorLine rings;
  
  // START
  void Start()
  {
    
    // Vectrosity Lines & Points; Drawn later with MakeCircle
    rings = new VectorLine("Ring1", new Vector2[32], Color.white, null, 4, LineType.Continuous);
  }
 
  // GUI
  void OnGUI () 
  {
    float mousex = Event.current.mousePosition.x;
    float mousey = Event.current.mousePosition.y;
    // Ring
    rings.MakeCircle(new Vector2(mousex, Screen.height - mousey), 30);
    rings.Draw();
    
    // Draw text
    Vector2 textSize = GUI.skin.label.CalcSize(new GUIContent("Test"));
    GUI.Label (new Rect(mousex - (textSize.x / 2), (mousey - 15), textSize.x, 20), "Test"
               );
  }
}
