using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CandlePlace : MonoBehaviour
{
    [SerializeField] private GameStateChannel _gameStateChannel;
    [SerializeField] private DialogueChannel _dialogueChannel;
    [SerializeField] private InventoryChannel _inventoryChannel;
    [SerializeField] private InputChannel _inputChannel;
    [SerializeField] private ConsumableItemData _matchesData;
    private Light2D _light;
    private float _turnOnLightIntensity = 0.8f;
    private CircleCollider2D _lightRangeCollider;
    private CapsuleCollider2D _candleSwitchCollider;
    private bool isTurnOn;
    private bool isPlayerInRange;
    private void Awake()
    {
        _lightRangeCollider = GetComponent<CircleCollider2D>();
        _candleSwitchCollider = GetComponent<CapsuleCollider2D>();
        _light = GetComponent<Light2D>();
    }
    private void OnEnable()
    {
        _gameStateChannel.OnGameStateChange += ManageEvents;
        _inventoryChannel.MatchesUsed += TryLightCandle;
    }
    private void OnDisable()
    {
        _gameStateChannel.OnGameStateChange -= ManageEvents;
        _inputChannel.OnInteract -= TryLightCandle;
        _inventoryChannel.MatchesUsed -= TryLightCandle;
    }
    private void ManageEvents(CurrentGameState gameState)
    {
        if (gameState == CurrentGameState.Gameplay)
        {
            _inputChannel.OnInteract += TryLightCandle;
        }
        else
        {
            _inputChannel.OnInteract -= TryLightCandle;
        }
    }
    private void Start()
    {
        CheckIfLightIsTurnOn();
    }
    private void CheckIfLightIsTurnOn()
    {
        isTurnOn = Random.value < 0.5f;

        SetupLight();
    }
    private void SetupLight()
    {
        if (isTurnOn)
        {
            _candleSwitchCollider.enabled = false;
            _lightRangeCollider.enabled = true;
            _light.intensity = _turnOnLightIntensity;
        }
        else
        {
            _lightRangeCollider.enabled = false;
        }
    }
    private void TryLightCandle()
    {
        if (isPlayerInRange && !isTurnOn)
        {
            if (InventoryDataController.Instance.GetItemList().Contains(_matchesData))
            {
                InventoryDataController.Instance.OnItemUsed(_matchesData);
                isTurnOn = true;
                SetupLight();
                _inventoryChannel.EnteredLightArea(true);
            }
            else
            {
                DialogueStruct[] noMatchesDialogue = new DialogueStruct[1];
                noMatchesDialogue[0].DialogueLine = "I don't have matches to light this candle.";
                noMatchesDialogue[0].SpeakerName = "Thinking";

                _dialogueChannel.RaiseDialogueRequested(noMatchesDialogue);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (isTurnOn)
            {
                _inventoryChannel.RaiseEnteredLightArea(true);
            }
            else
            {
                _inventoryChannel.RaiseEnteredMatchesTriggerArea(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (isTurnOn)
            {
                _inventoryChannel.RaiseEnteredLightArea(false);
            }
            else
            {
                _inventoryChannel.RaiseEnteredMatchesTriggerArea(false);
            }
        }
    }
}
