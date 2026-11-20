using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [Header("Dependance")]

    [SerializeField] Vector3 _direction;
    [SerializeField] Transform _transform;

    [Header("Configuration")]
    [SerializeField] int _speed;



    void Start()
    {
        
    }

    public void SetDirection(Vector3 dir)
    {
        _direction = dir;
    }




    void Update()
    {

        if (_direction != Vector3.zero)
        {
            _transform.rotation = Quaternion.LookRotation(_direction, _transform.up);
        }
        _transform.Translate(_direction * (_speed * Time.deltaTime), Space.World);
    }

}
