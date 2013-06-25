using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vectrosity;

public class GraphDrawer : MonoBehaviour
{
    private LinkedList<Curve> _curves;
 
 public GraphDrawer()
  {
    _curves = new LinkedList<Curve>();
  }

  public void addCurve(Curve curve)
  {
    _curves.AddLast(curve);
  }

  public void Start()
  {
  }

  public void Update()
  {
    foreach (Curve c in _curves)
        c.updatePts();
  }

  public void OnGUI()
  {
    foreach (Curve c in _curves)
      GUI.Label (new Rect(c.getMaxX() - c.getMinX(), Screen.height - c.getLastY(), 50, 20), c.getLabel());
  }
}