using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ActionsTMPro : MonoBehaviour
{
    private TextMeshProUGUI _text;
    [SerializeField] private CombatChannel _combatChannel;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        _combatChannel.ShowAttackText += OnShowAttackText;
        _combatChannel.MonsterEncounter += OnMonsterEncounter;
        _combatChannel.PlayerRunStarted += OnPlayerStartedRun;
        _combatChannel.ShowPlayerRunResult += OnShowPlayerRunResult;
        _combatChannel.IdleTurn += OnIdleTurn;
        _combatChannel.OnEntityKilled += OnEntityKilled;
        _combatChannel.ShowSkipTurnOnStun += OnSkipTurnOnStun;
        _combatChannel.ShowGlobalStatusEffectUsed += OnShowGlobalStatusEffectUsed;
        _combatChannel.ShowSkillText += OnShowTargetAttackSkillText;
    }
    private void OnDisable()
    {
        _combatChannel.ShowAttackText -= OnShowAttackText;
        _combatChannel.MonsterEncounter -= OnMonsterEncounter;
        _combatChannel.PlayerRunStarted -= OnPlayerStartedRun;
        _combatChannel.ShowPlayerRunResult -= OnShowPlayerRunResult;
        _combatChannel.IdleTurn -= OnIdleTurn;
        _combatChannel.OnEntityKilled -= OnEntityKilled;
        _combatChannel.ShowSkipTurnOnStun -= OnSkipTurnOnStun;
        _combatChannel.ShowGlobalStatusEffectUsed -= OnShowGlobalStatusEffectUsed;
        _combatChannel.ShowSkillText -= OnShowTargetAttackSkillText;
    }
    private void OnShowAttackText(string attackName, string attackerName, string targetName)
    {
        _text.text = $"<color=red>{attackerName}</color> attacks <color=red>{targetName}</color> with <color=red>{attackName}</color>!";
    }
    private void OnMonsterEncounter(string monsterName)
    {
        _text.text = $"<color=red>{monsterName}</color> appears in front of you!";
    }
    private void OnPlayerStartedRun(string playerName)
    {
        _text.text = $"<color=red>{playerName}</color> tries to run from the monster...";
    }
    private void OnShowPlayerRunResult(string runText)
    {
        _text.text = runText;
    }
    private void OnIdleTurn(string enemyName)
    {
        _text.text = $"<color=red>{enemyName}</color> observes your fear...";
    }
    private void OnEntityKilled(string deadName)
    {
        _text.text = $"<color=red>{deadName}</color> dies in an agonizing way...";
    }
    private void OnSkipTurnOnStun(string entityName)
    {
        _text.text = $"<color=red>{entityName}</color> is stunned and can't move.";
    }
    private void OnShowGlobalStatusEffectUsed(string globalStatusUsage)
    {
        _text.text = globalStatusUsage;
    }
    private void OnShowTargetAttackSkillText(string skillName, string attackerName, string targetName)
    {
        _text.text = $"<color=red>{attackerName}</color> casts <color=red>{skillName}</color> against <color=red>{targetName}</color>!";
    }
}
