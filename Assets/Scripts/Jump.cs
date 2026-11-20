using NaughtyAttributes;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [Header("Dependance")]

    [SerializeField] GameObject _footPosition;
    [SerializeField] Rigidbody _rb;
    [SerializeField] Transform _transform;
    [SerializeField] GameObject _jumpFX;
    [SerializeField] LayerMask _groundLayer;



    [Header("Configuration")]
    [SerializeField] float _checkGroundDistance;
    [SerializeField] int _jumpForce;
    [SerializeField] ForceMode _forceType;

    [ShowNonSerializedField] bool _isGrounded;

    public void TryJump()
    {
        _isGrounded = Physics.Raycast(_footPosition.transform.position, Vector3.down, _checkGroundDistance, _groundLayer);
        if (_isGrounded)
        {
            _rb.AddForce(_transform.up * _jumpForce, _forceType);
            GameObject.Instantiate(_jumpFX, _transform.position, Quaternion.Euler(-90, 0, 0));

        }
    }
}
