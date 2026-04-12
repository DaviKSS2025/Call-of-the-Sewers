using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class StatusEffectManager
{
    private List<StatusEffectInstance> _activeEffects = new();
    private BaseEntityController _entity;

    public StatusEffectManager(BaseEntityController entity)
    {
        _entity = entity;
    }
    public void ApplyEffect(StatusEffectData data, int statusChance, int statusDuration)
    {
        bool alreadyHasEffect = _activeEffects.Any(e => e.Data == data);

        if (alreadyHasEffect)
            return;
        if (!RollStatusEffectChance(statusChance))
            return;
        var instance = data.CreateInstance(_entity, statusDuration);
        instance.OnApply();
        _activeEffects.Add(instance);

        ShowEffectOnStatusPannel(data.StatusSprite, statusDuration);
    }
    private bool RollStatusEffectChance(int statusChance)
    {
        return statusChance >= Random.Range(1, 101);
    }
    public bool ExecuteStatusEffectsAndSkipTurnIfStunned()
    {
        bool isStunned = _activeEffects.Any(e => e is StunEffectInstance || e is DevouredEffectInstance);
        if (_activeEffects.Count > 0)
        {
            for (int i = _activeEffects.Count - 1; i >= 0; i--)
            {
                StatusEffectInstance effect = _activeEffects[i];
                if (effect.Tick())
                {
                    RemoveIfEffectIsOver(i, effect);
                }
                else
                {
                    UpdateStatusEffectDurationTMPro(effect.RemainingTurns.ToString(), i);
                }
            }
        }
        return isStunned;
    }
    private void RemoveIfEffectIsOver(int index, StatusEffectInstance effect)
    {
        effect.OnEnd();
        _activeEffects.RemoveAt(index);
        DisableSlot(index);
    }
    private void DisableSlot(int index)
    {
        _entity.StatusEffectSlotImages[index].enabled = false;
        _entity.StatusEffectDurationTMPro[index].enabled = false;
    }
    private void ShowEffectOnStatusPannel(Sprite statusSprite, int duration)
    {
        for (int i = 0; i < _entity.StatusEffectDurationTMPro.Length - 1; i++)
        {
            if (!_entity.StatusEffectSlotImages[i].enabled)
            {
                UpdateStatusEffectSlot(statusSprite, i);
                UpdateStatusEffectDurationTMPro(duration.ToString(),i);
                break;
            }
        }
    }
    private void UpdateStatusEffectSlot(Sprite statusSprite, int index)
    {
        _entity.StatusEffectSlotImages[index].enabled = true;
        _entity.StatusEffectSlotImages[index].sprite = statusSprite;
    }
    private void UpdateStatusEffectDurationTMPro(string remainingTurns, int index)
    {
        _entity.StatusEffectDurationTMPro[index].enabled = true;
        _entity.StatusEffectDurationTMPro[index].text = remainingTurns;
    }
}
