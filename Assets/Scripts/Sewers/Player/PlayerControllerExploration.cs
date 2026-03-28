using System;
using UnityEngine;

//Mandatory components to work
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class PlayerControllerExploration : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private IPlayerState currentState;
    private Animator _animator;
    private PlayerInputReader _playerInputReader;
    private PlayerAnimatorControllerExploration _animatorController;
    private PlayerStatsExploration _stats;
    private PlayerMovement _movement;
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private SurvivalStats _maxHealth;
    [SerializeField] private MovementSpeed _movementSpeed;

    public PlayerInputReader PlayerInputReader
    {
        get => _playerInputReader;
    }
    public PlayerAnimatorControllerExploration PlayerAnimatorControllerExplo
    {
        get => _animatorController;
    }
    public PlayerMovement PlayerMovementScript
    {
        get => _movement;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        //SOLID scripts incicialization
        _stats = new PlayerStatsExploration(_maxHealth.MaxHealth, _movementSpeed.WalkingSpeed);
        _playerInputReader = new PlayerInputReader();
        _playerInputReader.Initialize();
        _movement = new PlayerMovement(_rigidBody, _playerInputReader, _stats, _spriteRenderer);
        _animatorController = new PlayerAnimatorControllerExploration(_animator);
        _stats.Initialize();

        //State machine inicialization
        currentState = new PlayerMovingState(this);
        currentState.OnEnter();
    }


    void Update()
    {
        //State updates
        currentState.OnUpdate();
    }

    public void ChangeState(IPlayerState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }

        currentState = newState;
        currentState.OnEnter();
    }

    public void OnAnimationEvent(string eventName)
    {
        currentState.HandleAnimationEvent(eventName);
    }

    private void KillingPlayer()
    {
        //Enter death animation if health < 0.
        ChangeState(new PlayerDeadState(this));
        this.enabled = false;
    }
}
