using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStatsUI : CharacterStatsUI
{
    [SerializeField] private Slider _manaSlider;
    [SerializeField] private TextMeshProUGUI _manaTMPro;
    private int _maxMana;
    public int MaxMana
    {
        set => _maxMana = value;
    }
    public void OnManaChanged()
    {
        _manaSlider.value = CalculatePercentageForSliders(_stats.CurrentMana, _maxMana);
        _manaTMPro.text = $"{_stats.CurrentMana}";
    }
}
