using UnityEngine;
public class SavePlace : InteractionPlace
{
    public override void ShowTextAfterInteraction()
    {
        base.ShowTextAfterInteraction();
        SaveManager.Instance.ManualSave();
    }
    public override void SetupBeforeInteractionDialogue()
    {
        _beforeInteractionDialogue = new DialogueStruct[5];

        _beforeInteractionDialogue[0].SpeakerName = "System";
        _beforeInteractionDialogue[0].DialogueLine = $"You find your portable radio lying on the ground in the darkness.";
        _beforeInteractionDialogue[1].SpeakerName = "System";
        _beforeInteractionDialogue[1].DialogueLine = $"You desperately try to call for backup.";
        _beforeInteractionDialogue[2].SpeakerName = "System";
        _beforeInteractionDialogue[2].DialogueLine = $"...";
        _beforeInteractionDialogue[3].SpeakerName = "System";
        _beforeInteractionDialogue[3].DialogueLine = $"Nobody responds.";
        _beforeInteractionDialogue[4].SpeakerName = "System";
        _beforeInteractionDialogue[4].DialogueLine = $"But you feel at least a little better for having tried.";
    }
    public override void SetupAfterInteractionDialogue()
    {
        _afterInteractionDialogue = new DialogueStruct[1];
        _afterInteractionDialogue[0].SpeakerName = "System";
        _afterInteractionDialogue[0].DialogueLine = $"Game saved.";
    }
}
