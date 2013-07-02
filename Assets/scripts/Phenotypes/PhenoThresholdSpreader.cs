using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/*!
 \brief A phenotype class that represent a light reaction in function of the concentration in Water (H2O)
 in the Midium
 \author Pierre COLLET
 \mail pierre.collet91@gmail.com
 */
public class PhenoThresholdSpreader : Phenotype {

  public string        MoleculeName;
  public float         Threshold;
  public Component     component;

  //! Called at the begening
  public override void StartPhenotype()
  {
  }

  /*!
    \brief This function is called as Update in Monobehaviour.
    \details This function is called in the Phenotype class in the Update function
    This function should be implemented and all the graphical action has to be implemented in it.
    \sa Phenotype
   */
  public override void UpdatePhenotype()
  {
    bool isThresholdEnabled = false;
    Molecule mol = ReactionEngine.getMoleculeFromName(MoleculeName, _molecules);

    if (mol == null)
      return ;
    if (mol.getConcentration() >= Threshold && gameObject.GetComponent("ParticleSystem") == null)
      gameObject.AddComponent("ParticleSystem");
    else if (mol.getConcentration() < Threshold && gameObject.GetComponent("ParticleSystem") != null)
      Destroy(gameObject.GetComponent("ParticleSystem"));
//       Instantiate(prefab, new Vector3(GetComponent.Transform.x, GetComponent.Transform.y, GetComponent.Transform.z), Quaternion.identity);
  }
}
