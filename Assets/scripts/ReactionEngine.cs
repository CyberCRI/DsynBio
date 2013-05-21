using UnityEngine;
using System.Collections;
using System.Xml;
using System;
using System.Collections.Generic;

public class ReactionEngine : MonoBehaviour {

  public TextAsset _reactionFile;
  private FileLoader _fileLoader;

  public ReactionEngine()
  {
    _fileLoader = new FileLoader();
  }


	// Use this for initialization
  void Start () {
    _fileLoader.loadReactionsFromFile(_reactionFile);
  }
  
  // Update is called once per frame
  void Update () {
  }
}
