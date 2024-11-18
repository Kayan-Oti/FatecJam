using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;


/// <summary>
/// VERY primitive animator example.
/// </summary>
public class PlayerAnimator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _anim;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private List<Transform> _flipTargets;

    [Header("Settings")] 

    [Header("Particles")] 
    [SerializeField] private ParticleSystem _jumpParticles;
    [SerializeField] private ParticleSystem _launchParticles;
    [SerializeField] private ParticleSystem _moveParticles;
    [SerializeField] private ParticleSystem _landParticles;

    private PlayerMovement _player;
    private ParticleSystem.MinMaxGradient _currentGradient;
    private bool _grounded = true;
    private EventInstance _playerFootSteps;

    private void Awake()
    {
        _player = GetComponentInParent<PlayerMovement>();
    }

    private void Start() {
        _playerFootSteps = AudioManager.Instance.CreateEventInstance(FMODEvents.Instance.FootSteps);
    }
    private void OnEnable()
    {
        _player.Jumped += OnJumped;
        _player.GroundedChanged += OnGroundedChanged;
        _player.Crouch += OnCrouched;
        

        _moveParticles.Play();
    }

    private void OnDisable()
    {
        _player.Jumped -= OnJumped;
        _player.GroundedChanged -= OnGroundedChanged;
        _player.Crouch -= OnCrouched;

        _moveParticles.Stop();
    }

    private void Update()
    {
        if (_player == null) return;

        //Particle Color
        DetectGroundColor();

        //Player Flip Direction
        HandleSpriteFlip();

        //Player Animation + Particle
        HandleMovement();

        HandleVerticalVelocity();
    }

    #region Update Methods

    private void DetectGroundColor()
    {
        var hit = Physics2D.Raycast(transform.position, Vector3.down, 2);

        if (!hit || hit.collider.isTrigger || !hit.transform.TryGetComponent(out SpriteRenderer r)) return;
        var color = r.color;
        _currentGradient = new ParticleSystem.MinMaxGradient(color * 0.9f, color * 1.2f);
        SetColor(_moveParticles);
    }

    private void HandleSpriteFlip()
    {
        if (_player.FrameInput.x != 0){
            //Compare if curent Flip is diferent from Input
            if(_sprite.flipX != _player.FrameInput.x < 0){
                _sprite.flipX = _player.FrameInput.x < 0;
                foreach(Transform target in _flipTargets)
                    target.localPosition *= new Vector2(-1, 1);
            }
        }
    }


    private void HandleMovement()
    {
        var inputStrength = Mathf.Abs(_player.FrameInput.x);

        //Animation
        _anim.SetBool(RunningKey, inputStrength != 0);

        if(_player.IsWalking){
            _anim.SetFloat(RunningSpeedKey, 0.5f);
        }else{
            _anim.SetFloat(RunningSpeedKey, 1f);
        }

        if(_player.IsForcedToCrouch){
            _anim.SetFloat(CrouchSpeedKey, 0.5f);
        }
        else if(inputStrength == 0){
            _anim.SetFloat(CrouchSpeedKey, 0f);
        }
        else{
            _anim.SetFloat(CrouchSpeedKey, 1f);
        }


        //Sound
        if(inputStrength != 0 && _grounded){
            PLAYBACK_STATE playbackState;
            _playerFootSteps.getPlaybackState(out playbackState);
            if(playbackState.Equals(PLAYBACK_STATE.STOPPED))
                _playerFootSteps.start();
        }
        else
            _playerFootSteps.stop(STOP_MODE.ALLOWFADEOUT);


        //Particle Scale base on Speed
        _moveParticles.transform.localScale = Vector3.MoveTowards(_moveParticles.transform.localScale, Vector3.one * inputStrength, 2 * Time.deltaTime);
    }

    private void HandleVerticalVelocity(){
        float velocityY = _grounded? 0 : _player.FrameVelocityY;

        _anim.SetFloat(VerticalVelocityYKey, velocityY);
    }

    #endregion

    #region Event Methods
    private void OnJumped()
    {
        _anim.SetTrigger(JumpKey);
        _anim.ResetTrigger(GroundedKey);

        //Particles
        if (_grounded) // Avoid coyote
        {
            SetColor(_jumpParticles);
            SetColor(_launchParticles);
            _jumpParticles.Play();
        }
    }

    private void OnGroundedChanged(bool grounded, float impact)
    {
        _grounded = grounded;
        
        if (grounded)
        {
            DetectGroundColor();
            SetColor(_landParticles);

            _anim.SetTrigger(GroundedKey);
            //Audio
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.OnGrounded);
            _moveParticles.Play();

            _landParticles.transform.localScale = Vector3.one * Mathf.InverseLerp(0, 40, impact);
            _landParticles.Play();
        }
        else
        {
            _moveParticles.Stop();
        }
    }

    private void OnCrouched(bool state){
        if(state){
            _anim.ResetTrigger(CrouchEndKey);
            _anim.SetTrigger(CrouchStartKey);
        }
        else 
            _anim.SetTrigger(CrouchEndKey);
    }

    #endregion

    #region Set Methods

    private void SetColor(ParticleSystem ps)
    {
        var main = ps.main;
        main.startColor = _currentGradient;
    }

    #endregion

    #region Animation Keys

    private static readonly int GroundedKey = Animator.StringToHash("Grounded");
    private static readonly int RunningSpeedKey = Animator.StringToHash("RunningSpeed");
    private static readonly int CrouchSpeedKey = Animator.StringToHash("CrouchSpeed");
    private static readonly int JumpKey = Animator.StringToHash("Jump");
    private static readonly int CrouchStartKey = Animator.StringToHash("CrouchStart");
    private static readonly int CrouchEndKey = Animator.StringToHash("CrouchEnd");
    private static readonly int RunningKey = Animator.StringToHash("Running");
    private static readonly int VerticalVelocityYKey = Animator.StringToHash("VerticalVelocityY");

    private void OnDestroy() {
        _playerFootSteps.stop(STOP_MODE.IMMEDIATE);
        _playerFootSteps.release();
    }

    #endregion
}