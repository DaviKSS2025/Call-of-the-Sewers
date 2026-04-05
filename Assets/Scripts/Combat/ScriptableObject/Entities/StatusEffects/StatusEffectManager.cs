using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class StatusEffectManager
{
    private List<StatusEffectInstance> _activeEffects = new();
    private bool _isStunned;
    private BaseEntityController _entity;

    public bool IsStunned
    {
        get => _isStunned;
    }

    public StatusEffectManager(BaseEntityController entity)
    {
        _entity = entity;
    }
    public void ApplyEffect(StatusEffectData data)
    {
        bool alreadyHasEffect = _activeEffects.Any(e => e.Data == data);

        if (alreadyHasEffect)
            return;

        var instance = data.CreateInstance(_entity);
        instance.OnApply();
        _activeEffects.Add(instance);

        ShowEffectOnStatusPannel(data);
    }
    public bool ExecuteStatusEffectsAndSkipTurnIfStunned()
    {
        if (_activeEffects.Count > 0)
        {
            _isStunned = false;
            for (int i = _activeEffects.Count - 1; i >= 0; i--)
            {
                StatusEffectInstance effect = _activeEffects[i];
                if (effect is StunEffectInstance || effect is DevouredEffectInstance)
                {
                    _isStunned = true;
                    if (effect.ReduceTurn())
                    {
                        RemoveIfEffectIsOver(i, effect);
                    }
                    else
                    {
                        UpdateStatusEffectDurationTMPro(effect.RemainingTurns.ToString(), i);
                    }
                    continue;
                }
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
        return _isStunned;
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
    private void ShowEffectOnStatusPannel(StatusEffectData data)
    {
        for (int i = 0; i < _entity.StatusEffectDurationTMPro.Length - 1; i++)
        {
            if (!_entity.StatusEffectSlotImages[i].enabled)
            {
                UpdateStatusEffectSlot(data.StatusSprite, i);
                UpdateStatusEffectDurationTMPro(data.Duration.ToString(),i);
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
