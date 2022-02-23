﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.InputSystem;
#pragma warning disable 649

// sends input from PlayerInput to attached CharacterMovement components
public class PlayerController : MonoBehaviour
{
    // initial cursor state
    [SerializeField] private CursorLockMode _cursorMode = CursorLockMode.Locked;
    // make character look in Camera direction instead of MoveDirection
    [SerializeField] private bool _lookInCameraDirection;

    private CharacterMovement _characterMovement;
    private Vector2 _moveInput;

    private void Awake()
    {
        _characterMovement = GetComponent<CharacterMovement>();
        Cursor.lockState = _cursorMode;
    }

    public void OnMove(InputValue value)
    {
       _moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        _characterMovement?.Jump();
    }

    public void OnFire(InputValue value)
    {
        // placeholder for shooting stuff
    }

    public void OnLook(InputValue value)
    {
        Vector2 mouseScreenPos = value.Get<Vector2>();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        
        _characterMovement.SetLookDirection(mouseWorldPos);
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
    }
}