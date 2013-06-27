using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
/*!
  \brief This class represent a Medium
  \details
  A Medium is an area closed by a something that is permeable or not.
  Each Medium contain a list of molecules wich contains the concentration of
  each kind of molecules.
  Each Medium also have a list of reactions.
  You can define molecules diffusion of molecules between mediums with Fick of ActiveTransport.
  \sa Fick
  \sa ActiveTransport
  \author Pierre COLLET
  \mail pierre.collet91@gmail.com
*/
public class Medium
{
  private LinkedList<IReaction> _reactions;             //!< The list of reaction
  private ArrayList             _molecules;             //!< The list of molecules (Molecule)

  private int           _id;                            //!< The id of the Medium
  private string        _name;                          //!< The name of the Medium
  private string        _reactionsSet;                  //!< The ReactionSet id affected to this Medium
  private string        _moleculesSet;                  //!< The MoleculeSet id affected to this Medium

  // Graphic Stuff
  private LinkedList<Curve>    _curves;                 //!< List of graphical curves
  public GraphDrawer _graphic;                          //!< The graphic drawer

  //! Default Constructor
  public Medium()
  {
    _curves = new LinkedList<Curve>();
  }

  public void setId(int id) { _id = id;}
  public int getId() { return _id;}
  public void setName(string name) { _name = name;}
  public string getName() { return _name;}
  public void setReactionsSet(string reactionsSet) { _reactionsSet = reactionsSet;}
  public string getReactionsSet() { return _reactionsSet;}
  public void setMoleculesSet(string moleculesSet) { _moleculesSet = moleculesSet;}
  public string getMoleculesSet() { return _moleculesSet;}
  public ArrayList getMolecules() { return _molecules; }

  /*!
    \brief Substract a concentration to molecule corresponding to the name.
    \param name The name of the Molecules.
    \param value The value to substract.
   */
  public void subMolConcentration(string name, float value)
  {
    Molecule mol = ReactionEngine.getMoleculeFromName(name, _molecules);

    if (mol != null)
      mol.setConcentration(mol.getConcentration() - value);
  }

  /*!
    \brief Add a concentration to molecule corresponding to the name.
    \param name The name of the Molecules.
    \param value The value to Add.    
   */
  public void addMolConcentration(string name, float value)
  {
    Molecule mol = ReactionEngine.getMoleculeFromName(name, _molecules);

    if (mol != null)
      mol.setConcentration(mol.getConcentration() + value);
  }

  /*!
    \brief Load reactions from a ReactionsSet
    \param reactionsSet The set to load
   */
  public void initReactionsFromReactionsSet(ReactionsSet reactionsSet)
  {
    if (reactionsSet == null)
      return;
    foreach (IReaction reactions in reactionsSet.reactions)
      _reactions.AddLast(reactions);
  }

  /*!
    \brief Load degradation from each molecules
    \param allMolecules The list of all the molecules
   */
  public void initDegradationReactions(ArrayList allMolecules)
  {
    foreach (Molecule mol in allMolecules)
      _reactions.AddLast(new Degradation(mol.getDegradationRate(), mol.getName()));
  }

  /*!
    \brief Load Molecules from a MoleculesSet
    \param molSet The set to Load
    \param allMolecules The list of all the molecules
   */
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

  /*!
    \brief Initialize the Medium
    \param reactionsSets The list of all the reactions sets
    \param moleculesSets The list of all the molecules sets
    \param drawer The drawe wich will draw curves (Curves) of this Medium
   */
  public void Init(LinkedList<ReactionsSet> reactionsSets, LinkedList<MoleculesSet> moleculesSets, GraphDrawer drawer = null)
  {
    _reactions = new LinkedList<IReaction>();
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
    int i = 0;
//     for (int i = 0; i < _molecules.Count; i++)
    foreach (Molecule m in _molecules)
      {
        Curve c = new Curve(m.getName());
        _curves.AddLast(c);
        if (_graphic != null)
          _graphic.addCurve(c);
        i++;
      }    
  }

  /*!
    \brief Execute everything about simulation into the Medium
   */
  public void Update()
  {
    foreach (IReaction reaction in _reactions)
      reaction.react(_molecules);


    if (_name == "Cellia")
      {
        if (Input.GetKey(KeyCode.UpArrow))
          ReactionEngine.getMoleculeFromName("H", _molecules).setConcentration(ReactionEngine.getMoleculeFromName("H", _molecules).getConcentration() + 5f);
        if (Input.GetKey(KeyCode.DownArrow))
          ReactionEngine.getMoleculeFromName("H", _molecules).setConcentration(ReactionEngine.getMoleculeFromName("H", _molecules).getConcentration() - 5f);
        if (Input.GetKey(KeyCode.RightArrow))
          ReactionEngine.getMoleculeFromName("O", _molecules).setConcentration(ReactionEngine.getMoleculeFromName("O", _molecules).getConcentration() + 5f);
        if (Input.GetKey(KeyCode.LeftArrow))
          ReactionEngine.getMoleculeFromName("O", _molecules).setConcentration(ReactionEngine.getMoleculeFromName("O", _molecules).getConcentration() - 5f);
      }

    // Graphic Stuff
    LinkedListNode<Curve> node = _curves.First;
    foreach (Molecule mol in _molecules)
      {
//         Debug.Log("[" + mol.getName() + "] = " + mol.getConcentration());
        Vector2 p = new Vector2((float)Time.timeSinceLevelLoad*200f, mol.getConcentration() * 3.0f);
        node.Value.addPoint(p);
        node = node.Next;
      }
  }
}