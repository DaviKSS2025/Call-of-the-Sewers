using UnityEngine;

public class ColletableKey : Colletables
{
    [SerializeField] private DoorNames _doorName;

    public override void OnPlayerPickup()
    {
        if (_insideRange)
        {
            base.OnPlayerPickup();
            wasCollected = true;
            InventoryDataController.Instance.AddKey(_doorName.DoorName);

            DialogueStruct[] pickupKeyDialogue = new DialogueStruct[1];
            pickupKeyDialogue[0].DialogueLine = $"<color=red>Key of {_doorName.DoorName}</color> added to your inventory.";
            pickupKeyDialogue[0].SpeakerName = "System";

            _dialogueChannel.RaiseDialogueRequested(pickupKeyDialogue);
        }
    }
    public override bool CheckIfWasAlreadyPicked()
    {
        return InventoryDataController.Instance.GetKeyIDs().Contains(_doorName.DoorName);
    }
}
