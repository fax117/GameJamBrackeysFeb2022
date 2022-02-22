using System.Collections;
using  System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : CharacterMovement
{
    [Header("Top Down")] 
    [SerializeField] private bool _topDownMovement = false;

    public override Vector3 Velocity { get => _rigidbody.velocity ; protected set => _rigidbody.velocity = value; }

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        _rigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;
        _rigidbody.gravityScale = 0f;

        LookDirection = Vector3.right;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        Vector3 input = MoveInput;
        if (ForcedMovement > 0f) input = Vector3.right * ForcedMovement;
        Vector3 forward = Vector3.right * input.x;
        if (_topDownMovement) forward = new Vector3(MoveInput.x, MoveInput.z, 0f);
        _rigidbody.AddForce(GetMovementAcceleration(forward, !_topDownMovement));
    }
    
    protected override bool CheckGrounded()
    {
        // ignore ground checks if top-down
        if (_topDownMovement) return true;
        // configure layer mask for 2D raycast
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(_groundMask);
        // raycast to find ground
        RaycastHit2D[] hits = new RaycastHit2D[1];
        Physics2D.Raycast(_groundCheckStart, -transform.up, filter, hits, _groundCheckDistance);
        // gets velocity of surface underneath character if applicable
        if (hits[0] && hits[0].rigidbody != null) SurfaceVelocity = hits[0].rigidbody.velocity;
        else SurfaceVelocity = Vector3.zero;
        // sends hit info for grounding resolution
        return ResolveGrounded(hits[0], hits[0].normal);
    }
}