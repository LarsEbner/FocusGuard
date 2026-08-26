using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
public class AutoDeletingList<T> : IEnumerable<T>
{
    private readonly List<T> items;
    private readonly Func<T, bool> canRemove;

    /// <param name="canRemove">
    /// Entscheidet pro Eintrag, ob er entfernt werden darf (z. B. weil er
    /// alt genug ist, oder eine Distraction längst abgeschlossen ist).
    /// Standard: immer entfernbar.
    /// </param>
    public AutoDeletingList(List<T> items, Func<T, bool> canRemove = null)
    {
        this.items = items;
        this.canRemove = canRemove ?? (_ => true);
    }

    public void Add(T item)
    {
        items.Add(item);
    }

    /// <summary>
    /// Entfernt zuerst vom Anfang der Liste alle Einträge, für die
    /// canRemove true liefert, und liefert dann einen Enumerator über
    /// die verbleibenden Einträge.
    /// </summary>
    public IEnumerator<T> GetEnumerator()
    {
        RemoveOutdated();
        return items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private void RemoveOutdated()
    {
        // Nur vom Anfang her prüfen: sobald ein Eintrag nicht entfernt
        // werden darf, abbrechen – alles danach ist chronologisch jünger.
        while (items.Count > 0 && canRemove(items[0]))
        {
            items.RemoveAt(0);
        }
    }
}