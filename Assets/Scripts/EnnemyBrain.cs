using UnityEngine;
using UnityEngine.AI;

public class EnnemyBrain : MonoBehaviour
{
    public enum AttackEnum
    {
        First,
        Second,
        Third,
        Ultimate
    }

    [Header("Dépendances")]
    [SerializeField] Movement _moveSystem;
    [SerializeField] AttackController _attackController;
    [SerializeField] AimSystem _aimSystem;
    [SerializeField] Animator _animator;

    [Header("Déplacement")]
    [Tooltip("Distance à laquelle un point du chemin est considéré atteint")]
    [SerializeField] float _cornerTolerance = 0.3f;
    [Tooltip("Distance max pour projeter une position sur le NavMesh")]
    [SerializeField] float _navMeshSampleDistance = 2f;
    [SerializeField] float _turnSpeed = 720f;

    NavMeshPath _path;
    Vector3[] _corners = new Vector3[0];
    int _cornerIndex;

    public bool IsMoving { get; private set; }

    void Awake()
    {
        _path = new NavMeshPath();
    }

    void Update()
    {
        if (IsMoving)
            FollowPath();
    }

    public bool MoveTo(Vector3 destination)
    {
        if (!SampleNavMesh(transform.position, out Vector3 start) ||
            !SampleNavMesh(destination, out Vector3 end) ||
            !NavMesh.CalculatePath(start, end, NavMesh.AllAreas, _path) ||
            _path.status == NavMeshPathStatus.PathInvalid ||
            _path.corners.Length < 2)
        {
            Stop();
            return false;
        }

        _corners = _path.corners;
        _cornerIndex = 1;
        IsMoving = true;
        return true;
    }

    public void Stop()
    {
        IsMoving = false;
        _moveSystem.SetDirection(Vector3.zero);
        _animator.SetFloat("speed", 0);
    }

    void FollowPath()
    {
        Vector3 toCorner = _corners[_cornerIndex] - transform.position;
        toCorner.y = 0f;

        if (toCorner.magnitude <= _cornerTolerance)
        {
            _cornerIndex++;
            if (_cornerIndex >= _corners.Length)
            {
                Stop();
                return;
            }
            toCorner = _corners[_cornerIndex] - transform.position;
            toCorner.y = 0f;
        }

        Vector3 dir = toCorner.normalized;
        _moveSystem.SetDirection(dir);
        if (dir.sqrMagnitude > 0.001f)
            _aimSystem.SetDirection(dir);
        _animator.SetFloat("speed", 1);
    }

    bool SampleNavMesh(Vector3 position, out Vector3 result)
    {
        if (NavMesh.SamplePosition(position, out NavMeshHit hit, _navMeshSampleDistance, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }
        result = position;
        return false;
    }

    public bool RandomPoint(Vector3 center, float radius, out Vector3 result)
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 random = center + Random.insideUnitSphere * radius;
            if (NavMesh.SamplePosition(random, out NavMeshHit hit, _navMeshSampleDistance, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = center;
        return false;
    }

    public void FaceTarget(Vector3 position)
    {
        Vector3 dir = position - transform.position;
        dir.y = 0f;
        if (dir.sqrMagnitude < 0.001f) return;

        Quaternion look = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, look, _turnSpeed * Time.deltaTime);
    }

    public void AimAt(Vector3 position)
    {
        Vector3 dir = position - transform.position;
        dir.y = 0f;
        if (dir.sqrMagnitude < 0.001f) return;

        _aimSystem.SetDirection(dir.normalized);
    }

    public int FindUsableAttack(float distance)
    {
        int best = -1;
        float bestRange = Mathf.Infinity;

        for (int i = 0; i < _attackController.AttackCount; i++)
        {
            if (!(_attackController.GetAttack(i) is IAAttack attack) ||
                !_attackController.IsReady(i) || !attack.IsInRange(distance))
                continue;

            if (attack.range < bestRange)
            {
                bestRange = attack.range;
                best = i;
            }
        }
        return best;
    }

    public float SmallestAttackRange => _attackController.MinRange;

    public bool Attack(int index)
    {
        if (!_attackController.TryAttack(index))
            return false;

        if (_attackController.GetAttack(index) is IAAttack ia && !string.IsNullOrEmpty(ia.animatorTrigger))
            _animator.SetTrigger(ia.animatorTrigger);
        return true;
    }
}