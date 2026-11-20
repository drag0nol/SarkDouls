using UnityEngine.InputSystem;
using static EnumState;

internal class OnWander : IAState
{
    public OnWander(StateMachine statemachine) : base(statemachine)
    {
    }
    public override void OnEnter()
    {
        if (!Brain.RandomPoint(machine.SpawnPosition, Settings.wanderRadius, out var point) ||
            !Brain.MoveTo(point))
        {
            machine.SwitchState(STATE.IDLE);
        }
    }

    public override void OnStay()
    {
        if (Sensor.CanSeeTarget)
        {
            machine.SwitchState(STATE.CHASE);
            return;
        }

        if (!Brain.IsMoving) // arrivé à destination
            machine.SwitchState(STATE.IDLE);
    }
}