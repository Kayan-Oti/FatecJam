using UnityEngine;


/// <summary>
/// VERY primitive animator example.
/// </summary>
public class PlayerAnimator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _anim;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Transform _spiritTarget;

    [Header("Settings")] 

    [Header("Particles")] 
    [SerializeField] private ParticleSystem _jumpParticles;
    [SerializeField] private ParticleSystem _launchParticles;
    [SerializeField] private ParticleSystem _moveParticles;
    [SerializeField] private ParticleSystem _landParticles;

    private IPlayerController _player;
    private ParticleSystem.MinMaxGradient _currentGradient;
    private bool _grounded;

    private void Awake()
    {
        _player = GetComponentInParent<IPlayerController>();
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
                _spiritTarget.localPosition *= new Vector2(-1, 1);
            }
        }
    }


    private void HandleMovement()
    {
        var inputStrength = Mathf.Abs(_player.FrameInput.x);

        _anim.SetBool(RunningKey, inputStrength != 0);

        //Particle Scale base on Speed
        _moveParticles.transform.localScale = Vector3.MoveTowards(_moveParticles.transform.localScale, Vector3.one * inputStrength, 2 * Time.deltaTime);
    }

    #endregion

    #region Event Methods
    private void OnJumped()
    {
        _anim.SetTrigger(JumpKey);
        _anim.ResetTrigger(GroundedKey);


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
    private static readonly int IdleSpeedKey = Animator.StringToHash("IdleSpeed");
    private static readonly int JumpKey = Animator.StringToHash("Jump");
    private static readonly int CrouchStartKey = Animator.StringToHash("CrouchStart");
    private static readonly int CrouchEndKey = Animator.StringToHash("CrouchEnd");
    private static readonly int RunningKey = Animator.StringToHash("Running");

    #endregion
}