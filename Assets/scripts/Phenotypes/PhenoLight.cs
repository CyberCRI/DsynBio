using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PhenoLight : Phenotype {

  public Color         _color;
  public bool isActive;


  public override void StartPhenotype()
  {
    gameObject.AddComponent<Light>();
    gameObject.light.color = _color;
    gameObject.light.type = LightType.Point;
    gameObject.light.color = Color.blue;
    gameObject.light.range = 2.5f;
    gameObject.light.intensity = 1;
  }

  public override void UpdatePhenotype()
  {
    gameObject.light.enabled = true;
    if (!isActive)
      return ;
    Molecule mol = ReactionEngine.getMoleculeFromName("H2O", _molecules);
    if (mol == null)
      return ;
//     gameObject.light.intensity = 1;    
    float intensity = (float)(Math.Pow((double)(mol.getConcentration()), 0.5) / (40.0 + Math.Pow((double)mol.getConcentration(), 0.5))) * 8f;
    gameObject.light.intensity = intensity;
  }
}
