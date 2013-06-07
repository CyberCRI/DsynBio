using UnityEngine;
using System.Collections;
using System.Xml;
using System;
using System.Collections.Generic;

public class ReactionEngine : MonoBehaviour {

  private LinkedList<Medium>    _mediums;
  private LinkedList<ReactionsSet> _reactionsSets;
  private LinkedList<MoleculesSet> _moleculesSets;
  public string[]        _mediumsFiles;
  public TextAsset[]         _reactionsFiles;
  public TextAsset[]         _moleculesFiles;

  public GraphDrawer    _graphDrawer;

  public ReactionEngine()
  {
  }

  public static Molecule        getMoleculeFromName(string name, ArrayList molecules)
  {
    foreach (Molecule mol in molecules)
      if (mol.getName() == name)
        return mol;
    return null;
 }

  public static ReactionsSet    getReactionsSetFromId(string id, LinkedList<ReactionsSet> list)
  {
    foreach (ReactionsSet reactSet in list)
      if (reactSet.id == id)
        return reactSet;
    return null;
  }

  public static bool    isMoleculeIsDuplicated(Molecule mol, ArrayList list)
  {
    foreach (Molecule mol2 in list)
      if (mol2.getName() == mol.getName())
        return true;
    return false;
  }

  public static ArrayList    getAllMoleculesFromMoleculeSets(LinkedList<MoleculesSet> list)
  {
    ArrayList molecules = new ArrayList();

    
    foreach (MoleculesSet molSet in list)
      {
        foreach (Molecule mol in molSet.molecules)
          if (!isMoleculeIsDuplicated(mol, molecules))
            molecules.Add(mol);
      }
    return molecules;
  }

  public static MoleculesSet    getMoleculesSetFromId(string id, LinkedList<MoleculesSet> list)
  {
    foreach (MoleculesSet molSet in list)
      if (molSet.id == id)
        return molSet;
    return null;
  }

  public void Start ()
  {
    FileLoader fileLoader = new FileLoader();
    _reactionsSets = new LinkedList<ReactionsSet>();
    _moleculesSets = new LinkedList<MoleculesSet>();
    _mediums = new LinkedList<Medium>();
    
    foreach (TextAsset file in _reactionsFiles)
      LinkedListExtensions.AppendRange<ReactionsSet>(_reactionsSets, fileLoader.loadReactionsFromFile(file));
    foreach (TextAsset file in _moleculesFiles)
      LinkedListExtensions.AppendRange<MoleculesSet>(_moleculesSets, fileLoader.loadMoleculesFromFile(file));

    MediumLoader mediumLoader = new MediumLoader();
    foreach (string file in _mediumsFiles)
      LinkedListExtensions.AppendRange<Medium>(_mediums, mediumLoader.loadMediumsFromFile(file));
    foreach (Medium medium in _mediums)
      medium.Init(_reactionsSets, _moleculesSets, _graphDrawer);
//     Debug.Log("salut les coco2");
  }

  public void Update ()
  {
    foreach (Medium medium in _mediums)
      medium.Update();
  }
}
