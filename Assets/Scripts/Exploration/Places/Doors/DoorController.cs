using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private DoorNames _doorName;
    [SerializeField] private DialogueChannel _dialogueChannel;
    [SerializeField] private GameStateChannel _gameStateChannel;
    [SerializeField] private InputChannel _inputChannel;
    [SerializeField] private Sprite _openedSprite;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;
    private CapsuleCollider2D _capsuleCollider;
    private bool isPlayerOnRange;
    private DialogueStruct[] _openDialogue;
    private DialogueStruct[] _hasNotKeyDialogue;
    [SerializeField] protected SFXEventChannel _sfxChannel;
    [SerializeField] protected SimpleSFXEvent _openSFX;
    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _openDialogue = new DialogueStruct[1];
        _openDialogue[0].DialogueLine = "I opened the door.";
        _openDialogue[0].SpeakerName = "Thinking";

        _hasNotKeyDialogue = new DialogueStruct[1];
        _hasNotKeyDialogue[0].DialogueLine = $"I need a <color=red>{_doorName.DoorName}</color> key to open this door.";
        _hasNotKeyDialogue[0].SpeakerName = "Thinking";
    }
    private void OnEnable()
    {
        _gameStateChannel.OnGameStateChange += ManageInputs;
    }
    private void OnDisable()
    {
        _gameStateChannel.OnGameStateChange -= ManageInputs;
        _inputChannel.OnInteract -= CheckIfPlayerHasKey;
    }
    private void Start()
    {
        if (MapDataController.Instance.RuntimeExplorationData.OpenedDoors.TryGetValue(_doorName.DoorName, out bool state))
        {
            if (state)
            {
                OpenDoor(false);
            }
        }
        else
        {
            MapDataController.Instance.RuntimeExplorationData.OpenedDoors.Add(_doorName.DoorName, false);
        }
    }
    private void ManageInputs(CurrentGameState gameState)
    {
        if (gameState == CurrentGameState.Gameplay)
        {
            _inputChannel.OnInteract -= CheckIfPlayerHasKey;
            _inputChannel.OnInteract += CheckIfPlayerHasKey;
        }
        else
        {
            _inputChannel.OnInteract -= CheckIfPlayerHasKey;
        }
    }
    private void CheckIfPlayerHasKey()
    {
        if (isPlayerOnRange)
        {
            if (InventoryDataController.Instance.GetKeyIDs().Contains(_doorName.DoorName))
            {
                OpenDoor(true);
                _dialogueChannel.RaiseDialogueRequested(_openDialogue);
            }
            else
            {
                _dialogueChannel.RaiseDialogueRequested(_hasNotKeyDialogue);
            }
        }
    }
    private void OpenDoor(bool openedByPlayer)
    {
        _sfxChannel.RaiseEvent(_openSFX);
        _spriteRenderer.sprite = _openedSprite;
        _boxCollider.enabled = false;
        _capsuleCollider.enabled = false;
        MapDataController.Instance.OpenDoor(_doorName.DoorName);
        if (openedByPlayer)
        {
            //play open door SFX
        }
        enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnRange = false;
        }
    }
}
