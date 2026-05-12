using UnityEngine;
using System;
[CreateAssetMenu(fileName = "DialogueChannel", menuName = "Channels/DialogueChannel")]
public class DialogueChannel : ScriptableObject
{
    public Action<DialogueStruct[]> DialogueRequested;
    public Action OnDialogueStart;
    public Action OnDialogueEnd;
    public void RaiseDialogueRequested(DialogueStruct[] dialogues)
    {
        DialogueRequested?.Invoke(dialogues);
        OnDialogueStart?.Invoke();
    }
    public void RaiseDialogueEnd()
    {
        OnDialogueEnd?.Invoke();
    }
}
[Serializable]
public struct DialogueStruct
{
    private string _speakerName;
    private string _dialogueLine;

    public string SpeakerName
    {
        get => _speakerName;
        set => _speakerName = value;
    }
    public string DialogueLine
    {
        get => _dialogueLine;
        set => _dialogueLine = value;
    }
}