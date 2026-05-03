using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SmoothScrollToSelected : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private float scrollSpeed = 15f;

    private float _targetNormalizedPos = 1f;
    private bool _isScrolling;

    void OnEnable() => _targetNormalizedPos = scrollRect.verticalNormalizedPosition;

    void Update()
    {
        // Só monitora a troca de seleção se necessário
        var current = EventSystem.current.currentSelectedGameObject;
        if (current != null && current.transform.parent == scrollRect.content)
        {
            UpdateTargetPosition(current.GetComponent<RectTransform>());
        }

        if (_isScrolling)
        {
            scrollRect.verticalNormalizedPosition = Mathf.Lerp(
                scrollRect.verticalNormalizedPosition,
                _targetNormalizedPos,
                Time.deltaTime * scrollSpeed
            );

            if (Mathf.Abs(scrollRect.verticalNormalizedPosition - _targetNormalizedPos) < 0.0001f)
            {
                scrollRect.verticalNormalizedPosition = _targetNormalizedPos;
                _isScrolling = false;
            }
        }
    }

    private void UpdateTargetPosition(RectTransform target)
    {
        if (target == null) return;

        float contentHeight = scrollRect.content.rect.height;
        float viewportHeight = scrollRect.viewport.rect.height;
        float scrollRange = contentHeight - viewportHeight;

        if (scrollRange <= 0) return;

        // Converte a posição do item para o espaço local do Content
        Vector3 itemLocalPos = scrollRect.content.InverseTransformPoint(target.position);

        // Assume pivot no topo (0.5, 1) para o Content e Itens
        float itemTop = -itemLocalPos.y - (target.rect.height * target.pivot.y);
        float itemBottom = itemTop + target.rect.height;

        float currentScrollPos = (1f - _targetNormalizedPos) * scrollRange;

        if (itemTop < currentScrollPos) // Subindo
        {
            _targetNormalizedPos = 1f - (itemTop / scrollRange);
            _isScrolling = true;
        }
        else if (itemBottom > currentScrollPos + viewportHeight) // Descendo
        {
            _targetNormalizedPos = 1f - ((itemBottom - viewportHeight) / scrollRange);
            _isScrolling = true;
        }

        _targetNormalizedPos = Mathf.Clamp01(_targetNormalizedPos);
    }
}