using UnityEngine;
using System.Collections;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class DamageValueTMPro : MonoBehaviour
{
    private RectTransform _rect;
    private TextMeshProUGUI _text;

    [SerializeField] private CombatChannel _combatChannel;

    [Header("Floating text parameters")]
    [SerializeField] private AnimationCurve _levitationHeightCurve;
    [SerializeField] private AnimationCurve _levitationSideCurve;
    [SerializeField] private AnimationCurve _alphaCurve;
    [SerializeField] private Color _maxDamageColor;
    [SerializeField] private Color _minDamageColor;
    [SerializeField] private float _levitationDuration = 0.4f;
    [SerializeField] private float _levitationHeight = 80f;
    [SerializeField] private float _sideOffset = 40f;

    private void Start()
    {
        _rect = GetComponent<RectTransform>();
        _text = GetComponent<TextMeshProUGUI>();

        StartLevitation();
    }

    public void StartLevitation()
    {
        StartCoroutine(Levitate());
    }

    private IEnumerator Levitate()
    {
        int direction = -1;

        float elapsed = 0f;
        Vector2 startPos = _rect.anchoredPosition;

        while (elapsed < _levitationDuration)
        {
            float t = elapsed / _levitationDuration;

            float yOffset = _levitationHeightCurve.Evaluate(t) * _levitationHeight;
            float xOffset = _levitationSideCurve.Evaluate(t) * _sideOffset * direction;
            float alpha = _alphaCurve.Evaluate(t);

            _rect.anchoredPosition = new Vector2(
                startPos.x + xOffset,
                startPos.y + yOffset
            );

            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}