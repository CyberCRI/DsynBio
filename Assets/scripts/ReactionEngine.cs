using UnityEngine;
using System.Collections;
using System.Xml;
using System;
using System.Collections.Generic;

public class ReactionEngine : MonoBehaviour {

  public TextAsset _reactionFile;
  private FileLoader _fileLoader;

  private LinkedList<IReaction> _reactions;
  private ArrayList             _molecules;

  public ReactionEngine()
  {
    _fileLoader = new FileLoader();
  }


  public static Molecule        getMoleculeFromName(string name, ArrayList molecules)
  {
    foreach (Molecule mol in molecules)
      if (mol.getName() == name)
        return mol;
    return null;
 }

	// Use this for initialization
  void Start () {
    _fileLoader.loadReactionsFromFile(_reactionFile);
    _reactions = _fileLoader.getReactions();
    _molecules = _fileLoader.getMolecules();
  }
  
  // Update is called once per frame
  void Update () 
  {
    foreach (IReaction reaction in _reactions)
      {
        reaction.react(_molecules);
      }
  }
}
