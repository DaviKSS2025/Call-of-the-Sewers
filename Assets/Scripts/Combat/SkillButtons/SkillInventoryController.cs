using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SkillInventoryController : MonoBehaviour
{
    [SerializeField] private GameObject _skillButtonPrefab;
    [SerializeField] private SkillDatabase _skillDatabase;
    [SerializeField] private CombatChannel _combatChannel;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private InputChannel _inputChannel;
    private List<SkillButton> _generatedButtons = new List<SkillButton>();
    private List<SkillType> _playerSkills;
    private Animator _animator;
    private static readonly int Appearing = Animator.StringToHash("Appearing");
    private static readonly int Disappearing = Animator.StringToHash("Disappearing");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        _combatChannel.SkillEnd += UpdateAvailableSkills;
        _combatChannel.SkillEnd += HideSkillInventory;
        _combatChannel.HideSkillUI += HideSkillInventory;
        _combatChannel.HideSkillUI += UnsubscribeEvents;
    }
    private void OnDisable()
    {
        _combatChannel.SkillEnd -= UpdateAvailableSkills;
        _combatChannel.SkillEnd -= HideSkillInventory;
        _combatChannel.HideSkillUI -= HideSkillInventory;
        _combatChannel.HideSkillUI -= UnsubscribeEvents;
    }
    private void Start()
    {
        _playerSkills = PlayerDataController.Instance.RuntimeData.SkillList;
        SpawnSkillButtons();
    }

    private void SpawnSkillButtons()
    {
        foreach (SkillType skill in _playerSkills)
        {
            SkillButton skillButton = Instantiate(_skillButtonPrefab, transform).GetComponent<SkillButton>();
            _generatedButtons.Add(skillButton);

            SkillData skillToPass = _skillDatabase.GetSkill(skill);
            skillButton.Initialize(skillToPass, _playerController);
        }
    }
    private void UpdateAvailableSkills()
    {
        foreach (SkillButton button in _generatedButtons)
        {
            if (button.ThisSkillData.ManaCost > _playerController.Stats.CurrentMana)
            {
                Button currentButton = button.GetComponent<Button>();
                currentButton.enabled = false;
                currentButton.interactable = false;
                button.enabled = false;
            }
        }
    }
    public void StartSelectingSkill()
    {
        _inputChannel.OnUICancel += StopSelectingSkill;
        _combatChannel.RaiseChoosingSkill();
        _animator.ResetTrigger(Disappearing);
        _animator.SetTrigger(Appearing);
        EventSystem.current.SetSelectedGameObject(_generatedButtons[0].gameObject);
        SetButtonsActive(true);
        UpdateAvailableSkills();
    }
    private void StopSelectingSkill()
    {
        HideSkillInventory();
        _combatChannel.RaiseCancelChoosingSkill();
    }
    private void UnsubscribeEvents()
    {
        _inputChannel.OnUICancel -= StopSelectingSkill;
    }
    private void HideSkillInventory()
    {
        UnsubscribeEvents();
        _animator.ResetTrigger(Appearing);
        _animator.SetTrigger(Disappearing);
        SetButtonsActive(false);
    }
    private void SetButtonsActive(bool state)
    {
        foreach (SkillButton button in _generatedButtons)
        {
            Button currentButton = button.GetComponent<Button>();
            currentButton.interactable = state;
            button.enabled = state;
        }
    }
}
