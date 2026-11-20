using System;
using UnityEngine;

public class TargetGroupAutoRegister : MonoBehaviour
{

    [SerializeField] MultiplayerGroupTargetManager manager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager = FindFirstObjectByType<MultiplayerGroupTargetManager>();

        if (manager != null)
        {
            manager.RegisterTarget(transform);
        }
    }

    private void OnDestroy()
    {
        if (manager == null)
        {
            manager.UnRegisterTarget(transform);
        }
    }
}
