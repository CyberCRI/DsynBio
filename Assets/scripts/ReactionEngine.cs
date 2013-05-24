using UnityEngine;
using System.Collections;
using System.Xml;
using System;
using System.Collections.Generic;

public class ReactionEngine : MonoBehaviour {

  public TextAsset _reactionFile;
//   public Graph       _graphic;
  private FileLoader _fileLoader;

  private LinkedList<IReaction> _reactions;
  private ArrayList             _molecules;
  
  // Graphic Stuff
//   private LinkedList<Curve>    _curves;

  public ReactionEngine()
  {
    _fileLoader = new FileLoader();

    // Graphic stuffs
//     _curves = new LinkedList<Curve>();
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

    //graphic stuff
//     for (int i = 0; i < _molecules.Count; i++)
//       {
//         Curve c = new Curve();
//         _curves.AddLast(c);
//         _graphic.addCurve(c);
//       }
  }
  
  // Update is called once per frame
  void Update () 
  {
    foreach (IReaction reaction in _reactions)
        reaction.react(_molecules);

//     Vector2 p = default(Vector2);

    //FIXME : To delete
//     int i = 0;
//     if (i % 10 == 0)
//       {
//         LinkedListNode<Curve> node = _curves.First;
//         foreach (Molecule mol in _molecules)
//           {
//             p = new Vector2((float)Time.timeSinceLevelLoad / 100f, mol.getConcentration());
//             node.Value.addPoint(p);
//             node = node.Next;
//           }
        //     _graphic.addPoint(p);
//       }
//     i++;
  }
}
