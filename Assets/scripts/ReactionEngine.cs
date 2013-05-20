using UnityEngine;
using System.Collections;
using System.Xml;
using System;
using System.Collections.Generic;

public class ReactionEngine : MonoBehaviour {

  public TextAsset _reactionFile;

  private Parser _parser;

  private LinkedList<IReaction>   _reactions;
  private ArrayList          _molecules;

  public ReactionEngine()
  {
    _parser = new Parser();
    _reactions = new LinkedList<IReaction>();
    _molecules = new ArrayList();
  }

// ====================== PROMOTER LOADING ===========================

  private bool loadPromoterName(string value, Promoter prom)
  {
    if (String.IsNullOrEmpty(value))
      {
        Debug.Log("Error: Empty name field");
        return false;
      }
    prom.setName(value);
    return true;
  }

  private bool loadPromoterProductionMax(string value, Promoter prom)
  {
    if (String.IsNullOrEmpty(value))
      {
        Debug.Log("Error: Empty productionMax field");
        return false;
      }
    prom.setBeta(float.Parse(value.Replace(",", ".")));
    return true;
  }

  private bool loadPromoterTerminatorFactor(string value, Promoter prom)
  {
    if (String.IsNullOrEmpty(value))
      {
        Debug.Log("Error: Empty TerminatorFactor field");
        return false;
      }
    prom.setTerminatorFactor(float.Parse(value.Replace(",", ".")));
    return true;
  }

  private bool loadGene(Promoter prom, string name, string RBSf)
  {
    GeneProduct gene = new GeneProduct();

    if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(RBSf))
      {
        Debug.Log("Error: Empty Gene name field");
        return false;
      }
    gene.setName(name);
    gene.setRBSFactor(float.Parse(RBSf.Replace(",", ".")));
    prom.addProduct(gene);
    return true;
  }

  private bool loadPromoterOperon(XmlNode node, Promoter prom)
  {
    string name = null;
    string RBSf = null;
    bool n = false;
    bool rbsf = false;
    bool b = true;

    foreach (XmlNode gene in node)
      {
        n = false;
        rbsf = false;
        foreach(XmlNode attr in gene)
          {
            switch (attr.Name)
              {
              case "name":
                name = attr.InnerText;
                n = true;
                break;
              case "RBSFactor":
                RBSf = attr.InnerText;
                rbsf = true;
                break;
              }
          }
        if (n && rbsf)
          b = b && loadGene(prom, name, RBSf);
        if (!n)
          Debug.Log("Error : Missing Gene name in operon");
        if (!rbsf)
          Debug.Log("Error : Missing RBSfactor in operon");
      }
    return b;
  }

  private bool loadPromoterFormula(string formula, Promoter p)
  {
    TreeNode<NodeData> tree = _parser.Parse(formula);
    
    if (tree == null)
      {
        Debug.Log("Syntax Error in promoter Formula");
        return false;
      }
    p.setFormula(tree);
    return true;
  }

  private bool loadPromoters(XmlNode node)
  {
    XmlNodeList promotersList = node.SelectNodes("promoter");
    bool b = true;

    foreach (XmlNode promoter in promotersList)
      {
        Promoter p = new Promoter();
        foreach (XmlNode attr in promoter)
          {
            switch (attr.Name)
              {
              case "name":
                b = b && loadPromoterName(attr.InnerText, p);
                break;
              case "productionMax":
                b = b && loadPromoterProductionMax(attr.InnerText, p);
                break;
              case "terminatorFactor":
                b = b && loadPromoterTerminatorFactor(attr.InnerText, p);
                break;
              case "formula":
                b = b && loadPromoterFormula(attr.InnerText, p);
                break;
              case "operon":
                b = b && loadPromoterOperon(attr, p);
                break;
              }
            _reactions.AddLast(p);
          }
      }
    return b;
  }

// ====================== END PROMOTER LOADING ===========================

  

// ================== ENZYME REACTIONS LOADING ==========================

  private bool loadEnzymeReactionName(string value, EnzymeReaction er)
  {
    if (String.IsNullOrEmpty(value))
      {
        Debug.Log("Error: Empty name field");
        return false;
      }
    er.setName(value);
    return true;
  }

  private bool loadEnzymeReactionProductionMax(string value, EnzymeReaction er)
  {
    if (String.IsNullOrEmpty(value))
      {
        Debug.Log("Error: Empty productionMax field");
        return false;
      }
    er.setBeta(float.Parse(value.Replace(",", ".")));
    return true;
  }

  private bool loadEnzymeReactionProducts(XmlNode node, EnzymeReaction er)
  {
    foreach (XmlNode attr in node)
      {
        if (attr.Name == "name")
          {
            if (String.IsNullOrEmpty(attr.InnerText))
              Debug.Log("Warning : Empty name field in Enzyme Reaction definition");
            Product prod = new Product();
            prod.setName(node.InnerText);
            er.addProduct(prod);
          }
      }
    return true;
  }

  // FIXME : Determine what will be the syntax of EnzymeFormula
  private bool loadEnzymeReactionFormula(string formula, EnzymeReaction er)
  {
//     TreeNode<NodeData> tree = _parser.Parse(formula);
    
//     if (tree == null)
//       {
//         Debug.Log("Syntax Error in promoter Formula");
//         return false;
//       }
//     p.setFormula(tree);
    return true;
  }
  
  private bool loadEnzymeReactions(XmlNode node)
  {
    XmlNodeList EReactionsList = node.SelectNodes("enzyme");
    bool b = true;
    
    foreach (XmlNode EReaction in EReactionsList)
      {
        EnzymeReaction er = new EnzymeReaction();
        foreach (XmlNode attr in EReaction)
          {
            switch (attr.Name)
              {
              case "name":
                b = b && loadEnzymeReactionName(attr.InnerText, er);
                break;
              case "productionMax":
                b = b && loadEnzymeReactionProductionMax(attr.InnerText, er);
                break;
              case "formula":
                b = b && loadEnzymeReactionFormula(attr.InnerText, er);
                break;
              case "products":
                b = b && loadEnzymeReactionProducts(attr, er);
                break;
              }
          }
        _reactions.AddLast(er);
      }
    return b;
  }

// ================== END ENZYME REACTIONS LOADING =======================
// FIXME : check if we need to implement other chemical reactions
//   private bool loadChemicalReactions(XmlNode node)
//   {
//     XmlNodeList CReactionsList = node.SelectNodes("chemical");
//     foreach (XmlNode CReaction in CReactionsList)
//       {
//         Debug.Log("Chemical reaction");
//       }
//       return true;
//   }

  private bool storeMolecule(XmlNode node, Molecule.eType type)
  {
    Molecule mol = new Molecule();

    mol.setType(type);
    foreach (XmlNode attr in node)
      {
        switch (attr.Name)
          {
          case "name":
            mol.setName(attr.InnerText);
            break;
          case "description":
            mol.setDescription(attr.InnerText);
            break;
          case "concentration":
            mol.setConcentration(float.Parse(attr.InnerText.Replace(",", ".")));
            break;
          case "degradationRate":
            mol.setDegradationRate(float.Parse(attr.InnerText.Replace(",", ".")));
            break;
          }
     }
    _molecules.Add(mol);
    return true;
  }

  private bool loadMolecule(XmlNode node)
  {
    if (node.Attributes["type"] == null)
      return false;
    switch (node.Attributes["type"].Value)
      {
      case "enzyme":
        return storeMolecule(node, Molecule.eType.ENZYME);
      case "transcription_factor":
        return storeMolecule(node, Molecule.eType.TRANSCRIPTION_FACTOR);
      case "other":
        return storeMolecule(node, Molecule.eType.OTHER);
      }
    return true;
  }

  private bool loadMolecules(XmlNode node)
  {
    foreach (XmlNode mol in node)
      {
        if (mol.Name == "molecule")
          loadMolecule(mol);
      }
    return true;
  }

  private bool loadReactions(XmlNode node)
  {
    return loadPromoters(node) && loadEnzymeReactions(node);//  && loadChemicalReactions(node);
  }
  
  private void loadReactionsFromFile(TextAsset filePath)
  {
    bool b = true;

    XmlDocument xmlDoc = new XmlDocument();
    xmlDoc.LoadXml(filePath.text);
    XmlNodeList reactionsLists = xmlDoc.GetElementsByTagName("reactions");
    foreach (XmlNode reactionsNode in reactionsLists)
      b &= loadReactions(reactionsNode);

    XmlNodeList moleculesLists = xmlDoc.GetElementsByTagName("molecules");
    foreach (XmlNode moleculesNode in moleculesLists)
      b &= loadMolecules(moleculesNode);
  }

	// Use this for initialization
  void Start () {
    loadReactionsFromFile(_reactionFile);
  }
  
  // Update is called once per frame
  void Update () {
  }
}
