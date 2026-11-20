using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/IA Attack")]
public class IAAttack : Attack
{
    [Header("Attaque à utiliser")]
    public Attack attack;

    [Header("IA")]
    [Tooltip("Distance max à laquelle l'IA utilise cette attaque")]
    public float range = 2f;
    [Tooltip("Distance min à laquelle l'IA utilise cette attaque")]
    public float minRange = 0f;
    [Tooltip("Trigger de l'Animator lancé avec l'attaque (vide = aucun)")]
    public string animatorTrigger;

    [SerializeField, HideInInspector] Attack _lastAttack;

    public bool IsInRange(float distance) => distance >= minRange && distance <= range;

    public override void Execute(Transform origin, AttackData data)
    {
        if (attack && attack != this) attack.Execute(origin, data);
    }


    void OnValidate()
    {
        if (attack == this) attack = null;
        if (attack == _lastAttack) return;

        _lastAttack = attack;
        CopyFromAttack();
    }

    [ContextMenu("Recopier les valeurs de l'attaque")]
    void CopyFromAttack()
    {
        if (!attack) return;
        damage = attack.damage;
        cooldown = attack.cooldown;
        hitFx = attack.hitFx;
    }
}
