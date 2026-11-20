using UnityEngine;
using UnityEngine.InputSystem;
using static EnumState;

internal class OnChase : IAState
{
    float _repathTimer;

    public OnChase(StateMachine machine) : base(machine) { }

    public override void OnEnter()
    {
        _repathTimer = 0f; // recalcul immédiat
    }

    public override void OnStay()
    {
        if (!Sensor.CanSeeTarget)
        {
            machine.SwitchState(STATE.SEARCH);
            return;
        }

        if (Sensor.InAttackRange)
        {
            machine.SwitchState(STATE.ATTACK);
            return;
        }

        _repathTimer -= Time.deltaTime;
        if (_repathTimer <= 0f)
        {
            Brain.MoveTo(Sensor.Target.position);
            _repathTimer = Settings.repathInterval;
        }
    }
}