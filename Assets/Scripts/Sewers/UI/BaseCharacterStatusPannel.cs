using TMPro;
using UnityEngine;
using UnityEngine.UI;
public abstract class BaseCharacterStatusPannel : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI _characterName;
    [SerializeField] protected Image _characterImage;
    [SerializeField] protected GameObject _statusEffectContainers;
    [SerializeField] protected Image[] _statusEffectSlots;
    [SerializeField] protected Slider _healthSlider;
    [SerializeField] protected Image _weaponImage;
    [SerializeField] protected TextMeshProUGUI _weaponName;
    [SerializeField] protected TextMeshProUGUI _weaponAttackPower;
    [SerializeField] protected Image _armorImage;
    [SerializeField] protected TextMeshProUGUI _armorName;
    [SerializeField] protected TextMeshProUGUI _armorDefensePower;

    public virtual void Start()
    {
        UpdateCharacterStatus();
    }
    public virtual void UpdateCharacterStatus()
    {
        UpdateTexts();
        UpdateImages();
        UpdateSliders();
    }
    public virtual void UpdateTexts()
    {
        
    }
    public virtual void UpdateImages()
    {

    }
    public virtual void UpdateSliders()
    {

    }
}
