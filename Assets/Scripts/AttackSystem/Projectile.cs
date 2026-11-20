using UnityEngine;

public class Projectile : Hitbox
{
    [SerializeField] float _lifetime = 5f;
    [SerializeField] TrailRenderer _trail;

    Rigidbody _rb;
    float _speed;

    internal override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody>();
        _rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
    }

    public void Launch(float speed)
    {
        _speed = speed;
        Invoke(nameof(Despawn), _lifetime);
    }

    void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + transform.forward * _speed * Time.fixedDeltaTime);
    }

    protected override void OnHit(Collider other) => Despawn();

    void Despawn()
    {
        if (_trail)
        {
            _trail.transform.SetParent(null);
            _trail.emitting = false;
            _trail.autodestruct = true;
        }
        Destroy(gameObject);
    }
}
