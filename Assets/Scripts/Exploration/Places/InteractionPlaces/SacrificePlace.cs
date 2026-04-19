using UnityEngine;
using System.Collections.Generic;
public class SacrificePlace : InteractionPlace
{
    [SerializeField] private Weapons _weaponToGain;
    [SerializeField] private NPCDatabase _npcDatabase;
    [SerializeField] private ChoiceChannel _choiceChannel;
    private List<ChoiceOption> _choiceList = new List<ChoiceOption>();
    private DialogueStruct[] _sacrificeResultDialogue;
    private DialogueStruct[] _alreadySacrificedDialogue;
    private bool hasSacrificed;
    public DialogueStruct[] SacrificeResultDialogue
    {
        get => _sacrificeResultDialogue;
        set => _sacrificeResultDialogue = value;
    }
    public Weapons WeaponToGain
    {
        get => _weaponToGain;
    }
    public bool HasSacrificed
    {
        set => hasSacrificed = value;
    }
    public NPCDatabase Database
    {
        get => _npcDatabase;
    }
    public DialogueChannel DialChannel
    {
        get => _dialogueChannel;
    }
    public override void OnEnable()
    {
        _gameStateChannel.OnGameStateChange += ToggleInputs;
    }
    public override void Start()
    {
        base.Start();
        SetupAfterSacrificedDialogue();

        hasSacrificed = MapDataController.Instance.RuntimeData.UsedSacrificePlace;
        _choiceList.Add(new ChoiceOption("Yes.", new AcceptSacrificeEffect(this)));
        _choiceList.Add(new ChoiceOption("No.", new NegateSacrificeEffect(this)));
    }
    public override void SetupBeforeInteractionDialogue()
    {
        _beforeInteractionDialogue = new DialogueStruct[3];

        if (NPCDataController.Instance.RuntimeData.Count > 0)
        {

            _beforeInteractionDialogue[0].SpeakerName = "Thinking";
            _beforeInteractionDialogue[0].DialogueLine = "This altar emanates a frightening aura...";
            _beforeInteractionDialogue[1].SpeakerName = "Thinking";
            _beforeInteractionDialogue[1].DialogueLine = "The runic inscriptions on the stone base read \"A soul in exchange for power.\"";
            _beforeInteractionDialogue[2].SpeakerName = "Thinking";
            _beforeInteractionDialogue[2].DialogueLine = $"Would I trade <color=red>{_npcDatabase.GetNPCName(NPCDataController.Instance.RuntimeData[0].NPCInfo)}</color>'s soul for immeasurable power?";
        }
        else
        {
            _beforeInteractionDialogue[0].SpeakerName = "Thinking";
            _beforeInteractionDialogue[0].DialogueLine = "This altar emanates a frightening aura...";
            _beforeInteractionDialogue[1].SpeakerName = "Thinking";
            _beforeInteractionDialogue[1].DialogueLine = "The runic inscriptions on the stone base read \"A soul in exchange for power.\"";
            _beforeInteractionDialogue[2].SpeakerName = "Thinking";
            _beforeInteractionDialogue[2].DialogueLine = "I'm not mad enough to sacrifice myself.";
        }
    }
    public override void SetupAfterInteractionDialogue()
    {
        _afterInteractionDialogue = new DialogueStruct[1];

        _afterInteractionDialogue[0].SpeakerName = "Thinking";
        _afterInteractionDialogue[0].DialogueLine = "I accepted the power, but the guilt in my heart increased...";
    }
    private void SetupAfterSacrificedDialogue()
    {
        _alreadySacrificedDialogue = new DialogueStruct[1];

        _alreadySacrificedDialogue[0].SpeakerName = "Thinking";
        _alreadySacrificedDialogue[0].DialogueLine = "I already obtened all the power I need...";
    }
    public override void CallInteraction()
    {
        if (isInRange)
        {
            if (!hasSacrificed)
            {
                SetupBeforeInteractionDialogue();
                _dialogueChannel.RaiseDialogueRequested(_beforeInteractionDialogue);
                isInteracted = true;
                if (NPCDataController.Instance.RuntimeData.Count > 0)
                {
                    _dialogueChannel.OnDialogueEnd += CallChoice;
                }
            }
            else
            {
                _dialogueChannel.RaiseDialogueRequested(_alreadySacrificedDialogue);
                isInteracted = true;
            }
        }
    }
    private void CallChoice()
    {
        _choiceChannel.RaiseChoiceRequested(_choiceList);
    }
    public override void ShowTextAfterInteraction()
    {
        base.ShowTextAfterInteraction();
        CancelChoice();
    }
    protected void CancelChoice()
    {
        _dialogueChannel.OnDialogueEnd -= CallChoice;
    }
    private class AcceptSacrificeEffect : IChoiceEffect
    {
        private SacrificePlace _sacrificePlace;
        public AcceptSacrificeEffect(SacrificePlace sacrificePlace)
        {
            _sacrificePlace = sacrificePlace;
        }
        public void Execute()
        {
            _sacrificePlace.SacrificeResultDialogue = new DialogueStruct[5];

            _sacrificePlace.SacrificeResultDialogue[0].DialogueLine = "Yes, I need power... No matter the cost.";
            _sacrificePlace.SacrificeResultDialogue[0].SpeakerName = "Thinking";
            _sacrificePlace.SacrificeResultDialogue[1].DialogueLine = "You throw your partner onto the altar, ignoring their protests.";
            _sacrificePlace.SacrificeResultDialogue[1].SpeakerName = "System";
            _sacrificePlace.SacrificeResultDialogue[2].DialogueLine = "The altar begins to glow, shining so brightly that it momentarily dispels the darkness of the dungeon.";
            _sacrificePlace.SacrificeResultDialogue[2].SpeakerName = "System";
            _sacrificePlace.SacrificeResultDialogue[3].DialogueLine = $"At the end of the process, at the cost of your partner's soul, you obtain a <color=red>{_sacrificePlace.WeaponToGain.Name}</color>.";
            _sacrificePlace.SacrificeResultDialogue[3].SpeakerName = "System";
            _sacrificePlace.SacrificeResultDialogue[4].DialogueLine = $"<color=red>{_sacrificePlace.Database.GetNPCName(NPCDataController.Instance.RuntimeData[0].NPCInfo)}</color>'s lifeless body lies motionless on the ground...";
            _sacrificePlace.SacrificeResultDialogue[4].SpeakerName = "System";

            PlayerDataController.Instance.UpgradeWeapon(_sacrificePlace.WeaponToGain.ThisWeaponType);
            NPCDataController.Instance.RemoveNPC(NPCDataController.Instance.RuntimeData[0].NPCInfo);
            MapDataController.Instance.UsedSacrificePlace();
            _sacrificePlace.AfterInteractionDialogue = _sacrificePlace.SacrificeResultDialogue;
            _sacrificePlace.HasSacrificed = true;
            _sacrificePlace.ShowTextAfterInteraction();
        }
    }
    private class NegateSacrificeEffect : IChoiceEffect
    {
        private SacrificePlace _sacrificePlace;
        public NegateSacrificeEffect(SacrificePlace sacrificePlace)
        {
            _sacrificePlace = sacrificePlace;
        }
        public void Execute()
        {
            _sacrificePlace.CancelChoice();
            _sacrificePlace.SacrificeResultDialogue = new DialogueStruct[1];

            _sacrificePlace.SacrificeResultDialogue[0].DialogueLine = "Think again. Although the power is tempting, perhaps there's another way out of this dungeon.";
            _sacrificePlace.SacrificeResultDialogue[0].SpeakerName = "Thinking";


            _sacrificePlace.AfterInteractionDialogue = _sacrificePlace.SacrificeResultDialogue;
            _sacrificePlace.ShowTextAfterInteraction();
        }
    }
}
