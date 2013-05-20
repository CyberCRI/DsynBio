using System.Collections.Generic;

// public abstract class IFormula
// {
//   private abstract formula;
// }

public class Molecule
{
  
}

public class Product
{
  protected string _name;
  // FIXME : Potentially add a ptr to the molecule
  public void setName(string name) { _name = Tools.epurStr(name); }
  public string getName() { return _name; }
}

public class GeneProduct : Product
{
  private float _RBSf;
  
  public void setRBSFactor(float value) { _RBSf = value; }
  public float getRBSFactor() { return _RBSf; }  
}


public abstract class IReaction
{
  protected string _name;
  protected float _beta;
  protected LinkedList<Product> _products;

  public IReaction()
  {
    _products = new LinkedList<Product>();
  }

  public void setName(string name) { _name = Tools.epurStr(name); }
  public string getName() { return _name; }
  public void setBeta(float beta) { _beta = beta; }
  public float getBeta() { return _beta; }

  public abstract void react();
  public void addProduct(Product prod) { if (prod != null) _products.AddLast(prod); }
}

public class Promoter : IReaction
{
  private float _terminatorFactor;
  private TreeNode<NodeData> _formula;

//   public Promoter(string name = null, float beta = 0)
//   {
//     _products = new LinkedList<GeneProduct>();
//   }

  public void setTerminatorFactor(float v) { _terminatorFactor = v; }
  public float getTerminatorFactor() { return _terminatorFactor; }
  public void setFormula(TreeNode<NodeData> tree) { _formula = tree; }
  public TreeNode<NodeData> getFormula() { return _formula; }

  public override void react()
  {
    
  }

}

public class EnzymeReaction : IReaction
{
//   public EnzymeReaction(string name = null, float beta = 0)
//   {
//     _products = new LinkedList<Product>();
//   }

  public override void react()
  {
    
  }
}