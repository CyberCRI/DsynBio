using System.IO;
using System.Collections.Generic;
using System.Xml;
using System;
using UnityEngine;


public class MediumLoader
{


  public MediumLoader()
  {
    
  }

  public Medium   loadMedium(XmlNode node)
  {
    Medium medium = new Medium();

    foreach (XmlNode attr in node)
      {
        switch (attr.Name)
          {
          case "Id":
            medium.setId(Convert.ToInt32(attr.InnerText));
            break;
          case "Name":
            medium.setName(attr.InnerText);
            break;
          case "ReactionsSet":
            medium.setReactionsSet(attr.InnerText);
            break;
          case "MoleculesSet":
            medium.setMoleculesSet(attr.InnerText);
            break;
          }
      }
    return medium;
  }

  public LinkedList<Medium>     loadMediumsFromFile(string filePath)
  {

    LinkedList<Medium> mediums = new LinkedList<Medium>();
    Medium medium;


    MemoryStream ms = Tools.getEncodedFileContent(filePath);
    XmlDocument xmlDoc = new XmlDocument();
    xmlDoc.Load(ms);

//     XmlDocument xmlDoc = new XmlDocument();
//     Debug.Log(filePath);
//     xmlDoc.LoadXml(filePath);
    XmlNodeList mediumsLists = xmlDoc.GetElementsByTagName("Mediums");
    foreach (XmlNode mediumNodes in mediumsLists)
      {
        foreach (XmlNode mediumNode in mediumNodes)
          {
            medium = loadMedium(mediumNode);
            mediums.AddLast(medium);
          }
      }

//     StreamReader fileStream = new StreamReader(@filePath);
// //     LinkedList<Medium> mediums;

//     string text = fileStream.ReadToEnd();
//     Debug.Log(text);
//     fileStream.Close();

    if (mediums.Count == 0)
      return null;
    return mediums;
  }
}