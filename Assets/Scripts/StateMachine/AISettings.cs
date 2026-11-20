using UnityEngine;

[System.Serializable]
public class AISettings
{
    [Header("Idle")]
    public float idleMinTime = 1f;
    public float idleMaxTime = 3f;

    [Header("Wander")]
    public float wanderRadius = 8f;

    [Header("Chase")]
    [Tooltip("Intervalle de recalcul du chemin vers le joueur")]
    public float repathInterval = 0.25f;

    [Header("Attack")]
    public float attackCooldown = 1.2f;

    [Header("Search")]
    [Tooltip("Temps passé à chercher une fois arrivé à la dernière position connue")]
    public float searchDuration = 3f;
}
