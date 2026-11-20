using Unity.Cinemachine;
using UnityEngine;

public class MultiplayerGroupTargetManager : MonoBehaviour
{
    [SerializeField] CinemachineTargetGroup _targetGroup;

    [SerializeField] float _defaultWeight;
    [SerializeField] float _defaultRadius;

    private void Awake()
    {
        if (_targetGroup != null)
        {
            _targetGroup = FindFirstObjectByType<CinemachineTargetGroup>();
        }
    }

    public void RegisterTarget(Transform target)
    {

        if (target == null || _targetGroup == null) return;

        _targetGroup.AddMember(target, _defaultWeight, _defaultRadius);
    }

    public void UnRegisterTarget(Transform target)
    {
        if (target == null || _targetGroup == null) return;

        _targetGroup.RemoveMember(target);
    }
}
