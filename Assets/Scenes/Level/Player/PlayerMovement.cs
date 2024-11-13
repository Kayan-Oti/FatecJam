using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Hey!
/// Tarodev here. I built this controller as there was a severe lack of quality & free 2D controllers out there.
/// I have a premium version on Patreon, which has every feature you'd expect from a polished controller. Link: https://www.patreon.com/tarodev
/// You can play and compete for best times here: https://tarodev.itch.io/extended-ultimate-2d-controller
/// If you hve any questions or would like to brag about your score, come to discord: https://discord.gg/tarodev
/// </summary>
[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerMovement : MonoBehaviour, IPlayerController
{
    [SerializeField] private ScriptableStats _stats;
    private Rigidbody2D _rb;
    private CapsuleCollider2D _col;
    private FrameInput _frameInput;
    private Vector2 _frameVelocity;
    private bool _cachedQueryStartInColliders;

    #region Interface

    public Vector2 FrameInput => _frameInput.Move;
    public event Action<bool, float> GroundedChanged;
    public event Action Jumped;
    public event Action<bool> Crouch;

    #endregion

    private float _time;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<CapsuleCollider2D>();

        _cachedQueryStartInColliders = Physics2D.queriesStartInColliders;
        CrouchSetup();
    }

    private void Update()
    {
        _time += Time.deltaTime;
        GatherInput();
    }

    private void GatherInput()
    {
        _frameInput = new FrameInput
        {
            JumpDown = PlayerInputManager.JUMP_DOWN,
            JumpHeld = PlayerInputManager.JUMP_HELD,
            Move = PlayerInputManager.MOVEMENT,
            Crouch = PlayerInputManager.CROUCH
        };

        if (_stats.SnapInput)
        {
            _frameInput.Move.x = Mathf.Abs(_frameInput.Move.x) < _stats.HorizontalDeadZoneThreshold ? 0 : Mathf.Sign(_frameInput.Move.x);
            _frameInput.Move.y = Mathf.Abs(_frameInput.Move.y) < _stats.VerticalDeadZoneThreshold ? 0 : Mathf.Sign(_frameInput.Move.y);
        }

        if (_frameInput.JumpDown)
        {
            _jumpToConsume = true;
            _timeJumpWasPressed = _time;
        }
    }

    private void FixedUpdate()
    {
        if(_time < 0.3f)
            return;

        CheckCollisions();

        HandleJump();
        HandleDirection();
        HandleGravity();
        HandleCrouch();
        
        ApplyMovement();
    }

    #region Collisions
    
    private float _frameLeftGrounded = float.MinValue;
    private bool _grounded = true;

    private void CheckCollisions()
    {
        Physics2D.queriesStartInColliders = false;

        // Ground and Ceiling
        bool groundHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.down, _stats.GrounderDistance, ~_stats.IgnoreLayers);
        bool ceilingHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.up, _stats.GrounderDistance, ~_stats.IgnoreLayers);

        // Hit a Ceiling
        if (ceilingHit) _frameVelocity.y = Mathf.Min(0, _frameVelocity.y);

        // Landed on the Ground
        if (!_grounded && groundHit)
        {
            _grounded = true;
            _coyoteUsable = true;
            _bufferedJumpUsable = true;
            _endedJumpEarly = false;
            GroundedChanged?.Invoke(true, Mathf.Abs(_frameVelocity.y));
        }
        // Left the Ground
        else if (_grounded && !groundHit)
        {
            _grounded = false;
            _frameLeftGrounded = _time;
            GroundedChanged?.Invoke(false, 0);
        }

        Physics2D.queriesStartInColliders = _cachedQueryStartInColliders;
    }

    #endregion

    #region Jumping

    private bool _jumpToConsume;
    private bool _bufferedJumpUsable;
    private bool _endedJumpEarly;
    private bool _coyoteUsable;
    private float _timeJumpWasPressed;

    private bool HasBufferedJump => _bufferedJumpUsable && _time < _timeJumpWasPressed + _stats.JumpBuffer;
    private bool CanUseCoyote => _coyoteUsable && !_grounded && _time < _frameLeftGrounded + _stats.CoyoteTime;

    private void HandleJump()
    {
        if (!_endedJumpEarly && !_grounded && !_frameInput.JumpHeld && _rb.velocity.y > 0) _endedJumpEarly = true;

        if (!_jumpToConsume && !HasBufferedJump) return;

        if (_grounded || CanUseCoyote) ExecuteJump();

        _jumpToConsume = false;
    }

    private void ExecuteJump()
    {
        _endedJumpEarly = false;
        _timeJumpWasPressed = 0;
        _bufferedJumpUsable = false;
        _coyoteUsable = false;
        _frameVelocity.y = _stats.JumpPower;
        Jumped?.Invoke();
    }

    #endregion

    #region Horizontal

    private void HandleDirection()
    {
        if (_frameInput.Move.x == 0)
        {
            var deceleration = _grounded ? _stats.GroundDeceleration : _stats.AirDeceleration;
            _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, 0, deceleration * Time.fixedDeltaTime);
        }
        else
        {
            _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, _frameInput.Move.x * _stats.MaxSpeed, _stats.Acceleration * Time.fixedDeltaTime);
        }
    }

    #endregion

    #region Gravity

    private void HandleGravity()
    {
        if (_grounded && _frameVelocity.y <= 0f)
        {
            _frameVelocity.y = _stats.GroundingForce;
        }
        else
        {
            var inAirGravity = _stats.FallAcceleration;
            if (_endedJumpEarly && _frameVelocity.y > 0) inAirGravity *= _stats.JumpEndEarlyGravityModifier;
            _frameVelocity.y = Mathf.MoveTowards(_frameVelocity.y, -_stats.MaxFallSpeed, inAirGravity * Time.fixedDeltaTime);
        }
    }

    #endregion

    #region Crouch
    [Header("Crounch")]
    [SerializeField] private Transform _ceilingCheck;
    [SerializeField] private LayerMask _whatIsCelling;
    [Range(0,1)]
    [SerializeField] private float _crouchSpeed = 0.2f;
    [SerializeField] private Vector2 _crouchColliderOffSet;
    [SerializeField] private Vector2 _crouchColliderSize;
    private const float _ceilingRadius = .2f;
    private bool _isCrouching = false;
    private bool _wasCrouching = false;
    private Vector2 _originalColliderSize;
    private Vector2 _originalColliderOffset;

    private void HandleCrouch(){
        //Input
        if(_frameInput.Crouch){
            if(_grounded)
                _isCrouching = true;
            else
                _isCrouching = false;
        }else{
            //If can Stand up
            if (_wasCrouching && Physics2D.OverlapCircle(_ceilingCheck.position, _ceilingRadius, _whatIsCelling))
                _isCrouching = true;
            else
                _isCrouching = false;
        }

        //Crouching
        if(_isCrouching){
            if (!_wasCrouching)
				SetOnCrunchEvent(true);
			
            _frameVelocity.x *= _crouchSpeed;
        }
        //No crouch + was Crouching
        else if (_wasCrouching){
			SetOnCrunchEvent(false);
        }
        
        Debug.DrawRay(_ceilingCheck.position, Vector2.up * _ceilingRadius, Color.red);
    }

    private void SetOnCrunchEvent(bool state){
        _wasCrouching = state;

		if (state)
        {
            // Ajusta o tamanho e o offset do collider ao agachar
            _col.size = _crouchColliderSize;
            _col.offset = _crouchColliderOffSet;
        }
        else
        {
            // Restaura o tamanho e o offset originais do collider
            _col.size = _originalColliderSize;
            _col.offset = _originalColliderOffset;
        }

        Crouch?.Invoke(state);
    }

    private void CrouchSetup(){
        _originalColliderSize = _col.size;
        _originalColliderOffset = _col.offset;
    }


    #endregion

    private void ApplyMovement() => _rb.velocity = _frameVelocity;

    #if UNITY_EDITOR
    private void OnValidate()
    {
        if (_stats == null) Debug.LogWarning("Please assign a ScriptableStats asset to the Player Controller's Stats slot", this);
    }
    #endif
}

public struct FrameInput
{
    public bool JumpDown;
    public bool JumpHeld;
    public Vector2 Move;
    public bool Crouch;
}

public interface IPlayerController
{
    public event Action<bool, float> GroundedChanged;
    public event Action Jumped;
    public event Action<bool> Crouch;
    public Vector2 FrameInput { get; }
}
