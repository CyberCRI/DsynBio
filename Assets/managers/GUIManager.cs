using UnityEngine;

public class GUIManager : MonoBehaviour {

	public Texture2D atpBgImage;
	public Texture2D atpFgImage;
	public Texture2D lifeBgImage;
	public Texture2D lifeFgImage;

	void OnGUI(){
		// top-right
		GUI.BeginGroup (new Rect (0, 0, 200, 110));
		GUI.Box (new Rect (0, 0, 200, 120), "Gauges");
			// ATP
			GUI.BeginGroup (new Rect (10, 60, 180, 32));
				GUI.Box (new Rect (0, 0, 180, 32), atpBgImage);
				GUI.BeginGroup (new Rect (0, 0, Hero.energy * 180, 32));
					GUI.Box (new Rect (0, 0, 180, 32), atpFgImage);
				GUI.EndGroup ();
			GUI.EndGroup ();
			// HP
			GUI.BeginGroup (new Rect (10, 30, 180, 32));
				GUI.Box (new Rect (0, 0, 180, 32), lifeBgImage);
				GUI.BeginGroup (new Rect (0, 0, Hero.life * 180, 32));
					GUI.Box (new Rect (0, 0, 180, 32), lifeFgImage);
				GUI.EndGroup ();
			GUI.EndGroup ();
		GUI.EndGroup ();

		// top middle
		if(GUI.Button(new Rect((Screen.width / 2 - 170), 0, 100, 50), "Game")) {
			
		}
		if(GUI.Button(new Rect((Screen.width / 2 - 50), 0, 100, 50), "Bacterium")) {

		}
		if(GUI.Button(new Rect((Screen.width / 2 + 70), 0, 100, 50), "Lab")) {

		}

		// top-left
		GUI.BeginGroup (new Rect (Screen.width - 200, 0, 200, Screen.height / 2));
			GUI.Box (new Rect (0, 0, 200, Screen.height / 2), "Devices equiped");
		GUI.EndGroup();

		// bot-right panel
		GUI.BeginGroup (new Rect (Screen.width - 200, Screen.height - 300, 200, 300));
			GUI.Box (new Rect (0, 0, 200, 300), "Monitoring tool");
		GUI.EndGroup();

		// bot-left panel
		GUI.BeginGroup (new Rect (0, Screen.height - 300, 200, 300));
			GUI.Box (new Rect (0, 0, 200, 300), "Inventory");
			GUI.Box(new Rect (10, 30, 180, 24), ("Biobricks collected: " + Hero.collected));
		GUI.EndGroup();
	}

}
