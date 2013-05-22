using UnityEngine;

public class GUIManager : MonoBehaviour {

	public Hero hero;

	public Texture2D atpBgImage;
	public Texture2D atpFgImage;
	public Texture2D lifeBgImage;
	public Texture2D lifeFgImage;

	private bool _characPanel;
	private bool _labPanel;

	void Start(){
		_characPanel = false;
		_labPanel = false;
	}

	void OnGUI(){
		// top-right
		GUI.BeginGroup (new Rect (0, 0, 200, 110));
		GUI.Box (new Rect (0, 0, 200, 120), "Gauges");
			// ATP
			GUI.BeginGroup (new Rect (10, 60, 180, 32));
				GUI.Box (new Rect (0, 0, 180, 32), atpBgImage);
				GUI.BeginGroup (new Rect (0, 0, hero.getEnergy() * 180, 32));
					GUI.Box (new Rect (0, 0, 180, 32), atpFgImage);
				GUI.EndGroup ();
			GUI.EndGroup ();
			// HP
			GUI.BeginGroup (new Rect (10, 30, 180, 32));
				GUI.Box (new Rect (0, 0, 180, 32), lifeBgImage);
				GUI.BeginGroup (new Rect (0, 0, hero.getLife() * 180, 32));
					GUI.Box (new Rect (0, 0, 180, 32), lifeFgImage);
				GUI.EndGroup ();
			GUI.EndGroup ();
		GUI.EndGroup ();

		// top middle
		if(GUI.Button(new Rect((Screen.width / 2 - 170), 0, 100, 50), "Game")) {
			Debug.Log("test game button");
			_characPanel = false;
			_labPanel = false;
		}
		if(GUI.Button(new Rect((Screen.width / 2 - 50), 0, 100, 50), "Bacterium")) {
			_characPanel = true;
			_labPanel = false;
		}
		if(GUI.Button(new Rect((Screen.width / 2 + 70), 0, 100, 50), "Lab")) {
			_labPanel = true;
			_characPanel = false;

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
			GUI.Box(new Rect (10, 30, 180, 24), ("Biobricks collected: " + hero.getCollected()));
		GUI.EndGroup();

		// charac panel
		if (_characPanel == true){
			GUI.BeginGroup (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 300, 400, 600));
				GUI.Box (new Rect (0, 0, 400, 600), "Bacterium summary");
				GUI.Box (new Rect (30, 40, 180, 24), ("Initial speed: " + hero.getMoveSpeed()));
			GUI.EndGroup();
		}

		// lab panel
		if (_labPanel == true)
			GUI.Box (new Rect (Screen.width / 2 - 500, Screen.height / 2 - 300, 1000, 600), "Lab");

	}

}
