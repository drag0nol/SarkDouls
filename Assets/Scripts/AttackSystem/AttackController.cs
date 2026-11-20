using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] Transform _attackPosition;
    [SerializeField] Attack[] _attacks;

    float[] _cooldownRemaining;

    public int AttackCount => _attacks.Length;
    public Attack GetAttack(int index) => _attacks[index];
    public bool IsReady(int index) => _cooldownRemaining[index] <= 0f;

    /// <summary>Plus grande portée parmi les IAAttack (0 si aucune).</summary>
    public float MaxRange
    {
        get
        {
            float max = 0f;
            foreach (Attack a in _attacks)
                if (a is IAAttack ia && ia.range > max) max = ia.range;
            return max;
        }
    }

    /// <summary>Plus petite portée parmi les IAAttack (0 si aucune).</summary>
    public float MinRange
    {
        get
        {
            float min = Mathf.Infinity;
            foreach (Attack a in _attacks)
                if (a is IAAttack ia && ia.range < min) min = ia.range;
            return min == Mathf.Infinity ? 0f : min;
        }
    }

    void Awake() => _cooldownRemaining = new float[_attacks.Length];

    public bool TryAttack(int index)
    {
        if (index < 0 || index >= _attacks.Length || !_attacks[index] || _cooldownRemaining[index] > 0f)
            return false;

        _attacks[index].Execute(_attackPosition);
        _cooldownRemaining[index] = _attacks[index].cooldown;
        return true;
    }

    public void SetAttacks(Attack[] attacks)
    {
        _attacks = attacks;
        _cooldownRemaining = new float[_attacks.Length];
    }

    void Update()
    {
        for (int i = 0; i < _cooldownRemaining.Length; i++)
        {
            if (_cooldownRemaining[i] > 0f)
                _cooldownRemaining[i] = Mathf.Max(0f, _cooldownRemaining[i] - Time.deltaTime);
        }
    }

    public float GetCooldowRemaining(int index) => _cooldownRemaining[index];
}
