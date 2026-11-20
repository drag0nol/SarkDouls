using UnityEngine;

public class Dash : MonoBehaviour
{
    [Header("Dependance")]
    [SerializeField] Rigidbody _rb;
    [SerializeField] Transform _transform;
    [SerializeField] GameObject _dashFx;


    [Header("Configuration")]
    [SerializeField] int _dashForce;
    [SerializeField] ForceMode _forceType;


    public void TryDash()
    {

        _rb.AddForce(_transform.forward * _dashForce, _forceType);
        GameObject.Instantiate(_dashFx, _transform.position, transform.rotation);
    }
}
