using UnityEngine;

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
    private PlayerSanityDarknessDrain _sanityDarknessDrain;
    private PlayerMovement _movement;

    [SerializeField] private SurvivalStats _maxHealth;
    [SerializeField] private MovementSpeed _movementSpeed;
    [SerializeField] private MenuController _menuController;
    [SerializeField] private InputChannel _inputChannel;
    [SerializeField] private GameStateChannel _gameStateChannel;
    [SerializeField] private InventoryChannel _inventoryChannel;
    [SerializeField] private DialogueChannel _dialogueChannel;
    [SerializeField] private SimpleMusicEvent _explorationMusic;
    [SerializeField] private MusicEventChannel _musicChannel;

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
    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        _gameStateChannel.OnGameStateChange += OnGameStateChanged;
    }

    private void OnDisable()
    {
        _gameStateChannel.OnGameStateChange -= OnGameStateChanged;
        DisableMovementInputs();
        DisableDependencies();
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
            _currentMoveInput = Vector2.zero;
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
    private void DisableDependencies()
    {
        _sanityDarknessDrain.OnDisable();
    }
    private void OnMove(Vector2 input)
    {
        _currentMoveInput = input;
    }
    private void Start()
    {
        //SOLID scripts incicialization
        _stats = new PlayerStatsExploration(_maxHealth.MaxHealth, _movementSpeed.WalkingSpeed);
        _movement = new PlayerMovement(_rigidBody, _stats);
        _animatorController = new PlayerAnimatorControllerExploration(_animator);
        _stats.Initialize();
        _sanityDarknessDrain = new PlayerSanityDarknessDrain(_inventoryChannel, _gameStateChannel, _dialogueChannel);
        _sanityDarknessDrain.Initialize();

        //State machine inicialization
        currentState = new PlayerMovingState(this);
        currentState.OnEnter();

        transform.position = new Vector2(MapDataController.Instance.RuntimeExplorationData.WorldPosX, MapDataController.Instance.RuntimeExplorationData.WorldPosY);

        _musicChannel.RaiseEvent(_explorationMusic);
    }
    void Update()
    {
        currentState.OnUpdate();
        _sanityDarknessDrain.CheckDarkness();
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
