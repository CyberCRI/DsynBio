using UnityEngine;
using System;
using System.Collections;

public class EnzymeReaction : IReaction
{
  private string _substrate;
  private string _enzyme;
  private float _Kcat;
  private string  _effector;
  private float _alpha;
  private float _beta;
  private float _Km;
  private float _Ki;

  public void setSubstrate(string str) { _substrate = str; }
  public string getSubstrate() { return _substrate; }
  public void setEnzyme(string str) { _enzyme = str;}
  public string getEnzyme() { return _enzyme; }
  public void setKcat(float value) { _Kcat = value;}
  public float getKcat() { return _Kcat; }
  public string getEffector() { return _effector; }
  public void setEffector(string str) { _effector = str; }
  public float getAlpha() { return _alpha; }
  public void setAlpha(float value) { _alpha = value;}
  public float getBeta() { return _beta; }
  public void setBeta(float value) { _beta = value;}
  public float getKm() { return _Km; }
  public void setKm(float value) { _Km = value;}
  public void setKi(float value) { _Ki = value;}
  public float getKi() { return _Ki; }

  private float execEnzymeReaction(ArrayList molecules)
  {
    Molecule substrate = ReactionEngine.getMoleculeFromName(_substrate, molecules);
    Molecule enzyme = ReactionEngine.getMoleculeFromName(_enzyme, molecules);
    float Vmax = _Kcat * enzyme.getConcentration();
    float effectorConcentration = 0;

    if (_effector != "False")
      {
        Molecule effector = ReactionEngine.getMoleculeFromName(_effector, molecules);
        if (effector != null)
          effectorConcentration = effector.getConcentration();
      }
    float v = ((Vmax * (substrate.getConcentration() / _Km)) + (_beta * Vmax * substrate.getConcentration() * effectorConcentration / (_alpha * _Km * _Ki)))
      / (1f + (substrate.getConcentration() / _Km) + (effectorConcentration / _Ki) + (substrate.getConcentration() * effectorConcentration / (_alpha * _Km * _Ki)));

    return v;
  }

  public override void react(ArrayList molecules)
  {
    if (!_isActive)
      return;

    Molecule substrate = ReactionEngine.getMoleculeFromName(_substrate, molecules);
    if (substrate == null)
      return ;
    float delta = execEnzymeReaction(molecules);
    substrate.setConcentration(substrate.getConcentration() - delta);
    foreach (Product pro in _products)
      {
        Molecule mol = ReactionEngine.getMoleculeFromName(pro.getName(), molecules);
        Debug.Log(mol.getName());
        mol.setConcentration(mol.getConcentration() + delta);
      }
  }

}