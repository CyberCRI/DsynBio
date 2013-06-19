using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class InstantReaction : IReaction
{
  private string _name;
  private LinkedList<Product> _reactants;

  public InstantReaction()
  {
    _reactants = new LinkedList<Product>();
  }

  public string getName() { return _name; }
  public void setName(string str) { _name = str; }
  public void addReactant(Product reactant) { if (reactant != null) _reactants.AddLast(reactant); }


  private float getLimitantFactor(ArrayList molecules)
  {
    Product minReact = null;
    bool b = true;
    Molecule mol = null;
    Molecule molMin = null;

    foreach (Product r in _reactants)
      {
        mol = ReactionEngine.getMoleculeFromName(r.getName(), molecules);
        if (b && mol != null)
          {
            molMin = mol;
            minReact = r;
            b = false;
          }
        else if (mol != null)
          {
            if (molMin != null && ((mol.getConcentration() / r.getQuantityFactor()) < (molMin.getConcentration() / minReact.getQuantityFactor())))
              {
                molMin = mol;
                minReact = r;
              }
          }
      }
    if (minReact == null)
      return 0f;
    return (molMin.getConcentration() / minReact.getQuantityFactor());
  }

  public override void react(ArrayList molecules)
  {
    if (!_isActive)
      return;
    
    float delta = getLimitantFactor(molecules);
    Molecule mol;

    foreach (Product react in _reactants)
      {
        mol = ReactionEngine.getMoleculeFromName(react.getName(), molecules);
        mol.subConcentration(delta * react.getQuantityFactor());
      }
    foreach (Product prod in _products)
      {
        mol = ReactionEngine.getMoleculeFromName(prod.getName(), molecules);
        mol.addConcentration(delta * prod.getQuantityFactor());
      }
  }

}