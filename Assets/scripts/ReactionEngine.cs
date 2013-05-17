using UnityEngine;
using System.Collections;
using System.Xml;

public class IReaction
{
  protected string _name;
  protected float _beta;
}

public class ReactionEngine : MonoBehaviour {

  public TextAsset _reactionFile;
  private Parser _parser;

  private bool loadPromoters(XmlNode node)
  {
    XmlNodeList promotersList = node.SelectNodes("promoter");
    foreach (XmlNode promoter in promotersList)
      {
        Debug.Log("promoter");
      }
    return true;
  }

  private bool loadEnzymeReactions(XmlNode node)
  {
    XmlNodeList EReactionsList = node.SelectNodes("enzyme");
    foreach (XmlNode EReaction in EReactionsList)
      {
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
