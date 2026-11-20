using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Melee")]
public class MeleeAttack : Attack
{
    public Hitbox hitboxPrefab;
    public float activeDuration = 0.15f;

    public override void Execute(Transform origin, AttackData data)
    {
        Hitbox hitbox = Instantiate(hitboxPrefab, origin.position, origin.rotation, origin);
        hitbox.Init(data.damage, data.mask, hitFx);
        Destroy(hitbox.gameObject, activeDuration);
    }
}
