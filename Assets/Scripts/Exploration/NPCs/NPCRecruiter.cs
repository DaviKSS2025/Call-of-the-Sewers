using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class NPCRecruiter : MonoBehaviour
{
    [SerializeField] private NPCType _type;
    [SerializeField] private GameStateChannel _gameStateChannel;
    [SerializeField] private DialogueChannel _dialogueChannel;
    [SerializeField] private CutsceneChannel _cutsceneChannel;
    [SerializeField] private InputChannel _inputChannel;
    [SerializeField] private EntityName _npcName;
    private SpriteRenderer _spriteRenderer;
    private DialogueStruct[] _recruitDialogue;
    private DialogueStruct[] _confirmRecruitDialogue;
    private bool isInRange;
    private bool wasRecruited;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        _gameStateChannel.OnGameStateChange += ManageInputs;
        _dialogueChannel.OnDialogueEnd += OnDialogueEnd;
    }
    private void OnDisable()
    {
        _gameStateChannel.OnGameStateChange -= ManageInputs;
        _dialogueChannel.OnDialogueEnd -= OnDialogueEnd;
        _cutsceneChannel.OnBlackoutMiddle -= HideNPC;
        _cutsceneChannel.OnCutsceneEnd -= ConfirmRecruitment;
    }

    private void Start()
    {
        _recruitDialogue = new DialogueStruct[7];

        _recruitDialogue[0].DialogueLine = "Hello, stranger.";
        _recruitDialogue[0].SpeakerName = $"{_npcName.Name}";
        _recruitDialogue[1].DialogueLine = "Hi...? Who are you?";
        _recruitDialogue[1].SpeakerName = $"{SaveManager.Instance.Data.PlayerData.PlayerName}";
        _recruitDialogue[2].DialogueLine = $"My name is <color=red>{_npcName.Name}</color>.";
        _recruitDialogue[2].SpeakerName = $"{_npcName.Name}";
        _recruitDialogue[3].DialogueLine = "I fell into the sewer and lost my sense of direction.";
        _recruitDialogue[3].SpeakerName = $"{_npcName.Name}";
        _recruitDialogue[4].DialogueLine = "There seem to be some monsters and other horrifying creatures there.";
        _recruitDialogue[4].SpeakerName = $"{_npcName.Name}";
        _recruitDialogue[5].DialogueLine = "I used some spells to defend myself, but all this darkness is driving me crazy.";
        _recruitDialogue[5].SpeakerName = $"{_npcName.Name}";
        _recruitDialogue[6].DialogueLine = "Do you want to travel together? It would be safer if we joined forces.";
        _recruitDialogue[6].SpeakerName = $"{_npcName.Name}";
    }

    private void ManageInputs(CurrentGameState gameState)
    {
        if (gameState == CurrentGameState.Gameplay) 
        {
            _inputChannel.OnInteract += TryRecruitNPC;
        }
        else
        {
            _inputChannel.OnInteract -= TryRecruitNPC;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
        }
    }

    private void TryRecruitNPC()
    {
        if (isInRange)
        {
            wasRecruited = true;
            _dialogueChannel.RaiseDialogueRequested(_recruitDialogue);
        }
    }
    private void OnDialogueEnd()
    {
        if (wasRecruited)
        {
            _cutsceneChannel.RaiseBlackoutRequested();
            _cutsceneChannel.OnBlackoutMiddle += HideNPC;
            _cutsceneChannel.OnCutsceneEnd += ConfirmRecruitment;
        }
    }
    private void HideNPC()
    {
        _spriteRenderer.enabled = false;
    }
    private void ConfirmRecruitment()
    {
        NPCDataController.Instance.RecruitNPC(_type);

        _confirmRecruitDialogue = new DialogueStruct[1];

        _confirmRecruitDialogue[0].DialogueLine = $"<color=red>{_npcName.Name}</color> joined your party.";
        _confirmRecruitDialogue[0].SpeakerName = "System";

        _dialogueChannel.RaiseDialogueRequested(_confirmRecruitDialogue);
        gameObject.SetActive(false);
    }
}
