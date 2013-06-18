using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Molecule
{
  public enum eType
  {
    ENZYME,
    TRANSCRIPTION_FACTOR,
    OTHER
  }

  private string _name;
  private eType _type;
  private string _description;
  private float _concentration;
  private float _degradationRate;
  private float _size;

  public Molecule(Molecule mol = null)
  {
    if (mol != null)
      {
        _name = mol._name;
        _type = mol._type;
        _description = mol._description;
        _concentration = mol._concentration;
        _degradationRate = mol._degradationRate;
        _size = mol._size;
      }
  }

  public string getName() {return _name; }
  public eType getType() {return _type; }
  public string getDescription() {return _description; }
  public float getConcentration() {return _concentration; }
  public float getDegradationRate() {return _degradationRate; }
  public float getSize() { return _size; }

  public void setName(string name) { _name = name; }
  public void setType(eType type) { _type = type; }
  public void setDescription(string description) { _description = description; }
  public void setConcentration(float concentration) { _concentration = concentration; if (_concentration < 0) _concentration = 0;}
  public void setDegradationRate(float degradationRate) { _degradationRate = degradationRate; }
  public void setSize(float size) { _size = size; }
}

public class Product
{
  protected string _name;
  // FIXME : Potentially add a ptr to the molecule
  public void setName(string name) { _name = Tools.epurStr(name); }
  public string getName() { return _name; }
  public virtual float getProductionFactor() { return 1f;}
}

public class GeneProduct : Product
{
  private float _RBSf;
  
  public void setRBSFactor(float value) { _RBSf = value; }
  public float getRBSFactor() { return _RBSf; }  
  public override float getProductionFactor() { return _RBSf;}
}


public abstract class IReaction
{
  protected string _name;
  protected LinkedList<Product> _products;
  protected bool _isActive;

  public IReaction()
  {
    _products = new LinkedList<Product>();
    _isActive = true;
  }

  public void setName(string name) { _name = Tools.epurStr(name); }
  public string getName() { return _name; }

  public abstract void react(ArrayList molecules);
  public void addProduct(Product prod) { if (prod != null) _products.AddLast(prod); }
}

// ========================== DEGRADATION ================================

public class Degradation : IReaction
{
  private float _degradationRate;
  private string _molName;

  public Degradation(float degradationRate, string molName)
  {
    _degradationRate = degradationRate;
    _molName = molName;
  }

// FIXME : use _degradationRate
  public override void react(ArrayList molecules)
  {

    if (!_isActive)
      return;
//     Debug.Log("React degradation");s
    Molecule mol = ReactionEngine.getMoleculeFromName(_molName, molecules);
    mol.setConcentration(mol.getConcentration() - mol.getDegradationRate() * mol.getConcentration() * 1f//  * 0.05f
                         );
  }
}