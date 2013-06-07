using System.Collections;
using System.Collections.Generic;

public class MoleculesSet
{
  public string                 id;
  public ArrayList              molecules;
}

public class ReactionsSet
{
  public string                  id;
  public LinkedList<IReaction>   reactions;
}