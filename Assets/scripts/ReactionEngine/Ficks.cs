using UnityEngine;
using System.Collections.Generic;
using System.Collections;

//!  This class is a descriptive class of a FickReaction
public class FickProprieties
{
  public int MediumId1 {get; set;}
  public int MediumId2  {get; set;}
  public float P  {get; set;}
  public float surface  {get; set;}
}

//!  The class that manage all the diffusions reactions using Fick model.
/*!
  This class initialize from files and execute all the FickReaction.
*/
class Fick
{
  public const float MaximumMoleculeSize = 0.25f;       //!< Limit size of molecules that can cross the membrane of the Medium

  private LinkedList<FickReaction>      _reactions;     //!< The list of FickReaction
  FickLoader                            _loader;        //!< The class that load the FickReaction propieties
//   LinkedList<Medium>                    _mediums;

  //! Default constructor.
  public Fick()
  {
    _reactions = new LinkedList<FickReaction>();
    _loader = new FickLoader();
//     _mediums = mediums;
  }

  //! return a FickReaction reference that correspondond to the two given ids
  /*!
      \param id1 The first id.
      \param id2 The second id.
      \param list The list of FickReaction where to search in.
    */
  public static FickReaction   getFickReactionFromIds(int id1, int id2, LinkedList<FickReaction> list)
  {
    foreach (FickReaction react in list)
      {
        Medium medium1 = react.getMedium1();
        Medium medium2 = react.getMedium2();
        if (medium1 != null && medium2 != null)
          if (medium1.getId() == id1 && medium2.getId() == id2 || medium2.getId() == id1 && medium1.getId() == id2)
            return react;
      }
    return null;
  }

  //! Set attributes of FickReactions by using a list of FickProprieties
  /*!
      \param propsList The list of Proprieties.
      \param FRList The list of FickReactions.
    */
  public static void           finalizeFickReactionFromProps(LinkedList<FickProprieties> propsList, LinkedList<FickReaction> FRList)
  {
    FickReaction react;

    foreach (FickProprieties prop in propsList)
      {
        react = getFickReactionFromIds(prop.MediumId1, prop.MediumId2, FRList);
        if (react != null)
          {
            react.setPermCoef(prop.P);
            react.setSurface(prop.surface);
          }
        else
          Debug.Log("What da fuck dude!?");
      }
  }

  //! Load the diffusions reactions from a array of files and a Mediums list
  /*!
      \param files Array of files which contain information about diffusion reaction.
      \param mediums The list of all the medium.

This function load the diffusions reactions based on Fick model. It take a Array of file paths
and a list of Medium that should contain all the mediums of the simulation.
This function create the list of all the reactions between each Medium wich exist and initialize their parameters to 0.
Only the reactions explicitly defined in files are initialized to the values explicited in files.
If a parameter of a fick reaction is not specified in files then this parameter will be equal to 0.
    */
  public void           loadFicksReactionsFromFiles(string[] files, LinkedList<Medium> mediums)
  {
    LinkedList<FickProprieties> propsList = new LinkedList<FickProprieties>();
    LinkedList<FickProprieties> newPropList;

    foreach (string file in files)
      {
        newPropList = _loader.loadFickProprietiesFromFile(file);
        if (newPropList != null)
          LinkedListExtensions.AppendRange<FickProprieties>(propsList, newPropList);
      }
//     foreach (string file in files)
//       {
//         propsList = _loader.loadFickProprietiesFromFile(file);
        
//       }
    _reactions = FickReaction.getFickReactionsFromMediumList(mediums);
    finalizeFickReactionFromProps(propsList, _reactions);
    // replace values in _reactions by values in reactionsList;
  }

  //! This function is called at each frame and do all the reaction of type FickReaction.
  public void react()
  {
    foreach (FickReaction fr in _reactions)
      fr.react(null);
  }
}

class FickReaction : IReaction
{
  private float _surface;       //!< Contact surface size bwtween the two mediums
  private float _P;             //!< Permeability coefficient
  private Medium _medium1;      //!< The first Medium
  private Medium _medium2;      //!< The second Medium

//! Default constructor.
  public FickReaction()
  {
    _surface = 0;
    _P = 0;
    _medium1 = null;
    _medium2 = null;
  }

  public void setSurface(float surface) { _surface = surface;}
  public float getSurface() { return _surface;}
  public void setPermCoef(float P) { _P = P;}
  public float getPermCoef() { return _P;}
  public void setMedium1(Medium medium) { _medium1 = medium;}
  public Medium getMedium1() { return _medium1;}
  public void setMedium2(Medium medium) { _medium2 = medium;}
  public Medium getMedium2() { return _medium2;}

  //! Return all the FickReactions possible from a Medium list.
  /*!
      \param mediums The list of mediums.

This function return all the possible combinaisons of FickReaction in Medium list.
Example :
        - Medium1 + Medium2 + Medium3 = FickReaction(1, 2) + FickReaction(1, 3) + FickReaction(2, 3)
  */
  public static LinkedList<FickReaction> getFickReactionsFromMediumList(LinkedList<Medium> mediums)
  {
    FickReaction                newReaction;
    LinkedListNode<Medium>      node;
    LinkedListNode<Medium>      start = mediums.First;
    LinkedList<FickReaction>    fickReactions = new LinkedList<FickReaction>();

    while (start != null)
      {
        node = start.Next;
        while (node != null)
          {
            newReaction = new FickReaction();
            newReaction.setMedium1(start.Value);
            newReaction.setMedium2(node.Value);
            fickReactions.AddLast(newReaction);
            node = node.Next;
          }
        start = start.Next;
      }
    return fickReactions;
  }

//! Processing a reaction.
  /*!
      \param molecules A list of molecules (not usefull here)

A diffusion reaction based on fick model is calculated by using this formula :
 dn/dt = c1 - c2 * P * A
Where:
        - dn is the difference of concentration that will be applied
        - c1 and c2 the concentration the molecules in the 2 Mediums
        - P is the permeability coefficient
        - A is the contact surface size between the two Mediums
  */
  public override void react(ArrayList molecules)
  {
    ArrayList molMed1 = _medium1.getMolecules();
    ArrayList molMed2 = _medium2.getMolecules();
    Molecule mol2;
    float c1;
    float c2;
    float result;

    if (_P == 0f || _surface == 0f)
      return;
    foreach (Molecule mol1 in molMed1)
      {
        c1 = mol1.getConcentration();
        mol2 = ReactionEngine.getMoleculeFromName(mol1.getName(), molMed2);
        if (mol2 != null && mol2.getSize() <= Fick.MaximumMoleculeSize)
          {
            c2 = mol2.getConcentration();
            result = (c2 - c1) * _P * _surface * Time.deltaTime;
            mol2.setConcentration(c2 - result);
            mol1.setConcentration(c1 + result);
          }
      }
  }
}