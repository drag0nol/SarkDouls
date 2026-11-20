using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Area")]
public class AreaAttack : Attack
{
    public Hitbox hitboxPrefab;
    public float distance = 3f;
    public float activeTime = 0.2f;
    public GameObject castFx;

    public override void Execute(Transform origin, AttackData data)
    {
        Vector3 forward = Vector3.ProjectOnPlane(origin.forward, Vector3.up).normalized;
        Vector3 center = origin.position + forward * distance;
        center.y -= 1;
        Quaternion rotation = Quaternion.LookRotation(forward);

        Hitbox hitbox = Instantiate(hitboxPrefab, center, rotation);
        hitbox.Init(data.damage, data.mask, hitFx);
        Destroy(hitbox.gameObject, activeTime);

        if (castFx) Instantiate(castFx, center, rotation);
    }
}
