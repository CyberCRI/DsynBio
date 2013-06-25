using UnityEngine;
using System;
using System.Collections;

/*!
  \brief This class manage enzyme reactions
  \details

  Manage enzyme reactions.
  \attention HELENA NEEDS TO CORRECT THE DESCRIPTION

 */
public class EnzymeReaction : IReaction
{
  private string _substrate;            //! The substrate of the reaction
  private string _enzyme;               //! The enzyme of the reaction
  private float _Kcat;                  //! The affinity between the substrate and the enzyme coefficient
  private string  _effector;            //! The effector of the reaction
  private float _alpha;                 //! Alpha descriptor of the effector
  private float _beta;                  //! Beta descriptor of the effector
  private float _Km;                    //! Affinity coefficient between
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

  /*!
    Execute an enzyme reaction.
    \details This function do all the calcul of an enzymatic reaction.
    The formula is :

                           [S]                      [S] * [I]
                   Vmax * ----  + beta * Vmax * ----------------
                           Km                    alpha * Km * Ki
          delta = -------------------------------------------------
                              [S]    [I]      [S] * [I]
                         1 + ---- + ---- + -----------------
                              Km     Ki     alpha * Km * Ki

          with : Vmax -> Maximal production
                 S -> Substrate
                 I -> Effector
                 Km -> affinity between substrate and enzyme
                 Ki -> affinity between effector and enzyme
                 alpha -> Describe the competitivity of the effector (I) with the substrate (S). a >> 1 = competitive inhibition
                                                                                                 a << 1 Uncompetitive inhibition
                                                                                                 a = 1 Noncompetitive inhibition
                 beta -> Describe the extend of inhibition (< 1) or the extend of activation (> 1)
                 others configuration of beta and alpha are mixed inhibition.

    \reference http://depts.washington.edu/wmatkins/kinetics/inhibition.html
    \return return the value that will be produce.
    \param molecules The list of molecules.
   */
  public float execEnzymeReaction(ArrayList molecules)
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

  /*!
    \brief this fonction execute all the enzyme reactions
    \details It's call execEnzymeReaction and substract to the substrate concentration what this function return.
    This function also add this returned value to all the producted molecules.
    \param molecules The list of molecules
   */
  public override void react(ArrayList molecules)
  {
    if (!_isActive)
      return;

    Molecule substrate = ReactionEngine.getMoleculeFromName(_substrate, molecules);
    if (substrate == null)
      return ;
    float delta = execEnzymeReaction(molecules) * 1f;
    substrate.setConcentration(substrate.getConcentration() - delta);
    foreach (Product pro in _products)
      {
        Molecule mol = ReactionEngine.getMoleculeFromName(pro.getName(), molecules);
        mol.setConcentration(mol.getConcentration() + delta);
      }
  }

}