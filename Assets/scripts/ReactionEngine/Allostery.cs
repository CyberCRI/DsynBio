using UnityEngine;
using System;
using System.Collections;

public class Allostery : IReaction
{
  private string _name;
  private string _effector;
  private float _K;
  private int _n;
  private string _protein;
  private string _product;

  public Allostery()
  {
    Debug.Log("Construction");
  }

  public string getName() { return _name; }
  public void setName(string str) { _name = str; }
  public string getEffector() { return _effector; }
  public void setEffector(string str) { _effector = str; }
  public float getK() { return _K; }
  public void setK(float value) { _K = value;}
  public int getN() { return _n; }
  public void setN(int value) { _n = value;}
  public void setProtein(string str) { _protein = str;}
  public string getProtein() { return _protein; }
  public void setProduct(string str) { _product = str;}
  public string getProduct() { return _product; }

  public override void react(ArrayList molecules)
  {
    if (!_isActive)
      return;

    float delta;
    float m;
    Molecule effector = ReactionEngine.getMoleculeFromName(_effector, molecules);
    Molecule protein = ReactionEngine.getMoleculeFromName(_protein, molecules);
    Molecule product = ReactionEngine.getMoleculeFromName(_product, molecules);

    if (effector == null)
      Debug.Log("Cannot find effector molecule named : " + effector);
    else if (protein == null)
      Debug.Log("Cannot find protein molecule named : " + protein);
    else if (product == null)
      Debug.Log("Cannot find product molecule named : " + product);
    else
      {
        m = (float)Math.Pow(effector.getConcentration() / _K, _n);
        delta =  (m / (1 + m)) * protein.getConcentration();
        product.setConcentration(product.getConcentration() + delta * 1f);
        protein.setConcentration(protein.getConcentration() - delta * 1f);
        effector.setConcentration(effector.getConcentration() - delta * 1f);
      }
        Debug.Log("ca marche coco");
  }

}