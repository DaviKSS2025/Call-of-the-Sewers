using TMPro;
using UnityEngine;

public class EndgameDialogues : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dialogueTMPro;
    [SerializeField] private SceneChangeChannel _sceneChangeChannel;
    [SerializeField] private SceneNames _menuScene;
    private string[] _endgameDialogue = new string[6];
    private int _dialogueIndex = 0;

    private void Start()
    {
        _endgameDialogue[0] = "With great difficulty, I managed to escape those sewers.";
        _endgameDialogue[1] = "I never want to come back here again.";
        _endgameDialogue[2] = "I've seen things... I've done things... Unforgettable.";
        _endgameDialogue[3] = "The depths of this place... penetrate my soul. It feels like ghosts haunt my thoughts.";
        _endgameDialogue[4] = "Will I ever be able to recover from everything that has happened?";
        _endgameDialogue[5] = "Maybe I'm just... losing my mind.";

        _dialogueTMPro.text = _endgameDialogue[0];
    }
    public void NextDialogue()
    {
        if (_dialogueIndex < _endgameDialogue.Length - 1)
        {
            _dialogueIndex++;
            _dialogueTMPro.text = _endgameDialogue[_dialogueIndex];
        }
        else
        {
            ReturnToMenu();
        }
    }
    private void ReturnToMenu()
    {
        _sceneChangeChannel.RaiseGoToTargetScene(_menuScene);
        gameObject.SetActive(false);
    }
}
