using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Curve
{

  public const int _maxPoints = 200;
  private LinkedList<Vector2> _points;
  private float _minY;
  private float _maxY;
  private Color _color;

  public Curve()
  {
    _points = new LinkedList<Vector2>();
    _minY = 0;
    _maxY = 0;
    _color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
  }

  private float getMinX() { return _points.First.Value.x; }
  private float getMaxX() { return _points.Last.Value.x; }
  public float getMaxY() { return _maxY; }
  public float getMinY() { return _minY; }
  public Color getColor() { return _color; }
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
  }

  public void addPoint(Vector2 pt)
  {
    if (_points.Count >= _maxPoints)
      removeFirstPoint();
    
    _points.AddLast(pt);
  }


}