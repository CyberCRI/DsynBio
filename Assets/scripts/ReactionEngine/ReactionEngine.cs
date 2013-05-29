using UnityEngine;
using System.Collections;
using System.Xml;
using System;
using System.Collections.Generic;

public class ReactionEngine : MonoBehaviour {

  public TextAsset _reactionFile;
  public GraphDrawer _graphic;
  private FileLoader _fileLoader;

  private LinkedList<IReaction> _reactions;
  private ArrayList             _molecules;
  
  // Graphic Stuff
  private LinkedList<Curve>    _curves;

  public ReactionEngine()
  {
    _fileLoader = new FileLoader();

    _curves = new LinkedList<Curve>();
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

//     graphic stuff
    for (int i = 0; i < _molecules.Count; i++)
      {
        Curve c = new Curve();
        _curves.AddLast(c);
        _graphic.addCurve(c);
      }
  }

  //   private int _i = 0;
  // Update is called once per frame
  void Update ()
  {
    foreach (IReaction reaction in _reactions)
        reaction.react(_molecules);

//     Vector2 p = default(Vector2);
//     Time.fixedDeltaTime = 0.0000000002f;
    //FIXME : To delete
//     _i = 0;
//     if (_i % 100 == 0)
//       {
    if (Input.GetKey (KeyCode.UpArrow))
      {
        Molecule mole = ReactionEngine.getMoleculeFromName("X", _molecules);
        mole.setConcentration(mole.getConcentration() + 0.2f);
      }
    if (Input.GetKey (KeyCode.DownArrow))
      {
        Molecule mole = ReactionEngine.getMoleculeFromName("X", _molecules);
        mole.setConcentration(mole.getConcentration() - 0.2f);
      }
    if (Input.GetKey (KeyCode.LeftArrow))
      {
        Molecule mole = ReactionEngine.getMoleculeFromName("Y", _molecules);
        mole.setConcentration(mole.getConcentration() - 0.2f);
      }
    if (Input.GetKey (KeyCode.RightArrow))
      {
        Molecule mole = ReactionEngine.getMoleculeFromName("Y", _molecules);
        mole.setConcentration(mole.getConcentration() + 0.2f);
      }

        LinkedListNode<Curve> node = _curves.First;
        foreach (Molecule mol in _molecules)
          {
            Vector2 p = new Vector2((float)Time.timeSinceLevelLoad*200f, mol.getConcentration() *100f);
            node.Value.addPoint(p);
//             node.Value.updatePts();
            node = node.Next;
          }
//       }
//     _i++;
  }
}
