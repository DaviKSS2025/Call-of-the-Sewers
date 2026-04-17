using System;
using Unity.VisualScripting;
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
    private PlayerAnimatorControllerExploration _animatorController;
    private PlayerStatsExploration _stats;
    private PlayerMovement _movement;
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private SurvivalStats _maxHealth;
    [SerializeField] private MovementSpeed _movementSpeed;
    [SerializeField] private MenuController _menuController;
    [SerializeField] private InputChannel _inputChannel;
    [SerializeField] private GameStateChannel _gameStateChannel;

    private Vector2 _currentMoveInput;
    public Vector2 CurrentMoveInput => _currentMoveInput;
    public InputChannel PlayerInputChannel
    {
        get => _inputChannel;
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
    private void OnEnable()
    {
        _gameStateChannel.OnGameStateChange += OnGameStateChanged;
    }

    private void OnDisable()
    {
        _gameStateChannel.OnGameStateChange -= OnGameStateChanged;
        DisableMovementInputs();
    }

    private void OnGameStateChanged(CurrentGameState gameState)
    {
        if (gameState == CurrentGameState.Gameplay)
        {
            EnableMovementInputs();
        }
        else
        {
            DisableMovementInputs();
            _movement.CantMove();
        }
    }

    private void EnableMovementInputs()
    {
        _inputChannel.OnMove -= OnMove;
        _inputChannel.OnMove += OnMove;
    }

    private void DisableMovementInputs()
    {
        _inputChannel.OnMove -= OnMove;
    }
    private void OnMove(Vector2 input)
    {
        _currentMoveInput = input;
    }
    private void Start()
    {
        //SOLID scripts incicialization
        _stats = new PlayerStatsExploration(_maxHealth.MaxHealth, _movementSpeed.WalkingSpeed);
        _movement = new PlayerMovement(_rigidBody, _stats, _spriteRenderer);
        _animatorController = new PlayerAnimatorControllerExploration(_animator);
        _stats.Initialize();

        //State machine inicialization
        currentState = new PlayerMovingState(this);
        currentState.OnEnter();

        transform.position = MapDataController.Instance.RuntimeData.WorldPosition;
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
}
