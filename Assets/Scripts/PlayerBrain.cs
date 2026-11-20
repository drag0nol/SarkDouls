using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBrain : MonoBehaviour
{
    enum AttackEnum{
        First,
        Second,
        Third,
        Ultimate
    }

    [Header("Dependance")]
    [SerializeField] Movement _moveSystem;
    [SerializeField] Jump _jumpSystem;
    [SerializeField] Dash _dashSystem;
    [SerializeField] AttackController _attackController;
    [SerializeField] AimSystem _targetSystem;
    [SerializeField] Camera cam;
    [SerializeField] LivingEntity lifeSystem;
    [SerializeField] Animator _animator;

    [SerializeField] LivingEntity _lifesystem;


    [Header("Configuration")]
    [SerializeField] PlayerInput _playerInput;
    [SerializeField] InputActionReference _moveInput;
    [SerializeField] InputActionReference _jumpInput;
    [SerializeField] InputActionReference _baseAttackInput;
    [SerializeField] InputActionReference _distanceAttackInput;
    [SerializeField] InputActionReference _areaAttackInput;
    [SerializeField] InputActionReference _ultimateAttackInput;
    [SerializeField] InputActionReference _aimInput;
    [SerializeField] InputActionReference _dashInput;
    [SerializeField] InputActionReference _interactInput;

    [SerializeField] Transform playerPos;
    [SerializeField] LayerMask _groundLayer;
    public GameObject objectFocus = null;



    void Awake()
    {
        cam = Camera.main;
    }

    void Start()
    {
        if (_moveInput)
        {
            _playerInput.actions.FindAction(_moveInput.name).started += StartMove;
            _playerInput.actions.FindAction(_moveInput.name).performed += MoveUpdate;
            _playerInput.actions.FindAction(_moveInput.name).canceled += MoveStop;
        }
        if (_jumpInput)
            _playerInput.actions.FindAction(_jumpInput.name).started += Jump;
        if (_baseAttackInput)
            _playerInput.actions.FindAction(_baseAttackInput.name).started += FirstAttack;
        if (_distanceAttackInput)
            _playerInput.actions.FindAction(_distanceAttackInput.name).started += SecondAttack;
        if (_areaAttackInput)
            _playerInput.actions.FindAction(_areaAttackInput.name).started += ThirdAttack;
        if (_ultimateAttackInput)
            _playerInput.actions.FindAction(_ultimateAttackInput.name).started += UltimateAttack;
        if (_aimInput && _playerInput.currentControlScheme == "Keyboard")
            _playerInput.actions.FindAction(_aimInput.name).performed += AimKeyBoard;
        else if (_aimInput)
            _playerInput.actions.FindAction(_aimInput.name).performed += AimJoyStick;
        if (_dashInput)
            _playerInput.actions.FindAction(_dashInput.name).started += Dash;
        if (_interactInput)
            _playerInput.actions.FindAction(_interactInput.name).started += Interact;
    }

    

    void OnDestroy()
    {
        if (_moveInput)
        {
            _playerInput.actions.FindAction(_moveInput.name).started -= StartMove;
            _playerInput.actions.FindAction(_moveInput.name).performed -= MoveUpdate;
            _playerInput.actions.FindAction(_moveInput.name).canceled -= MoveStop;
        }
        if (_jumpInput)
            _playerInput.actions.FindAction(_jumpInput.name).started -= Jump;
        if (_baseAttackInput)
            _playerInput.actions.FindAction(_baseAttackInput.name).started -= FirstAttack;
        if (_distanceAttackInput)
            _playerInput.actions.FindAction(_distanceAttackInput.name).started -= SecondAttack;
        if (_areaAttackInput)
            _playerInput.actions.FindAction(_areaAttackInput.name).started -= ThirdAttack;
        if (_ultimateAttackInput)
            _playerInput.actions.FindAction(_ultimateAttackInput.name).started -= UltimateAttack;
        if (_aimInput && _playerInput.currentControlScheme == "Keyboard")
            _playerInput.actions.FindAction(_aimInput.name).performed -= AimKeyBoard;
        else if (_aimInput)
            _playerInput.actions.FindAction(_aimInput.name).performed -= AimJoyStick;
        if (_dashInput)
            _playerInput.actions.FindAction(_dashInput.name).started -= Dash;
        if (_interactInput)
            _playerInput.actions.FindAction(_interactInput.name).started -= Interact;
    }

    void MoveStop(InputAction.CallbackContext obj)
    {
        _moveSystem.SetDirection(Vector3.zero);
        _animator.SetFloat("speed", 0);
    }

    void StartMove(InputAction.CallbackContext obj)
    {
        var joystickDirection = obj.ReadValue<Vector2>();
        var dir = Quaternion.Euler(0, -45, 0) * new Vector3(joystickDirection.x, 0, joystickDirection.y);
        _moveSystem.SetDirection(dir);
        _animator.SetFloat("speed", 1);
    }

    void MoveUpdate(InputAction.CallbackContext obj)
    {
        var joystickDirection = obj.ReadValue<Vector2>();
        var dir = Quaternion.Euler(0, -45, 0) * new Vector3(joystickDirection.x, 0, joystickDirection.y);
        _moveSystem.SetDirection(dir);
        _animator.SetFloat("speed", 1);
    }

    void Jump(InputAction.CallbackContext obj)
    {
        _jumpSystem.TryJump();
        _animator.SetTrigger("isJumping");
    }

    void FirstAttack(InputAction.CallbackContext obj) { 
        
        _attackController.TryAttack((int)AttackEnum.First);
        _animator.SetTrigger("isAttacking");
    
    }

    void SecondAttack(InputAction.CallbackContext obj) {
        _attackController.TryAttack((int)AttackEnum.Second);
        _animator.SetTrigger("isHeavyAttacking");
    }
    void ThirdAttack(InputAction.CallbackContext obj) => _attackController.TryAttack((int)AttackEnum.Third);
    void UltimateAttack(InputAction.CallbackContext obj) => _attackController.TryAttack((int)AttackEnum.Ultimate);

    void AimKeyBoard(InputAction.CallbackContext obj)
    {
        Vector3 direction = Vector3.zero;
        Vector2 input = obj.action.ReadValue<Vector2>();
        Ray ray = cam.ScreenPointToRay(input);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, _groundLayer))
        {
            Vector3 dir = hit.point - playerPos.position;
            dir.y = 0;
            if (dir.sqrMagnitude > 0.001f) direction = dir.normalized;
        }
        if (direction != Vector3.zero)
            _targetSystem.SetDirection(direction);
    }

    void AimJoyStick(InputAction.CallbackContext obj)
    {
        Vector2 input = obj.action.ReadValue<Vector2>();
        Vector3 camForward = Vector3.ProjectOnPlane(cam.transform.forward, Vector3.up).normalized;
        Vector3 camRight = Vector3.ProjectOnPlane(cam.transform.right, Vector3.up).normalized;
        Vector3 direction = (camRight * input.x + camForward * input.y).normalized;
        _targetSystem.SetDirection(direction);
    }

    void Dash(InputAction.CallbackContext obj)
    {
        _dashSystem.TryDash();
        _animator.SetTrigger("isDashing");
    }

    //private void FixedUpdate()
    //{
    //    _lifesystem.TakeDamage(1);
    //    Debug.Log("AAAAIIIIE");
    //}

    private void Interact(InputAction.CallbackContext context)
    {
        if (objectFocus != null && objectFocus.TryGetComponent(out Heal heal))
        {
            heal.Use(lifeSystem);
        }
    }
}