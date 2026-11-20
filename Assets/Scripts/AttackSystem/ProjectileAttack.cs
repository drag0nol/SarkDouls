using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Projectile")]
public class ProjectileAttack : Attack
{
    public Projectile projectilePrefab;
    public float speed = 15f;

    public override void Execute(Transform origin, AttackData data)
    {
        Projectile projectile = Instantiate(projectilePrefab, origin.position, origin.rotation);
        projectile.Init(data.damage, data.mask, hitFx);
        projectile.Launch(speed);
    }
}
