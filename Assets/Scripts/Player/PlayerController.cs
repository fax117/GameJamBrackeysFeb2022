using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
#pragma warning disable 649

// sends input from PlayerInput to attached CharacterMovement components
public class PlayerController : MonoBehaviour
{
    // initial cursor state
    [SerializeField] private CursorLockMode _cursorMode = CursorLockMode.Locked;
    // make character look in Camera direction instead of MoveDirection
    [SerializeField] private bool _lookInCameraDirection;

    private Animator _characterAnimator;
    private CharacterMovement _characterMovement;
    private PlayerHealth _playerHealth;
    private ShootingController _shootingController;
    private Vector2 _moveInput;
    private Vector3 _mouseWorldPos;
    private bool shootActive = false;

    private void Awake()
    {
        _characterAnimator = GetComponent<Animator>();
        _characterMovement = GetComponent<CharacterMovement>();
        _playerHealth = GetComponent<PlayerHealth>();
        _shootingController = GetComponent<ShootingController>();
        Cursor.lockState = _cursorMode;
    }

    public void OnMove(InputValue value)
    {
        if (_playerHealth.IsDead) return;

       _moveInput = value.Get<Vector2>();
    }

    public void OnDash(InputValue value)
    {
        _characterMovement.CanDash = value.isPressed;
    }

    public void OnFire(InputValue value)
    {
        if (_playerHealth.IsDead) return;
        // placeholder for shooting stuff
        _shootingController.canShoot = value.Get<float>() > 0.5f;

        // Animation triggers
        if (_shootingController.canShoot && _characterAnimator.GetBool("IsWerewolf"))
            _characterAnimator.SetBool("IsAttacking", true);
        else
            _characterAnimator.SetBool("IsAttacking", false);
    }

    public void OnLook(InputValue value)
    {
        if (_playerHealth.IsDead) return;
        Vector2 mouseScreenPos = value.Get<Vector2>();
        _mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        
        _characterMovement.SetLookDirection(_mouseWorldPos);
    }

    private void Update()
    {
        if (_characterMovement == null) return;

        // find correct right/forward directions based on main camera rotation
        Vector3 up = Vector3.up;
        Vector3 right = Camera.main.transform.right;
        Vector3 forward = Vector3.Cross(right, up);
        Vector3 moveInput = forward * _moveInput.y + right * _moveInput.x;
        
        _characterMovement.SetMoveInput(moveInput);

        if (_shootingController.canShoot) _shootingController.Shoot();
    }
}