using UnityEngine;

public class ColletableKey : Colletables
{
    [SerializeField] private DoorNames _doorName;

    private void Start()
    {
        if (InventoryDataController.Instance.GetKeyIDs().Contains(_doorName.DoorName))
        {
            Destroy(gameObject);
        }
    }
    public override void OnPlayerPickup()
    {
        if (_insideRange)
        {
            wasCollected = true;
            InventoryDataController.Instance.AddKey(_doorName.DoorName);

            DialogueStruct[] pickupKeyDialogue = new DialogueStruct[1];
            pickupKeyDialogue[0].DialogueLine = $"<color=red>Key of {_doorName.DoorName}</color> added to your inventory.";
            pickupKeyDialogue[0].SpeakerName = "System";

            _dialogueChannel.RaiseDialogueRequested(pickupKeyDialogue);
        }
    }
}
