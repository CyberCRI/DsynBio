using System;

class Tools
{
  static public string epurStr(string str)
  {
    char[] charToStrip = {' ', '\t', '\n'};
    string strippedLine = str.TrimStart(charToStrip);
    strippedLine = strippedLine.TrimEnd(charToStrip);
    return strippedLine;
  }
}