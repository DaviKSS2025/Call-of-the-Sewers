using TMPro;
using UnityEngine;
using UnityEngine.UI;
public abstract class CharacterStatsUI : MonoBehaviour
{
    [SerializeField] protected Slider _healthSlider;
    [SerializeField] protected TextMeshProUGUI _healthTMPro;
    [SerializeField] protected TextMeshProUGUI _characterName;
    protected StatsController _stats;
    protected int _maxHealth;
    public void Initialize(string characterName, StatsController stats, int maxHealth)
    {
        _characterName.text = characterName;
        _stats = stats;
        _maxHealth = maxHealth;
    }
    public void OnHealthChanged()
    {
        _healthSlider.value = CalculatePercentageForSliders(_stats.CurrentHealth, _maxHealth);
        _healthTMPro.text = $"{_stats.CurrentHealth}";
    }
    public float CalculatePercentageForSliders(int value1, int value2)
    {
        return Mathf.Clamp01((float)value1 / value2);
    }
}
