using UnityEngine;

public class EnemySensor : MonoBehaviour
{
    [Header("Vision")]
    [SerializeField] float _viewRadius = 10f;
    [SerializeField, Range(0, 360)] float _viewAngle = 120f;
    [SerializeField] float _eyeHeight = 1.5f;
    [SerializeField] LayerMask _obstacleMask;

    [Header("Attaques")]
    [Tooltip("Les portées sont lues dans les attaques de ce controller")]
    [SerializeField] AttackController _attackController;

    PlayerTarget _target;

    public Transform Target => _target ? _target.transform : null;
    public bool CanSeeTarget { get; private set; }
    public bool InAttackRange { get; private set; }
    public bool HasLastKnownPosition { get; private set; }
    public Vector3 LastKnownPosition { get; private set; }
    public float DistanceToTarget { get; private set; } = Mathf.Infinity;

    void Awake()
    {
        if (!_attackController) _attackController = GetComponent<AttackController>();
    }

    void Update()
    {
        if (!(CanSeeTarget && IsVisible(_target, false)))
            _target = FindClosestVisible();

        CanSeeTarget = _target != null;

        if (CanSeeTarget)
        {
            DistanceToTarget = FlatDistance(_target.transform.position);
            InAttackRange = DistanceToTarget <= _attackController.MaxRange;
            LastKnownPosition = _target.transform.position;
            HasLastKnownPosition = true;
        }
        else
        {
            DistanceToTarget = Mathf.Infinity;
            InAttackRange = false;
        }
    }

    PlayerTarget FindClosestVisible()
    {
        PlayerTarget best = null;
        float bestDistance = Mathf.Infinity;

        foreach (PlayerTarget player in PlayerTarget.All)
        {
            if (!IsVisible(player, true))
                continue;

            float distance = FlatDistance(player.transform.position);
            if (distance < bestDistance)
            {
                bestDistance = distance;
                best = player;
            }
        }
        return best;
    }

    bool IsVisible(PlayerTarget player, bool checkAngle)
    {
        if (!player || !player.isActiveAndEnabled)
            return false;

        Vector3 toPlayer = player.transform.position - transform.position;
        toPlayer.y = 0f;

        if (toPlayer.magnitude > _viewRadius)
            return false;

        if (checkAngle && Vector3.Angle(transform.forward, toPlayer) > _viewAngle * 0.5f)
            return false;

        Vector3 eye = transform.position + Vector3.up * _eyeHeight;
        Vector3 targetPoint = player.transform.position + Vector3.up * _eyeHeight;
        return !Physics.Linecast(eye, targetPoint, _obstacleMask);
    }

    float FlatDistance(Vector3 position)
    {
        Vector3 d = position - transform.position;
        d.y = 0f;
        return d.magnitude;
    }

    public void ForgetTarget()
    {
        HasLastKnownPosition = false;
        _target = null;
    }
}