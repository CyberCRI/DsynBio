using System;
using System.IO;
using System.Text;

class Tools
{
  static public string epurStr(string str)
  {
    char[] charToStrip = {' ', '\t', '\n'};
    string strippedLine = str.TrimStart(charToStrip);
    strippedLine = strippedLine.TrimEnd(charToStrip);
    return strippedLine;
  }

  static public MemoryStream getEncodedFileContent(string path)
  {
    StreamReader fileStream = new StreamReader(@path);
    string text = fileStream.ReadToEnd();
    fileStream.Close();
    byte[] encodedString = Encoding.UTF8.GetBytes(text);
    MemoryStream ms = new MemoryStream(encodedString);
    ms.Flush();
    ms.Position = 0;

    return ms;
  }
}