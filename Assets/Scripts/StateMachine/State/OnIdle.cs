using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static EnumState;

public class OnIdle : IAState
{
    float _timer;

    public OnIdle(StateMachine machine) : base(machine) { }

    public override void OnEnter()
    {
        Brain.Stop();
        _timer = Random.Range(Settings.idleMinTime, Settings.idleMaxTime);
    }

    public override void OnStay()
    {
        if (Sensor.CanSeeTarget)
        {
            machine.SwitchState(STATE.CHASE);
            return;
        }

        _timer -= Time.deltaTime;
        if (_timer <= 0f)
            machine.SwitchState(STATE.WANDER);
    }
}
