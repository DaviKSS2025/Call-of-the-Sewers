using TMPro;
using UnityEngine;
using static UnityEngine.LowLevelPhysics2D.PhysicsLayers;

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
    }
    private void OnDisable()
    {
        _combatChannel.ShowAttackText -= OnShowAttackText;
        _combatChannel.MonsterEncounter -= OnMonsterEncounter;
        _combatChannel.PlayerRunStarted -= OnPlayerStartedRun;
        _combatChannel.ShowPlayerRunResult -= OnShowPlayerRunResult;
        _combatChannel.IdleTurn -= OnIdleTurn;
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
}
