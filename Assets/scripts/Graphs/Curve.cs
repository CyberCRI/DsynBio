using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vectrosity;

public class Curve
{

  public const int _maxPoints = 200;
  private string _label;
  private LinkedList<Vector2> _points;
  private VectorLine _line;
  private Vector2[] _pts;
  private float _minY;
  private float _maxY;
  private Color _color;

  public Curve(string label = "")
  {
    _label = label;
    _points = new LinkedList<Vector2>();
    _pts = new Vector2[_maxPoints];
    _minY = 0;
    _maxY = 0;
    _color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    _line = new VectorLine("test", _pts, _color, null, 2.0f, LineType.Continuous, Joins.Weld);

    _line.Draw();
  }

  public float getLastY() { return _points.Last.Value.y; }
  public float getMinX() { if (_points.Count > 0) return _points.First.Value.x; return 0;}
  public float getMaxX() { if (_points.Count > 0) return _points.Last.Value.x; return 0;}
  public float getMaxY() { return _maxY; }
  public float getMinY() { return _minY; }
  public Color getColor() { return _color; }
  public string getLabel() { return _label; }
  public LinkedList<Vector2> getPointsList() { return _points; }

  public void updateMinMax()
  {
    float max = _points.First.Value.y;
    float min = _points.First.Value.y;

    foreach (Vector2 v in _points)
      {
        if (v.y > max)
          max = v.y;
        if (v.y < min)
          min = v.y;
      }
    _maxY = max;
    _minY = min;
  }
  
  public void removeFirstPoint()
  {
    if (_maxY == _points.First.Value.y || _minY == _points.First.Value.y)
      {
        _points.RemoveFirst();
        updateMinMax();
      }
    else
      _points.RemoveFirst();
  }

  public void addPoint(Vector2 pt)
  {
    if (_points.Count >= _maxPoints)
      removeFirstPoint();
//     Debug.Log("add: "+pt.x+" pt.y="+pt.y);
    _points.AddLast(pt);
  }


  public void updatePts()
  {
    int i = 0;
    Vector2 tmpPt;

    foreach (Vector2 pt in _points)
      {
        tmpPt = pt;
        tmpPt.x -= getMinX();
        _pts[i] = tmpPt;
        i++;
      }    
    _line.drawStart = 0;
    _line.drawEnd = _points.Count - 1;
    _line.Draw();
  }

  public Vector2[] getPts()
  {
    updatePts();
    return _pts;
  }

}