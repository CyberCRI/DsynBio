using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System;
using System.IO;

public class FickLoader
{


  public FickLoader()
  {
    
  }

  public FickProprieties   loadFickProprieties(XmlNode node)
  {
    FickProprieties props = new FickProprieties();

    foreach (XmlNode attr in node)
      {
        switch (attr.Name)
          {
          case "MediumId1":
            props.MediumId1 = Convert.ToInt32(attr.InnerText);
            break;
          case "MediumId2":
            props.MediumId2 = Convert.ToInt32(attr.InnerText);
            break;
          case "P":
            props.P = float.Parse(attr.InnerText.Replace(",", "."));
            break;
          case "surface":
            props.surface = float.Parse(attr.InnerText.Replace(",", "."));
            break;
          }
      }
    return props;
  }

  public LinkedList<FickProprieties>     loadFickProprietiesFromFile(string filePath)
  {
    LinkedList<FickProprieties> ficksProps = new LinkedList<FickProprieties>();
    FickProprieties prop;

    MemoryStream ms = Tools.getEncodedFileContent(filePath);
    XmlDocument xmlDoc = new XmlDocument();
    xmlDoc.Load(ms);

//     XmlDocument xmlDoc = new XmlDocument();
//     Debug.Log(filePath);
//     xmlDoc.LoadXml(filePath);
    XmlNodeList ficksLists = xmlDoc.GetElementsByTagName("ficks");
    foreach (XmlNode ficksNodes in ficksLists)
      {
        foreach (XmlNode fickNode in ficksNodes)
          {
            if (fickNode.Name == "fickProp")
              {
                prop = loadFickProprieties(fickNode);
                ficksProps.AddLast(prop);
              }
          }
      }

//     StreamReader fileStream = new StreamReader(@filePath);
// //     LinkedList<FickProprieties> mediums;

//     string text = fileStream.ReadToEnd();
//     Debug.Log(text);
//     fileStream.Close();

    if (ficksProps.Count == 0)
      return null;
    return ficksProps;
  }
}