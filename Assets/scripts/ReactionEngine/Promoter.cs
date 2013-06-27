using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;

// ========================== Promoter ================================

/*!
 *  \brief     Manage promoter reactions
 *  \details   This class manage all the promoter reactions

 A promoter reaction represent the behaviour of a promoter and of the transcription that it manage (Device).
 The promoter responds to a logic input function that should respect the synthax below.

 Input function:
 ==============

        Grammar :

                EXPR ::= ANDEXPR [OP_OR OREXPR]
                ANDEXPR ::= PAREXPR [OP_AND ANDEXPR]
                PAREXPR ::= (NOTEXPR | OP_LPAR OREXPR OP_RPAR)
                NOTEXPR ::= [OP_NOT] (OPERANDEXPR | BOOL_EXPR)
                BOOL_EXPR :: = (OP_TRUE | OP_FALSE)
                OPERANDEXPR ::= CONSTANTEXPR WORD
                CONSTANTEXPR ::= OP_LHOOK FLOATNUMBER OP_COMMA FLOATNUMBER OP_RHOOK
                WORD ::= CHAR [CHAR | NUMBER]
                NUMBER ::= (0|1|2|3|4|5|6|7|8|9) [NUMBER]
                CHAR ::= (a-z,A-Z)
                FLOATNUMBER ::= NUMBER [OP_DOT NUMBER]
                
        Default Operators:

                OP_OR ::= "+"
                OP_AND ::= "*"
                OP_LPAR ::= "("
                OP_RPAR ::= ")"
                OP_NOT ::= "!"
                OP_TRUE :: = "T"
                OP_FALSE ::= "F"
                OP_LHOOK ::= "["
                OP_RHOOK ::= "]"
                OP_COMMA ::= ","
                OP_DOT ::= "."
                

        Examples :

                - T                             Always true
                - F                             Always false
                - [1.2,1]X                      Activated when  [X] >= 1.2 with Stepness = 1

                - [1.3,1]X*([2.4,2]Y+[2.5,1]Z)  Activated when  [X] >= 1.3 with Stepness = 1 AND
                                                                 ([Y] >= 2.4 with Stepness = 2 OR
                                                                  [Z] >= 2.5 with stepness = 1)

                - !([1.3,2]X|[1.4,1]Y)          Activated when  [X] <= 1.3 with stepness = 2 OR
                                                                [Y] <= 1.4 with stepness = 1
                                                                ... But not only. See below to understand

                - ![0.8,1]LacI*[3.4,2]GFP       Activated when  [LacI] <= 0.8 with stepness = 1 AND
                                                                [GFP] >= 3.4 with stepness = 2
        

Synthax tree and execution:
==========================

A tree is build from the grammar above.

        Example :

                - [1.3,2]X*![1.4,1]Y create the tree below:


                                 AND
                                  |
                        ---------------------
                        |                   |
                        C                  NOT
                  ------------              |
                  |          |              C
                  1.4        X        ------------                 
                  |                   |          |
                  2                   1.4        Y
                                      |
                                      1

Execution :

Here, the tree is left recursive executed.
Each node is executed by a specific function.

See below how each kind of node ares executed :

        - AND (*) node:                         return Min(leftNode, RightNode)
        - OR (+) node:                          return leftNode + rightNode
        - Not (!) node:                         return 1 - leftNode
        - constant (C) node:                    return hill_function with  K parameter = leftNode  and  n parameter = leftNode of leftNode
        - Transcription factor:                 return concentration of concerned transcription factor
          (X or Y in the tree above) node
        - Value (1.4, 2, 1.4 and 1 above) node: return the value that it contain.
        
        /!\ hill_function = [X]^n / (K + [X]^n)
        This function can be replaced by a stepfunction that correspond to a Hill function with n = +inf



A Device will transcript all the operon and so increase concentration of molecules that it produces.
In order to do this, it needs this parameters:

                - Beta -> maximal production rate
                - Terminator factor -> between 0-1 that describe the probability that the terminator stop the transcription
                - formula -> the result value of the tree above
                - Operon :      - The Molecule that is transcripted
                                - The RBS factor (RBSf), between 0-1 that correspond to the RBS affinity with the ribosomes

To see how the calculus is done, refer you to the react() function of this class.


\attention To understand how to build a PromoterReaction refer you to the PromoterLoader class

\author    Pierre COLLET
\mail pierre.collet91@gmail.com
 */
public class Promoter : IReaction
{
  private float _terminatorFactor;                      //! Determine the fiability of the terminator (0-1 wich correspond to 0% to 100%)
  private TreeNode<PromoterNodeData> _formula;          //! The formula describe in the detailled description
  protected float _beta;                                //! The maximal production of the promoter

  public void setBeta(float beta) { _beta = beta; }
  public float getBeta() { return _beta; }
  public void setTerminatorFactor(float v) { _terminatorFactor = v; }
  public float getTerminatorFactor() { return _terminatorFactor; }
  public void setFormula(TreeNode<PromoterNodeData> tree) { _formula = tree; }
  public TreeNode<PromoterNodeData> getFormula() { return _formula; }


  /*! 
Implementation of a Hill function
\param K Threshold value
\param concentration Quantity of the molecule
\param n Stepness parameter
  */
  public static float hillFunc(float K, float concentration, double n)
  {
    return (float)(Math.Pow(concentration, n) / (K + Math.Pow(concentration, n)));
  }

  /*! 
Implementation of a step function function
\param K Threshold value
\param concentration Quantity of the molecule
  */
  public static float stepFunc(float K, float concentration)
  {
    if (concentration > K)
      return 1f;
    return 0f;
  }

//   FIXME : Check all possible issues like product or molecule not exists;
  /*! 
Execute a Node of type : Constant
\param node The node of the tree to execute
\param molecules The list of molecules
\return The result of the hill function.
  */
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
  /*! 
Execute a Node of type : Word
\param node The node of the tree to execute
\param molecules The list of molecules
\return return the concentration of the molecule in the node.
  */
  private Molecule execWord(TreeNode<PromoterNodeData> node, ArrayList molecules)
  {
    return ReactionEngine.getMoleculeFromName(node.getData().value, molecules);
  }

  // FIXME : check issues like node == null etc;
  /*! 
Execute a Node of type : Bool
\param node The node of the tree to execute
\return 1 if the value of the node is True, 0 else
  */
  private float execBool(TreeNode<PromoterNodeData> node)
  {
    if (node.getData().value == "T")
      return 1f;
    return 0f;
  }

  // FIXME : check issues like bad parse and node == null
  /*! 
Execute a Node of type : Num
\param node The node of the tree to execute
\param molecules The list of molecules
\return The value that contain the node
  */
  private float execNum(TreeNode<PromoterNodeData> node, ArrayList molecules)
  {
    return float.Parse(node.getData().value.Replace(",", "."));
  }

  /*! 
Execute a Node.
\param node The node of the tree to execute
\param molecules The list of molecules
\return The result of the function
  */
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

  /*! 
\brief Execute a promoter reaction as describe in the detailled reaction
\details Once the tree is executed, the result is put in delta and used as follow :

For each Product P in the operon :

        [P] += delta * RBSf * TerminatorFactor * beta(Maximal production)

\param molecules The list of molecules
  */
  public override void react(ArrayList molecules)
  {
    if (!_isActive)
      return;
    float delta = execNode(_formula, molecules);
    foreach (Product pro in _products)
      {
//         Debug.Log(pro.getName());
        Molecule mol = ReactionEngine.getMoleculeFromName(pro.getName(), molecules);
        mol.setConcentration(mol.getConcentration() + delta * 1f
                             * pro.getQuantityFactor() * _terminatorFactor * _beta);
      }
    //       pro.setConcentration(pro.getConcentration() * delta);
  }

}
