using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vectrosity;

public class LineToPanel : MonoBehaviour {
	
	public Camera GUICam;
	public bool draw = true;
	public float padding = 20f;
	public PanelInfos infos;
	public List<Line> line {get{return _lines;}}
	
	private List<Line> _lines;
	private List<float> _values;
	private float v = 0;
	private int nb = 0;
	
	// Use this for initialization
	void Start () {
		infos = new PanelInfos();
		refreshInfos();
		
		VectorLine.SetCamera3D(GUICam);		
		
		_lines = new List<Line>();
		_values = new List<float>();
		for(int i = 0; i < 20; i++){
			_lines.Add(new Line(200, 800, infos));
			_values.Add(0f);
		}
		drawLines(true);
	}
	
	// Update is called once per frame
	void Update () {
		bool resize = refreshInfos();
		drawLines(resize);
	}
	
	void drawLines(bool resize) {
		int i = 0;
		foreach(Line line in _lines){
			_values[i] += Random.Range(-50, 50);
			_values[i] = Mathf.Clamp(_values[i], 0, 850);
			if(resize) line.resize();
			if(draw)
				line.addPoint(_values[i]);
			else
				line.addPoint();
			line.redraw();
			i++;
		}
	}
	
	public bool refreshInfos(){
		bool changed = false;
		if(infos.layer != gameObject.layer){
			infos.layer = gameObject.layer;
			changed = true;
		}
		if(infos.padding != padding){
			infos.padding = padding;
			changed = true;
		}
		if(infos.panelDimensions != collider.bounds.size){
			infos.panelDimensions = collider.bounds.size;
			changed = true;
		}
		if(infos.panelPos != transform.position){
			infos.panelPos = transform.position;
			changed = true;
		}
		
		return changed;
	}
}
