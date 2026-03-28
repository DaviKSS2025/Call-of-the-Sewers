using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class TargetPannelController : MonoBehaviour
{
    [SerializeField] private SelectionChannel _selectionChannel;
    private Animator _animator;

    private static readonly int Appearing = Animator.StringToHash("Appearing");
    private static readonly int Disappearing = Animator.StringToHash("Disappearing");
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        _selectionChannel.StartSelectionUI += OnSelectionStarted;
        _selectionChannel.SelectionEnd += OnSelectionEnd;
    }
    private void OnDisable()
    {
        _selectionChannel.StartSelectionUI -= OnSelectionStarted;
        _selectionChannel.SelectionEnd -= OnSelectionEnd;
    }

    private void OnSelectionStarted()
    {
        _animator.ResetTrigger(Disappearing);
        _animator.SetTrigger(Appearing);
    }
    private void OnSelectionEnd()
    {
        _animator.ResetTrigger(Appearing);
        _animator.SetTrigger(Disappearing);
    }
}
