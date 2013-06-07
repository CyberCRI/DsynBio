using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Medium
{
//   public TextAsset _reactionFile;
//   private FileLoader _fileLoader;
  private LinkedList<IReaction> _reactions;
  private ArrayList             _molecules;

  private string        _id;
  private string        _name;
  private string        _reactionsSet;
  private string        _moleculesSet;

  // Graphic Stuff
  private LinkedList<Curve>    _curves;
  public GraphDrawer _graphic;

  public Medium()
  {
    _curves = new LinkedList<Curve>();
  }

  public void setId(string id) { _id = id;}
  public string getId() { return _id;}
  public void setName(string name) { _name = name;}
  public string getName() { return _name;}
  public void setReactionsSet(string reactionsSet) { _reactionsSet = reactionsSet;}
  public string getReactionsSet() { return _reactionsSet;}
  public void setMoleculesSet(string moleculesSet) { _moleculesSet = moleculesSet;}
  public string getMoleculesSet() { return _moleculesSet;}

  public void initReactionsFromReactionsSet(ReactionsSet reactionsSet)
  {
    _reactions = new LinkedList<IReaction>();

    foreach (IReaction reactions in reactionsSet.reactions)
      _reactions.AddLast(reactions);
  }

  public void initDegradationReactions(ArrayList allMolecules)
  {
    foreach (Molecule mol in allMolecules)
      _reactions.AddLast(new Degradation(mol.getDegradationRate(), mol.getName()));
  }

  public void initMoleculesFromMoleculesSets(MoleculesSet molSet, ArrayList allMolecules)
  {
    Molecule newMol;
    Molecule startingMolStatus;

    _molecules = new ArrayList();
    foreach (Molecule mol in allMolecules)
      {
        newMol = new Molecule(mol);
        startingMolStatus = ReactionEngine.getMoleculeFromName(mol.getName(), molSet.molecules);
        if (startingMolStatus == null)
          newMol.setConcentration(0);
        else
          newMol.setConcentration(startingMolStatus.getConcentration());
        _molecules.Add(newMol);
      }

      
  }

  public void Init(LinkedList<ReactionsSet> reactionsSets, LinkedList<MoleculesSet> moleculesSets, GraphDrawer drawer = null)
  {
    ReactionsSet reactSet = ReactionEngine.getReactionsSetFromId(_reactionsSet, reactionsSets);
    MoleculesSet molSet = ReactionEngine.getMoleculesSetFromId(_moleculesSet, moleculesSets);
    ArrayList allMolecules = ReactionEngine.getAllMoleculesFromMoleculeSets(moleculesSets);

    if (reactSet == null)
      Debug.Log("Cannot find group of reactions named " + _reactionsSet);
    if (molSet == null)
      Debug.Log("Cannot find group of molecules named" + _moleculesSet);

    initReactionsFromReactionsSet(reactSet);
    initMoleculesFromMoleculesSets(molSet, allMolecules);
    initDegradationReactions(allMolecules);

    _graphic = drawer;
    for (int i = 0; i < _molecules.Count; i++)
      {
        Curve c = new Curve();
        _curves.AddLast(c);
        if (_graphic != null)
          _graphic.addCurve(c);
      }    
  }

  public void Update()
  {
    foreach (IReaction reaction in _reactions)
      reaction.react(_molecules);




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
        
        Molecule mole = ReactionEngine.getMoleculeFromName("E", _molecules);
        mole.setConcentration(mole.getConcentration() - 0.2f);
      }
    if (Input.GetKey (KeyCode.RightArrow))
      {
        Molecule mole = ReactionEngine.getMoleculeFromName("E", _molecules);
        mole.setConcentration(mole.getConcentration() + 0.2f);
      }
    // Graphic Stuff
    LinkedListNode<Curve> node = _curves.First;
    foreach (Molecule mol in _molecules)
      {
        Vector2 p = new Vector2((float)Time.timeSinceLevelLoad*200f, mol.getConcentration() *100f);
        node.Value.addPoint(p);
        node = node.Next;
      }
  }
}