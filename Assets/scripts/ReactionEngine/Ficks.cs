using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class FickProprieties
{
  public int MediumId1 {get; set;}
  public int MediumId2  {get; set;}
  public float P  {get; set;}
  public float surface  {get; set;}
}

class Fick
{
  private LinkedList<FickReaction>      _reactions;
  FickLoader                            _loader;
//   LinkedList<Medium>                    _mediums;

  public Fick(// LinkedList<Medium> mediums
              )
  {
    _reactions = new LinkedList<FickReaction>();
    _loader = new FickLoader();
//     _mediums = mediums;
  }

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

  public void           loadFicksReactionsFromFiles(string[] files, LinkedList<Medium> mediums)
  {
    LinkedList<FickProprieties> propsList = new LinkedList<FickProprieties>();

    foreach (string file in files)
      LinkedListExtensions.AppendRange<FickProprieties>(propsList, _loader.loadFickProprietiesFromFile(file));
//     foreach (string file in files)
//       {
//         propsList = _loader.loadFickProprietiesFromFile(file);
        
//       }
    _reactions = FickReaction.getFickReactionsFromMediumList(mediums);
    finalizeFickReactionFromProps(propsList, _reactions);
    // replace values in _reactions by values in reactionsList;
  }

  public void react()
  {
    foreach (FickReaction fr in _reactions)
      fr.react(null);
  }
}

class FickReaction : IReaction
{
  private float _surface;
  private float _P;
  private Medium _medium1;
  private Medium _medium2;

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

  public override void react(ArrayList molecules)
  {
    ArrayList molMed1 = _medium1.getMolecules();
    ArrayList molMed2 = _medium2.getMolecules();
    Molecule mol2;
    float c1;
    float c2;
    float result;
    
    foreach (Molecule mol1 in molMed1)
      {
        c1 = mol1.getConcentration();
        mol2 = ReactionEngine.getMoleculeFromName(mol1.getName(), molMed2);
        if (mol2 != null)
          {
            c2 = mol2.getConcentration();
            result = (c2 - c1) * _P * _surface;
            if (result < 0) // go to c2
              {
                Debug.Log("dans un sens");
                mol2.setConcentration(c2 + result);
                mol1.setConcentration(c1 - result);
              }
            else // go to c1
              {
                mol1.setConcentration(c1 + result);
                Debug.Log("et dans lautre");
                mol2.setConcentration(c2 - result);
              }
          }
      }
  }
}