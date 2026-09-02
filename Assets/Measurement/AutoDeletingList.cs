using NUnit.Framework.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Liste, die veraltete Einträge automatisch entfernt, sobald über sie
/// iteriert wird (foreach, LINQ, ...). Es gibt keine separate
/// "Aufräum"-Methode, die man manuell aufrufen müsste – das Entfernen
/// alter Einträge passiert als Seiteneffekt jeder Enumeration, direkt in
/// GetEnumerator().
/// </summary>
/// <remarks>
/// Einträge müssen chronologisch hinzugefügt werden (Add ans Ende), da
/// beim Aufräumen nur vom Anfang der Liste her geprüft wird: sobald
/// canRemove für einen Eintrag false liefert, wird abgebrochen.
/// </remarks>
public class AutoDeletingList<T> : ICollection<T>
{
    private readonly ICollection<T> items;
    private readonly Func<T, bool> canRemove;


    /// <param name="canRemove">
    /// Entscheidet pro Eintrag, ob er entfernt werden darf (z. B. weil er
    /// alt genug ist, oder eine Distraction längst abgeschlossen ist).
    /// Standard: immer entfernbar.
    /// </param>
    public AutoDeletingList(ICollection<T> items, Func<T, bool> canRemove = null)
    {
        this.items = items;
        this.canRemove = canRemove ?? (_ => true);
    }

    public void Add(T item)
    {
        items.Add(item);
    }

    public bool IsReadOnly => items.IsReadOnly;

    public int Count => items.Count;

    /// <summary>
    /// Entfernt beim Erzeugen des Enumerators alle Einträge,
    /// für die canRemove true liefert.
    ///
    /// Die Collection muss dabei keine bestimmte Reihenfolge besitzen.
    /// </summary>
    public IEnumerator<T> GetEnumerator()
    {
        RemoveOutdated();
        return items.GetEnumerator();
    }


    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }


    private void RemoveOutdated()
    {
        List<T> itemsToRemove = new();

        foreach (T item in items)
        {
            if (canRemove(item))
            {
                itemsToRemove.Add(item);
            }
        }

        foreach (T item in itemsToRemove)
        {
            items.Remove(item);
        }
    }

    public void Clear()
    {
        items.Clear();
    }

    public bool Contains(T item)
    {
        return items.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        items.CopyTo(array, arrayIndex);
    }

    public bool Remove(T item)
    {
        return items.Remove(item);
    }
}