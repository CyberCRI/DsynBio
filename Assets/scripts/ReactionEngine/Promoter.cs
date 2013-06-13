using System.Collections.Generic;
using System.Collections;
using System;


using UnityEngine;

// ========================== Promoter ================================

public class Promoter : IReaction
{
  private float _terminatorFactor;
  private TreeNode<PromoterNodeData> _formula;
  protected float _beta;

//   public Promoter(string name = null, float beta = 0)
//   {
//     _products = new LinkedList<GeneProduct>();
//   }

  public void setBeta(float beta) { _beta = beta; }
  public float getBeta() { return _beta; }
//   public void setN(float n) { _n = n; }
//   public float getN() { return _n; }
  public void setTerminatorFactor(float v) { _terminatorFactor = v; }
  public float getTerminatorFactor() { return _terminatorFactor; }
  public void setFormula(TreeNode<PromoterNodeData> tree) { _formula = tree; }
  public TreeNode<PromoterNodeData> getFormula() { return _formula; }

  public static float hillFunc(float K, float concentration, double n)
  {
    return (float)(Math.Pow(concentration, n) / (K + Math.Pow(concentration, n)));
  }

  public static float stepFunc(float K, float concentration)
  {
    if (concentration > K)
      return 1f;
    return 0f;
//     return (float)(Math.Pow(concentration, n) / (K + Math.Pow(concentration, n)));
  }

//   FIXME : Check all possible issues like product or molecule not exists;
  private float execConstant(TreeNode<PromoterNodeData> node, ArrayList molecules)
  {
    if (node == null)
      return 0f;

    if (node.getRightNode().getData().token == PromoterParser.eNodeType.BOOL)
      return execBool(node.getRightNode());
    Molecule mol = execWord(node.getRightNode(), molecules);
    float K = execNum(node.getLeftNode(), molecules);
    float n = 1f;
    if (node.getLeftNode() != null && node.getLeftNode().getLeftNode() != null)
      n = execNum(node.getLeftNode().getLeftNode(), molecules);
//     Debug.Log("concentration de " + mol.getName() + " = " + mol.getConcentration());
//     return stepFunc(K, mol.getConcentration());
//     if (mol.getName() == "Y")
//       Debug.Log("hill : " + hillFunc(K, mol.getConcentration(), 4));
//     else
//       Debug.Log(mol.getName());
    return hillFunc(K, mol.getConcentration(), n);
  }

  // FIXME : check issues like node == null etc;
  private Molecule execWord(TreeNode<PromoterNodeData> node, ArrayList molecules)
  {
    return ReactionEngine.getMoleculeFromName(node.getData().value, molecules);
  }

  // FIXME : check issues like node == null etc;
  private float execBool(TreeNode<PromoterNodeData> node)
  {
    if (node.getData().value == "T")
      return 1f;
    return 0f;
  }

  // FIXME : check issues like bad parse and node == null
  private float execNum(TreeNode<PromoterNodeData> node, ArrayList molecules)
  {
    return float.Parse(node.getData().value.Replace(",", "."));
  }

  private float execNode(TreeNode<PromoterNodeData> node, ArrayList molecules)
  {
    if (node != null)
      {
        if (node.getData().token == PromoterParser.eNodeType.OR)
          return execNode(node.getLeftNode(), molecules) + execNode(node.getRightNode(), molecules);
        else if (node.getData().token == PromoterParser.eNodeType.AND)
          return Math.Min(execNode(node.getLeftNode(), molecules), execNode(node.getRightNode(), molecules));
        else if (node.getData().token == PromoterParser.eNodeType.NOT)
          return 1f - execNode(node.getLeftNode(), molecules);
        else if (node.getData().token == PromoterParser.eNodeType.CONSTANT)
          return execConstant(node, molecules);
        else if (node.getData().token == PromoterParser.eNodeType.BOOL)
          return execBool(node);
        else if (node.getData().token == PromoterParser.eNodeType.WORD)
          {
            Molecule mol = ReactionEngine.getMoleculeFromName(node.getData().value, molecules);
            if (mol != null)
              return mol.getConcentration();
          }
        else if (node.getData().token == PromoterParser.eNodeType.NUM)
          return float.Parse(node.getData().value.Replace(",", "."));
      }
    return 1.0f;
  }

  public override void react(ArrayList molecules)
  {
    if (!_isActive)
      return;
    float delta = execNode(_formula, molecules);
    foreach (Product pro in _products)
      {
//         Debug.Log(pro.getName());
        Molecule mol = ReactionEngine.getMoleculeFromName(pro.getName(), molecules);
        mol.setConcentration(mol.getConcentration() + delta * 0.01f * pro.getProductionFactor() * _terminatorFactor * _beta);
      }
    //       pro.setConcentration(pro.getConcentration() * delta);
  }

}
