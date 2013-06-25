using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Phenotype : MonoBehaviour
{
  public ReactionEngine _RE;
  public int            _mediumId;

  protected ArrayList             _molecules;
  protected Medium                _mediumRef;
  protected bool                  _isIdChanged;

  public void   setMediumId(int id)
  {
    _mediumId = id;
    _isIdChanged = true;
  }

  public void updateMolecules()
  {
    if (_isIdChanged)
      {
        LinkedList<Medium>    mediums = _RE.getMediumList();
        _mediumRef = ReactionEngine.getMediumFromId(_mediumId, mediums);
        _isIdChanged = false;
      }
    _molecules = _mediumRef.getMolecules();    
  }

  // Use this for initialization
  virtual public void Start () {
    _isIdChanged = true;
    StartPhenotype();
  }
  
  // Update is called once per frame
  virtual public void Update () {
    updateMolecules();
    UpdatePhenotype();
  }

  public abstract void StartPhenotype();
  public abstract void UpdatePhenotype();
}
