using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using System.IO;
using System.Text;

public class Lexer
{
  public enum eToken
  {
    OP_OR,
    OP_AND,
    LPAR,
    RPAR,
    OP_NOT,
    LHOOK,
    RHOOK,
    NUM,
    CHAR,
    END
  }

  public struct Token
  {
    public Token(eToken t, string c)
    {
      this.token = t;
      this.c = c;
    }
    public eToken       token;
    public string       c;
  }

  static private Token[] _tokenTab = new Token[]
    {
      new Token(eToken.OP_OR, "|"),
      new Token(eToken.OP_AND, "*"),
      new Token(eToken.LPAR, "("),
      new Token(eToken.RPAR, ")"),
      new Token(eToken.OP_NOT, "!"),
      new Token(eToken.LHOOK, "["),
      new Token(eToken.RHOOK, "]"),
      new Token(eToken.NUM, "0123456789."),
    };

  
  public Lexer.Token getToken(char c)
  {
    foreach (Token i in _tokenTab)
      {
        if (i.c.IndexOf(c) != -1)
          return new Token(i.token, new string(c, 1));
      }
    return new Token(eToken.CHAR, new string(c, 1));
  }

  public LinkedList<Token> lex(string str)
  {
    LinkedList<Token> tokList = new LinkedList<Token>();

    foreach (char c in str)
      tokList.AddLast(getToken(c));
    tokList.AddLast(new Token (eToken.END, ""));
    return tokList;
  }

// ==================== Debug ========================
  
  public void PPTokenList(LinkedList<Token> list)
  {
    int i = 0;
    string tokStr = "UNKNOWN_TOKEN =(";

    foreach (Token t in list)
      {
        switch (t.token)
          {
          case eToken.OP_OR:
            tokStr = "OP_OR";
            break;
          case eToken.OP_AND:
            tokStr = "OP_AND";
            break;
          case eToken.LPAR:
            tokStr = "LPAR";
            break;
          case eToken.RPAR:
            tokStr = "RPAR";
            break;
          case eToken.OP_NOT:
            tokStr = "OP_NOT";
            break;
          case eToken.LHOOK:
            tokStr = "LHOOK";
            break;
          case eToken.RHOOK:
            tokStr = "RHOOK";
            break;
          case eToken.NUM:
            tokStr = "NUM";
            break;
          case eToken.CHAR:
            tokStr = "CHAR";
            break;
          case eToken.END:
            tokStr = "END";
            break;
          }
        Debug.Log("Token " + i + " : " + t.c + " -> " + tokStr);
        i++;
      }
  }
}

public class NodeData
{
  public NodeData(Parser.eNodeType t = default(Parser.eNodeType), string v = default(string))
  {
    token = t;
    value = v;
  }
  public Parser.eNodeType   token  {get; set;}
  public string         value  {get; set;}
}

public class Parser
{
  public enum eNodeType
  {
    OR,
    AND,
    NOT,
    CONSTANT,
    WORD,
    NUM,
    BOOL
  }


  LinkedList<Lexer.Token>     _restoreList;
  int                   _restoreStatus;

  public Parser()
  {
    _restoreStatus = 0;
    _restoreList = new LinkedList<Lexer.Token>();
  }



  public void           popToken(LinkedList<Lexer.Token> list)
  {
    Lexer.Token tok = list.First();
    list.RemoveFirst();
    _restoreList.AddFirst(tok);
    _restoreStatus += 1;
  }

  public void           restoreToken(LinkedList<Lexer.Token> list)
  {
    Lexer.Token tok = _restoreList.First();
    _restoreList.RemoveFirst();
    list.AddFirst(tok);
    _restoreStatus -= 1;
  }

  public void           restoreListState(LinkedList<Lexer.Token> list, int state)
  {
    while (_restoreStatus > state)
      restoreToken(list);
  }

  public int            getRestoreListStatus()
  {
    return _restoreStatus;
  }

  public TreeNode<NodeData>     ParseFormula(LinkedList<Lexer.Token> tokenList)
  {
    return ParseORExpr(tokenList);
  }
  public TreeNode<NodeData>     ParseORExpr(LinkedList<Lexer.Token> tokenList)
  {
    NodeData data = new NodeData();
    TreeNode<NodeData> left = null;
    TreeNode<NodeData> right = null;
    int restoreStatus;

    restoreStatus = getRestoreListStatus();
    if ((left = ParseAndExpr(tokenList)) == null)
      {
        restoreListState(tokenList, restoreStatus);
        return null;
      }
    if (tokenList.First().token == Lexer.eToken.OP_OR)
      {
        Lexer.Token tok = tokenList.First();
        popToken(tokenList);
        if ((right = ParseORExpr(tokenList)) == null)
          {
            restoreListState(tokenList, restoreStatus);
            Debug.Log("Syntax error : bad OR expr");
            return null;
          }
        data.token = Parser.eNodeType.OR;
        data.value = tok.c;
        return new TreeNode<NodeData>(data, left, right);
      }
    else
      return left;
  }

  public TreeNode<NodeData>     ParseAndExpr(LinkedList<Lexer.Token> tokenList)
  {
    NodeData data = new NodeData();
    TreeNode<NodeData> left = null;
    TreeNode<NodeData> right = null;
    int restoreStatus;

    restoreStatus = getRestoreListStatus();
    if ((left = ParseParExpr(tokenList)) == null)
      {
        restoreListState(tokenList, restoreStatus);
        return null;
      }
    if (tokenList.First().token == Lexer.eToken.OP_AND)
      {
        Lexer.Token tok = tokenList.First();
        popToken(tokenList);
        if ((right = ParseAndExpr(tokenList)) == null)
          {
            restoreListState(tokenList, restoreStatus);
            Debug.Log("Syntax error : bad AND expr");
            return null;
          }
        data.token = Parser.eNodeType.AND;
        data.value = tok.c;
        return new TreeNode<NodeData>(data, left, right);
      }
    else
      return left;
  }

  public TreeNode<NodeData>     ParseParExpr(LinkedList<Lexer.Token> tokenList)
  {
    int restoreStatus;
    TreeNode<NodeData> node = null;        

    restoreStatus = getRestoreListStatus();
    if (tokenList.First().token == Lexer.eToken.LPAR)
      {
        popToken(tokenList);
        if ((node = ParseORExpr(tokenList)) == null)
          {
            restoreListState(tokenList, restoreStatus);
            Debug.Log("Syntax error : bad ParExpr");
            return null;
          }
        if (tokenList.First().token != Lexer.eToken.RPAR)
          {
            restoreListState(tokenList, restoreStatus);
            Debug.Log("Syntax error : bad ParExpr : missing closing parenthis");
            return null;
          }
        popToken(tokenList);
        return node;
      }
    if ((node = ParseNotExpr(tokenList)) == null)
      {
        restoreListState(tokenList, restoreStatus);
        return null;
      }
    return node;
  }


  public TreeNode<NodeData>     ParseNotExpr(LinkedList<Lexer.Token> tokenList)
  {
    NodeData data = new NodeData();
    TreeNode<NodeData> node = new TreeNode<NodeData>(data);
    int restoreStatus;

    restoreStatus = getRestoreListStatus();
    if (tokenList.First().token == Lexer.eToken.OP_NOT)
      {
        Lexer.Token tok = tokenList.First();
        popToken(tokenList);
        if ((node = ParseOperandExpr(tokenList)) == null)
          {
            restoreListState(tokenList, restoreStatus);
            Debug.Log("Syntax Error: Bad Not Expr");
            return null;
          }
        data.token = Parser.eNodeType.NOT;
        data.value = tok.c;
        return new TreeNode<NodeData>(data, node);
      }
    return ParseOperandExpr(tokenList);
  }

  public TreeNode<NodeData>     ParseOperandExpr(LinkedList<Lexer.Token> tokenList)
  {
    TreeNode<NodeData> left;
    TreeNode<NodeData> right;
    NodeData    data = new NodeData();
    int restoreStatus;

    restoreStatus = getRestoreListStatus();

    if ((right = ParseBool(tokenList)) != null)
      {
        data.token = Parser.eNodeType.CONSTANT;
        data.value = "C";
        return new TreeNode<NodeData>(data, null, right);
      }
    if ((left = ParseConstants(tokenList)) == null)
      {
        restoreListState(tokenList, restoreStatus);
        Debug.Log("Syntax error : No constant defined");
        return null;
      }
    if ((right = ParseWord(tokenList)) == null)
      {
        restoreListState(tokenList, restoreStatus);
        Debug.Log("Syntax error : Need name for operand");
        return null; 
      }
    data.token = Parser.eNodeType.CONSTANT;
    data.value = "C";
    return new TreeNode<NodeData>(data, left, right);
  }

  public TreeNode<NodeData>     ParseBool(LinkedList<Lexer.Token> tokenList)
  {
    NodeData data = new NodeData();
    int restoreStatus;
    string value = "";

    restoreStatus = getRestoreListStatus();
    if (tokenList.First().token != Lexer.eToken.CHAR)
      {
        restoreListState(tokenList, restoreStatus);
        Debug.Log("Syntax error : A word need to begin with a Character.");        
        return null;
      }
    value += tokenList.First().c;
    popToken(tokenList);

    if (value == "T" || value == "F")
      data.token = Parser.eNodeType.BOOL;
    else
      {
        restoreListState(tokenList, restoreStatus);
        return null;        
      }
    data.value = value;
    return new TreeNode<NodeData>(data);
  }

  public TreeNode<NodeData>     ParseConstants(LinkedList<Lexer.Token> tokenList)
  {
    TreeNode<NodeData> node;
    int restoreStatus;

    restoreStatus = getRestoreListStatus();
    if (tokenList.First().token != Lexer.eToken.LHOOK)
      {
        Debug.Log("Syntax error : Need a '[' character to define constants");
        return null;
      }
    popToken(tokenList);
    if ((node = ParseNumber(tokenList)) == null)
      {
        restoreListState(tokenList, restoreStatus);
        return null;
      }
    if (tokenList.First().token != Lexer.eToken.RHOOK)
      {
        restoreListState(tokenList, restoreStatus);
        Debug.Log("Syntax error : Need a ']' character to define constants");
        return null;
      }
    popToken(tokenList);
    return node;
  }

  public TreeNode<NodeData>     ParseWord(LinkedList<Lexer.Token> tokenList)
  {
    NodeData data = new NodeData();
    int restoreStatus;
    string value = "";

    restoreStatus = getRestoreListStatus();
    if (tokenList.First().token != Lexer.eToken.CHAR)
      {
        restoreListState(tokenList, restoreStatus);
        Debug.Log("Syntax error : A word need to begin with a Character.");        
        return null;
      }
    value += tokenList.First().c;
    popToken(tokenList);
    while (tokenList.First().token == Lexer.eToken.CHAR || tokenList.First().token == Lexer.eToken.NUM)
      {
        value += tokenList.First().c;
        popToken(tokenList);
      }
    data.token = Parser.eNodeType.WORD;
    data.value = value;
    return new TreeNode<NodeData>(data);
  }

  public TreeNode<NodeData>     ParseNumber(LinkedList<Lexer.Token> tokenList)
  {
    NodeData data = new NodeData();
    int restoreStatus;
    string value = "";

    restoreStatus = getRestoreListStatus();
    if (tokenList.First().token != Lexer.eToken.NUM)
      {
        restoreListState(tokenList, restoreStatus);
        Debug.Log("Syntax error : A Number need to begin with a number.");
        return null;
      }
    value += tokenList.First().c;
    popToken(tokenList);
    while (tokenList.First().token == Lexer.eToken.NUM)
      {
        value += tokenList.First().c;
        popToken(tokenList);
      }
    data.token = Parser.eNodeType.WORD;
    data.value = value;
    return new TreeNode<NodeData>(data);
  }


  public TreeNode<NodeData> Parse(string str)
  {
    clear();
    Lexer lex = new Lexer();
    LinkedList<Lexer.Token> tokenList = lex.lex(str);
//     lex.PPTokenList(tokenList);
    TreeNode<NodeData> tree = ParseFormula(tokenList);
    return tree;
  }

  public void clear()
  {
    _restoreList.Clear();
    _restoreStatus = 0;
  }



// ========================== Pretty Print Tree =================================

  public void  PPRecTree(TreeNode<NodeData> node, ref string str)
  {
    if (node != null)
      {
        if (node.getLeftNode() != null)
          {
            str += "\"" + node.getData().value + "\"" + "->" + "\"" +node.getLeftNode().getData().value + "\"\n";
            PPRecTree(node.getLeftNode(), ref str);            
          }
        if (node.getRightNode() != null)
          {
            str += "\"" + node.getData().value + "\"" + "->" + "\"" + node.getRightNode().getData().value + "\"\n";
            PPRecTree(node.getRightNode(), ref str);
          }
      }
  }

  public void PPTree(TreeNode<NodeData> tree)
  {
    string output;
    if (tree == null)
      Debug.Log("failed");
    output = "Digraph G {";
    PPRecTree(tree, ref output);
    output += "}";
    string path = "graph.txt";

    // This text is added only once to the file. 
//     if (!File.Exists(path))
//       {
//         File.WriteAllText(path, output);
//       }
  }
}