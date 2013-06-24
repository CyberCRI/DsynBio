using System.Collections.Generic;

//! Usefull functions for LinkedList
public static class LinkedListExtensions   
{
  /*!
    \brief Append a IEnumerable at the end of another LinkedList
    \param source The destination list
    \param items The list to append
   */
  public static void AppendRange<T>(this LinkedList<T> source,
                                    IEnumerable<T> items)
  {
    foreach (T item in items)
        source.AddLast(item);
  }

  /*!
    \brief Append a IEnumerable at the end of another LinkedList
    \param source The destination list
    \param items The list to prepend
   */
  public static void PrependRange<T>(this LinkedList<T> source,
                                     IEnumerable<T> items)
  {
    LinkedListNode<T> first = source.First;
    foreach (T item in items)
        source.AddBefore(first, item);
  }
}