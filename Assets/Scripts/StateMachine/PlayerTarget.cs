using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// À mettre sur le prefab du joueur.
/// Le joueur s'enregistre automatiquement quand il apparaît et se retire quand il disparaît.
/// </summary>
public class PlayerTarget : MonoBehaviour
{
    static readonly List<PlayerTarget> _all = new List<PlayerTarget>();
    public static IReadOnlyList<PlayerTarget> All => _all;

    void OnEnable() => _all.Add(this);
    void OnDisable() => _all.Remove(this);
}
