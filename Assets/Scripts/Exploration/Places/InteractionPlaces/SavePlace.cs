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
        _beforeInteractionDialogue = new DialogueStruct[1];

        _beforeInteractionDialogue[0].SpeakerName = "Thinking";
        _beforeInteractionDialogue[0].DialogueLine = $"Finally, a safe place to rest...";
    }
    public override void SetupAfterInteractionDialogue()
    {
        _afterInteractionDialogue = new DialogueStruct[1];
        _afterInteractionDialogue[0].SpeakerName = "System";
        _afterInteractionDialogue[0].DialogueLine = $"Game saved.";
    }
}
