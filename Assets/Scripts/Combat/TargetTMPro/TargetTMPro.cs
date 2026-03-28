using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TargetTMPro : MonoBehaviour
{
    private TextMeshProUGUI _text;
    [SerializeField] private SelectionChannel _selectionChannel;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _selectionChannel.NewTargetSelected += OnNewTargetSelected;
    }
    private void OnDisable()
    {
        _selectionChannel.NewTargetSelected -= OnNewTargetSelected;
    }
    private void OnNewTargetSelected(BaseEntityController targetName)
    {
        _text.text = $"Target: {targetName.EntityNameString}";
    }
}
