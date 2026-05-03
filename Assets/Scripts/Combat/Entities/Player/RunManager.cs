using UnityEngine;

public class RunManager
{
    private float _runChance;
    private SceneChangeChannel _sceneChangeChannel;
    private CombatChannel _combatChannel;
    private string _playerName;
    private StatsController _stats;
    private int _runManaCost = 5;
    private bool _wasRunSuccesfull;
    public bool WasRunSuccesfull
    {
        get => _wasRunSuccesfull;
    }
    public RunManager(StatsController stats, float runChance, SceneChangeChannel sceneChangeChannel, CombatChannel combatChannel, string playerName) 
    { 
        _stats = stats;
        _runChance = runChance;
        _sceneChangeChannel = sceneChangeChannel;
        _combatChannel = combatChannel;
        _playerName = playerName;
    }

    public void RunStarted()
    {
        _combatChannel.RaisePlayerRunStarted(_playerName);
    }
    public bool HasManaToRun()
    {
        return _stats.CurrentMana >= _runManaCost;
    }
    public void RollRunChance()
    {
        float manaFactor = _stats.CurrentMana / 100f;
        float finalChance = _runChance * manaFactor;

        _wasRunSuccesfull = Random.Range(0f, 1f) <= finalChance;

        _combatChannel.RaisePlayerRunResult(_wasRunSuccesfull, _playerName);
    }
    public void ExecuteRun()
    {
        _stats.UseMana(_runManaCost);
        _sceneChangeChannel.RaiseSceneChanged();
    }
}
