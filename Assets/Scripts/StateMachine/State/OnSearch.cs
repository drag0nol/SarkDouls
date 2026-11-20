using UnityEngine;
using UnityEngine.InputSystem;
using static EnumState;

internal class OnSearch : IAState
{
    float _timer;

    public OnSearch(StateMachine machine) : base(machine) { }

    public override void OnEnter()
    {
        _timer = Settings.searchDuration;

        if (Sensor.HasLastKnownPosition)
            Brain.MoveTo(Sensor.LastKnownPosition);
        else
            Brain.Stop();
    }

    public override void OnStay()
    {
        if (Sensor.CanSeeTarget)
        {
            machine.SwitchState(STATE.CHASE);
            return;
        }

        // On attend d'être arrivé à la dernière position connue avant de décompter
        if (Brain.IsMoving)
            return;

        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            Sensor.ForgetTarget();
            machine.SwitchState(STATE.WANDER);
        }
    }

    public override void OnExit()
    {
        Brain.Stop();
    }
}