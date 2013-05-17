using UnityEngine;
using System.Collections;
using System.Xml;
using System;

public class ReactionEngine : MonoBehaviour {

  public TextAsset _reactionFile;

  private Parser _parser;

  private IReaction[]   _reactions;

  public ReactionEngine()
  {
    _parser = new Parser();
  }

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
    Debug.Log("terminatorFactor set to : " + prom.getTerminatorFactor());
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

    foreach (XmlNode attr in node)
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
      return loadGene(prom, name, RBSf);
    if (!n)
      Debug.Log("Error : Missing Gene name in operon");
    if (!rbsf)
      Debug.Log("Error : Missing RBSfactor in operon");
    return false;
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

  // FIXME : checkout what to do with the boolean
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
            Debug.Log(attr.Name);
          }
      }
    return b;
  }

  private bool loadEnzymeReactions(XmlNode node)
  {
    XmlNodeList EReactionsList = node.SelectNodes("enzyme");
    foreach (XmlNode EReaction in EReactionsList)
      {
        EnzymeReaction er = new EnzymeReaction();
        Debug.Log("Enzyme reaction");
      }
      return true;
  }

  private bool loadChemicalReactions(XmlNode node)
  {
    XmlNodeList CReactionsList = node.SelectNodes("chemical");
    foreach (XmlNode CReaction in CReactionsList)
      {
        Debug.Log("Chemical reaction");
      }
      return true;
  }

  private bool loadReactions(XmlNode node)
  {
    return loadPromoters(node) && loadEnzymeReactions(node) && loadChemicalReactions(node);
  }

  private void loadReactionsFromFile(TextAsset filePath)
  {
    XmlDocument xmlDoc = new XmlDocument();
    xmlDoc.LoadXml(filePath.text);
    XmlNodeList reactionsLists = xmlDoc.GetElementsByTagName("reactions"); 
    foreach (XmlNode reactionsNode in reactionsLists)
      {
        loadReactions(reactionsNode);
      }
  }

	// Use this for initialization
  void Start () {
    loadReactionsFromFile(_reactionFile);
    //           Parser p = new Parser();
    //           TreeNode<NodeData> tree = p.Parse("[0.5]X*(![0.4]Y|[0.5]A*![0.4]B)");
    //           p.PPTree(tree);
  }
  
  // Update is called once per frame
  void Update () {
  }
}
