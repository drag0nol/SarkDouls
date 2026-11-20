using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static EnumState;

public class StateMachine : MonoBehaviour
{
    [SerializeField] STATE _startState = STATE.IDLE;

    [Header("Dépendances")]
    [SerializeField] EnnemyBrain _brain;
    [SerializeField] EnemySensor _sensor;

    [Header("Réglages")]
    [SerializeField] AISettings _settings = new AISettings();

    public EnnemyBrain Brain => _brain;
    public EnemySensor Sensor => _sensor;
    public AISettings Settings => _settings;
    public Vector3 SpawnPosition { get; private set; }
    public STATE CurrentState { get; private set; }

    Dictionary<STATE, IAState> _states;
    IAState _current;

    void Awake()
    {
        if (!_brain) _brain = GetComponent<EnnemyBrain>();
        if (!_sensor) _sensor = GetComponent<EnemySensor>();
        SpawnPosition = transform.position;

        _states = new Dictionary<STATE, IAState>
        {
            { STATE.IDLE,   new OnIdle(this) },
            { STATE.WANDER, new OnWander(this) },
            { STATE.SEARCH, new OnSearch(this) },
            { STATE.CHASE,  new OnChase(this) },
            { STATE.ATTACK, new OnAttack(this) }
        };
    }

    void Start()
    {
        SwitchState(_startState);
    }

    void Update()
    {
        _current?.OnStay();
    }

    public void SwitchState(STATE newState)
    {
        if (!_states.TryGetValue(newState, out IAState next))
        {
            Debug.LogError($"État {newState} inconnu");
            return;
        }

        if (next == _current)
            return;

        _current?.OnExit();
        CurrentState = newState;
        _current = next;
        _current.OnEnter();
    }
}
