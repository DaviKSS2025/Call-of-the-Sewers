using UnityEngine;

public class NPCCharacterStatusPannel : BaseCharacterStatusPannel
{
    [SerializeField] private NPCDatabase _database;
    [SerializeField] protected GameObject _statusPannel;

    public override void UpdateCharacterStatus()
    {
        if (DoesCharacterExist())
        {
            _statusPannel.SetActive(true);
            base.UpdateCharacterStatus();
        }
        else
        {
            _statusPannel.SetActive(false);
        }
    }
    public override void UpdateTexts()
    {
        _characterName.text = _database.GetNPCName(NPCDataController.Instance.RuntimeData[0].NPCInfo);
        _healthValue.text = NPCDataController.Instance.RuntimeData[0].CurrentHealth.ToString();
    }
    public override void UpdateImages()
    {
        _characterImage.sprite = _database.GetNPCStatusSprite(NPCDataController.Instance.RuntimeData[0].NPCInfo);
    }
    public override void UpdateSliders()
    {
        _healthSlider.value = (float)NPCDataController.Instance.RuntimeData[0].CurrentHealth / _database.GetNPCSurvivalStats(NPCDataController.Instance.RuntimeData[0].NPCInfo).MaxHealth;
    }
    private bool DoesCharacterExist()
    {
        return NPCDataController.Instance.RuntimeData != null && NPCDataController.Instance.RuntimeData.Count > 0;
    }
}
