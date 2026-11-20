using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Hitbox : MonoBehaviour
{
    protected int _damage;
    protected LayerMask _mask;
    protected GameObject _hitFx;

    readonly HashSet<LivingEntity> _alreadyHit = new HashSet<LivingEntity>();

    internal virtual void Awake()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
        GetComponent<Collider>().isTrigger = true;
    }

    public void Init(int damage, LayerMask mask, GameObject hitFx)
    {
        _damage = damage;
        _mask = mask;
        _hitFx = hitFx;
    }

    void OnTriggerEnter(Collider other)
    {
        if ((_mask.value & (1 << other.gameObject.layer)) == 0) return;

        if (other.TryGetComponent(out LivingEntity entity) && _alreadyHit.Add(entity))
        {
            entity.TakeDamage(_damage);
            if (_hitFx) Instantiate(_hitFx, other.ClosestPoint(transform.position), Quaternion.identity);
        }

        OnHit(other);
    }

    protected virtual void OnHit(Collider other) { }

}
