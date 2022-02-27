using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 649

public abstract class CharacterMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] protected float _speed = 5f;
    [SerializeField] protected float _acceleration = 10f;
    [SerializeField] protected float _turnSpeed = 10f;
    [SerializeField] protected bool _controlRotation = true;    // character turns towards movement direction
    
    [Header("Dash")] 
    [SerializeField] protected float _dashSpeed = 10f;
    [SerializeField] protected float _dashLenght = 1f;
    [SerializeField] protected float _dashCoolDown= 1f;

    [Header("Airborne")]
    [SerializeField] protected float _gravity = -20f;       // custom gravity value
    [SerializeField] protected float _jumpHeight = 2.25f;   // peak height of jump
    [SerializeField] protected float _airControl = 0.1f;    // percentage of acceleration applied while airborne
    [SerializeField] protected bool _airTurning = true;     // character can turn while airborne

    [Header("Grounding")]
    [SerializeField] protected float _groundCheckOffset = 0.1f;     // height inside character where grounding ray starts
    [SerializeField] protected float _groundCheckDistance = 0.4f;   // distance down from offset position
    [SerializeField] protected float _maxSlopeAngle = 40f;          // maximum climbable slope, character will slip on anything higher
    [SerializeField] protected float _groundedFudgeTime = 0.25f;    // leeway time for players to still jump after leaving the ground
    [SerializeField] protected LayerMask _groundMask = 1 << 0;      // mask for layers considered the ground

    // public read-only properties
    public float Speed => _speed;
    public bool IsFudgeGrounded => Time.timeSinceLevelLoad < _lastGroundedTime + _groundedFudgeTime;

    // public-read private-set properties
    public bool IsGrounded { get; protected set; } = true;
    public Vector3 GroundNormal { get; protected set; } = Vector3.up;
    public Vector3 MoveInput { get; protected set; }
    public bool HasMoveInput { get; protected set; }
    public Vector3 LocalMoveInput { get; protected set; }
    public float LookDirection { get; protected set; }
    public virtual Vector3 Velocity { get; protected set; }
    public Vector3 SurfaceVelocity { get; protected set; }
    public bool CanDash { get; set; }
    public bool IsDashing { get; set; }

    // public properties
    public bool CanMove { get; set; } = true;
    public float MoveSpeedMultiplier { get; set; } = 1f;
    public float ForcedMovement { get; set; } = 0f;
    public float TurnSpeedMultiplier { get; set; } = 1f;

    // protected properties
    protected Vector3 _groundCheckStart => transform.position + transform.up * _groundCheckOffset;

    // private fields
    protected float _lastGroundedTime;
    private float _dashCounter;
    private float _dashCoolCounter;

    // receives movement input and clamps it to prevent over-acceleration
    public virtual void SetMoveInput(Vector3 input)
    {
        HasMoveInput = input.magnitude > 0.1f;
        if (!HasMoveInput) input = Vector3.zero;
        MoveInput = Vector3.ClampMagnitude(input, 1f);
        // finds movement input as local direction rather than world
        LocalMoveInput = transform.InverseTransformDirection(MoveInput);
    }

    // sets character look direction, flattening y-value
    public void SetLookDirection( Vector3 MouseWorldPos)
    {
        Vector3 targetDirection = MouseWorldPos - transform.position;
        LookDirection = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
    }

    // attempts a jump, will fail if not grounded
    public void Jump()
    {
        if (CanMove && IsFudgeGrounded)
        {
            float jumpVelocity = Mathf.Sqrt(2f * -_gravity * _jumpHeight);
            Velocity = new Vector3(Velocity.x, jumpVelocity, Velocity.z);
        }
    }

    protected virtual void FixedUpdate()
    {
        // rotates character towards movement direction
        if (_controlRotation && (IsGrounded || _airTurning))
        {
           transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, LookDirection - 90)); 
        }
        
        if (CanDash) 
        {
            if (_dashCoolCounter <= 0 && _dashCounter <= 0)
            {
                _dashCounter = _dashLenght;
                IsDashing = true;
            }

            if (_dashCounter > 0)
            {
                _dashCounter -= Time.deltaTime;

                if (_dashCounter <= 0)
                {
                    IsDashing = false;
                    _dashCoolCounter = _dashCoolDown;
                }
            }

            if (_dashCoolCounter > 0)
            {
                _dashCoolCounter -= Time.deltaTime;

                if (_dashCoolCounter <= 0)
                {
                    CanDash = false;
                }
            }
        }
    }

    protected virtual void Update()
    {
        IsGrounded = CheckGrounded();
        if (!CanMove) SetMoveInput(Vector3.zero);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = IsGrounded ? Color.green : Color.red;
        Gizmos.DrawRay(_groundCheckStart, -transform.up * _groundCheckDistance);
    }

    protected Vector3 GetMovementAcceleration(Vector3 movement, bool flattenY = true)
    {
        // calculates desirection movement velocity
        Vector3 targetVelocity = movement * (_speed * MoveSpeedMultiplier);
        // adds velocity of surface under character, if character is stationary
        targetVelocity += SurfaceVelocity * (1f - Mathf.Abs(MoveInput.magnitude));
        // calculates acceleration required to reach desired velocity and applies air control if not grounded
        Vector3 velocityDiff = targetVelocity - Velocity;
        if(flattenY) velocityDiff.y = 0f;
        float control = IsGrounded ? 1f : _airControl;
        Vector3 acceleration = velocityDiff * (_acceleration * control);
        // zeros acceleration if airborne and not trying to move (allows for nice jumping arcs)
        if (!IsGrounded && !HasMoveInput) acceleration = Vector3.zero;
        // add gravity
        acceleration += GroundNormal * _gravity;
        return acceleration;
    }

    protected abstract bool CheckGrounded();

    // checks if character is grounded based on floor normal
    protected bool ResolveGrounded(bool hit, Vector3 normal)
    {
        // set default ground direction to up
        GroundNormal = Vector3.up;
        // if ground wasn't hit, character is not grounded
        if (!hit) return false;

        // test angle between character up and ground, angles above _maxSlopeAngle are invalid
        bool angleValid = Vector3.Angle(transform.up, normal) < _maxSlopeAngle;
        if (angleValid)
        {
            // record last time character was grounded and set correct floor normal direction
            _lastGroundedTime = Time.timeSinceLevelLoad;
            GroundNormal = normal;
            return true;
        }

        return false;
    }
}
