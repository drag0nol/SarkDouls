using UnityEngine;

public abstract class IAState
{
    protected readonly StateMachine machine;

    protected EnnemyBrain Brain => machine.Brain;
    protected EnemySensor Sensor => machine.Sensor;
    protected AISettings Settings => machine.Settings;

    protected IAState(StateMachine machine)
    {
        this.machine = machine;
    }

    public virtual void OnEnter() { }
    public virtual void OnStay() { }
    public virtual void OnExit() { }
}
