using UnityEngine;

[CreateAssetMenu(fileName = "SurvivalStats", menuName = "Stats/SurvivalStats")]
public class SurvivalStats : ScriptableObject
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _maxMana;
    [SerializeField] private float _defense;

    public int MaxHealth
    {
        get => _maxHealth;
    }
    public int MaxMana
    {
        get => _maxMana;
    }
    public float Defense
    {
        get => _defense;
    }
}
