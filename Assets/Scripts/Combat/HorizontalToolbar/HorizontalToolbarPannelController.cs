using UnityEngine;

public class HorizontalToolbarPannelController : MonoBehaviour
{
    [SerializeField] private SelectionChannel _selectionChannel;
    [SerializeField] private TurnChangeChannel _turnChangeChannel;
    [SerializeField] private BaseEntityController _playerEntity;
    private Animator _animator;
    private static readonly int Appearing = Animator.StringToHash("Appearing");
    private static readonly int Disappearing = Animator.StringToHash("Disappearing");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _selectionChannel.StartSelectionUI += HideToolbar;
        _selectionChannel.SelectionConfirmed += HideToolbar;
        _turnChangeChannel.HideUIOnEndActions += HideToolbar;
        _selectionChannel.SelectionEnd += ShowToolBar;
        _turnChangeChannel.UpdateCurrentTurnUser += OnPlayerTurnStarted;
    }
    private void OnDisable()
    {
        _selectionChannel.StartSelectionUI -= HideToolbar;
        _selectionChannel.SelectionConfirmed -= HideToolbar;
        _turnChangeChannel.HideUIOnEndActions -= HideToolbar;
        _selectionChannel.SelectionEnd -= ShowToolBar;
        _turnChangeChannel.UpdateCurrentTurnUser -= OnPlayerTurnStarted;
    }

    private void HideToolbar()
    {
        _animator.ResetTrigger(Appearing);
        _animator.SetTrigger(Disappearing);
    }

    private void ShowToolBar()
    {
        _animator.ResetTrigger(Disappearing);
        _animator.SetTrigger(Appearing);
    }

    private void OnPlayerTurnStarted(BaseEntityController entity)
    {
        if (entity == _playerEntity)
        {
            ShowToolBar();
        }
    }
}
