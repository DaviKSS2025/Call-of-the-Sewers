using System.Linq;
using UnityEngine;

public class DJonesStrategy : BaseStrategy, INPCStrategy
{
    private SkillData _darkHold;
    private SkillData _darkFire;
    private SkillData _darkHealing;
    private SkillData _darkness;
    private float _darkFireChance = 0.3f;
    private float _healingThreshold = 0.65f;
    private IEntityListHandler _entityListHandler;
    public DJonesStrategy(AnimatorStateController animatorController, CombatChannel combatChannel, SkillData darkHold, SkillData darkFire, SkillData darkHealing, SkillData darkness, IEntityListHandler entityListHandler) : base(animatorController, combatChannel) 
    { 
        _darkHold = darkHold;
        _darkFire = darkFire;
        _darkHealing = darkHealing;
        _darkness = darkness;
        _entityListHandler = entityListHandler;
    }

    public override void ChooseStrategy()
    {
        int currentAllyHealth = 0;
        int allyMaxHealth = 0;
        bool haveBlindEnemies = false;
        bool haveStunnedEnemies = false;

        foreach (BaseEntityController entity in _entityListHandler.TurnOrder)
        {
            if (entity.EntityType == TargetType.NPC || entity.EntityType == TargetType.Player)
            {
                currentAllyHealth += entity.Stats.CurrentHealth;
                allyMaxHealth += entity.SurvStats.MaxHealth;
            }
            else
            {
                if (entity.StatusManager.ActiveEffects.Any(e => e is BlindnessEffectInstance))
                {
                    haveBlindEnemies = true;
                }
                else if (entity.StatusManager.ActiveEffects.Any(f => f is StunEffectInstance))
                {
                    haveStunnedEnemies = true;
                }
            }
        }
        if (currentAllyHealth < allyMaxHealth * _healingThreshold)
        {
            Debug.Log("NPC resolveu curar");
            _combatChannel.RaiseSkillUsed(_darkHealing);
        }
        else if (!haveBlindEnemies)
        {
            Debug.Log("NPC resolveu usar escuridão");
            _combatChannel.RaiseSkillUsed(_darkness);
        }
        else if (!haveStunnedEnemies)
        {
            Debug.Log("NPC resolveu usar dark hold");
            _combatChannel.RaiseSkillUsed(_darkHold);
        }
        else
        {
            Debug.Log("NPC não entrou nos demais casos");
            bool useDarkFire = Random.value < _darkFireChance;
            if (useDarkFire)
            {
                _combatChannel.RaiseSkillUsed(_darkFire);
            }
            else
            {
                PrepareAttack();
            }
        }
    }
}