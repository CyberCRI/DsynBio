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

  public string getName() {return _name; }
  public eType getType() {return _type; }
  public string getDescription() {return _description; }
  public float getConcentration() {return _concentration; }
  public float getDegradationRate() {return _degradationRate; }

  public void setName(string name) { _name = name; }
  public void setType(eType type) { _type = type; }
  public void setDescription(string description) { _description = description; }
  public void setConcentration(float concentration) { _concentration = concentration; if (_concentration < 0) _concentration = 0;}
  public void setDegradationRate(float degradationRate) { _degradationRate = degradationRate; }
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
  protected float _beta;
  protected LinkedList<Product> _products;
  protected bool _isActive;

  public IReaction()
  {
    _products = new LinkedList<Product>();
    _isActive = true;
  }

  public void setName(string name) { _name = Tools.epurStr(name); }
  public string getName() { return _name; }
  public void setBeta(float beta) { _beta = beta; }
  public float getBeta() { return _beta; }

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

  public override void react(ArrayList molecules)
  {
    if (!_isActive)
      return;
//     Debug.Log("React degradation");
    Molecule mol = ReactionEngine.getMoleculeFromName(_molName, molecules);
    mol.setConcentration(mol.getConcentration() - mol.getDegradationRate() * mol.getConcentration() * 0.05f);
  }
}