using UnityEngine;

public struct AttackData
{
    public int damage;
    public LayerMask mask;

    public AttackData(int damage, LayerMask mask)
    {
        this.damage = damage;
        this.mask = mask;
    }
}

public abstract class Attack : ScriptableObject
{
    public int damage;
    public float cooldown;
    public LayerMask targetMask;
    public GameObject hitFx;

    /// <summary>Valeurs de cet asset.</summary>
    public AttackData Data => new AttackData(damage, targetMask);

    /// <summary>Exécute l'attaque avec ses propres valeurs (utilisé par le joueur).</summary>
    public void Execute(Transform origin) => Execute(origin, Data);

    /// <summary>Exécute l'attaque avec des valeurs données.</summary>
    public abstract void Execute(Transform origin, AttackData data);
}
