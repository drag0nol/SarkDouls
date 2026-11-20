using UnityEngine;
using UnityEngine.InputSystem;
using static EnumState;

internal class OnAttack : IAState
{
    float _attackDelay;
    float _repathTimer;

    public OnAttack(StateMachine machine) : base(machine) { }

    public override void OnEnter()
    {
        Brain.Stop();
        _attackDelay = 0f;
        _repathTimer = 0f;
    }

    public override void OnStay()
    {
        if (!Sensor.CanSeeTarget)
        {
            machine.SwitchState(STATE.SEARCH);
            return;
        }

        if (!Sensor.InAttackRange)
        {
            machine.SwitchState(STATE.CHASE);
            return;
        }

        Vector3 targetPos = Sensor.Target.position;
        Brain.FaceTarget(targetPos);
        Brain.AimAt(targetPos);

        if (_attackDelay > 0f)
        {
            _attackDelay -= Time.deltaTime;
            Brain.Stop();
            return;
        }

        int attack = Brain.FindUsableAttack(Sensor.DistanceToTarget);
        if (attack >= 0)
        {
            Brain.Stop();
            if (Brain.Attack(attack))
                _attackDelay = Settings.attackCooldown;
            return;
        }

        if (Sensor.DistanceToTarget > Brain.SmallestAttackRange)
        {
            _repathTimer -= Time.deltaTime;
            if (_repathTimer <= 0f)
            {
                Brain.MoveTo(targetPos);
                _repathTimer = Settings.repathInterval;
            }
        }
        else
        {
            Brain.Stop();
        }
    }

    public override void OnExit()
    {
        Brain.Stop();
    }
}