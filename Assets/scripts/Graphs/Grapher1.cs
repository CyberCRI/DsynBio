using UnityEngine;
using System.Collections;
using System;

public class Grapher1 : MonoBehaviour {

  public const int MAX_RESOLUTION = 1000;
  public const int MIN_RESOLUTION = 2;
  
  public enum eFunctionType
  {
    Linear,
    Tanh,
    Exponential,
    Parabola,
    Sine
  }

  private delegate float functionDelegate (float x);
  private static functionDelegate[] funcTab =
  {
    linear,
    tanh,
    exponential,
    parabola,
    sine
  };

  public eFunctionType _functionType;
  public int _resolution;
  private int _currentResolution;
  private ParticleSystem.Particle[] _points;


  private static float linear(float x) { return x; }
  private static float tanh(float x) { return (float)Math.Tanh(x); }
  private static float exponential (float x) { return x * x; }
  private static float parabola (float x) { x = 2f * x - 1f; return x * x; }
  private static float sine (float x) { return 0.5f + 0.5f * Mathf.Sin(2 * Mathf.PI * x + Time.timeSinceLevelLoad); }

  private void createPoints ()
  {
    _currentResolution = _resolution;
    _points = new ParticleSystem.Particle[_resolution];
    float increment = 1f / (_resolution - 1);
    for(int i = 0; i < _resolution; i++)
      {
        float x = i * increment;
        _points[i].position = new Vector3(x, 0f, 0f);
        _points[i].color = new Color(x, 0f, 0f);
        _points[i].size = 0.1f;
      }
  }

  // Use this for initialization
  void Start () {
    createPoints();
  }
  
  // Update is called once per frame
  void Update () {
    if (_resolution < MIN_RESOLUTION)
      _resolution = MIN_RESOLUTION;
    if (_resolution > MAX_RESOLUTION)
      _resolution = MAX_RESOLUTION;
      if (_currentResolution != _resolution)
        createPoints();

      for (int i = 0; i < _resolution; i++)
        {
          Vector3 p = _points[i].position;
          p.y = funcTab[(int)_functionType](p.x - 1f);
          _points[i].position = p;
        }

    particleSystem.SetParticles(_points, _points.Length);
  }
}
