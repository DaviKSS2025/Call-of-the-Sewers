using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CharacterStatsUI : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private TextMeshProUGUI _healthTMPro;
    [SerializeField] private Slider _manaSlider;
    [SerializeField] private TextMeshProUGUI _manaTMPro;
    [SerializeField] private TextMeshProUGUI _characterName;
    private StatsController _stats;
    private int _maxMana;
    private int _maxHealth;
    public void Initialize(string characterName, StatsController stats, int maxHealth, int maxMana)
    {
        _characterName.text = characterName;
        _stats = stats;
        _maxHealth = maxHealth;
        _maxMana = maxMana;
    }
    public void OnHealthChanged()
    {
        _healthSlider.value = CalculatePercentageForSliders(_stats.CurrentHealth, _maxHealth);
        _healthTMPro.text = $"{_stats.CurrentHealth}";
    }
    public void OnManaChanged()
    {
        _manaSlider.value = CalculatePercentageForSliders(_stats.CurrentMana, _maxMana);
        _manaTMPro.text = $"{_stats.CurrentMana}";
    }
    private float CalculatePercentageForSliders(int value1, int value2)
    {
        return Mathf.Clamp01((float)value1 / value2);
    }
}
