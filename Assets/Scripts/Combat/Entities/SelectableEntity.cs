using UnityEngine;
public enum TargetType
{
    Player,
    NPC,
    Enemy
}
public class SelectableEntity
{
    private BaseEntityController _entity;
    public SelectableEntity(BaseEntityController entity)
    {
        _entity = entity;
    }

    public void Subscribe()
    {
        _entity.SelectionChannel.NewTargetSelected += OnEntitySelected;
        _entity.SelectionChannel.SelectionEnd += OnSelectionEnd;
        _entity.SelectionChannel.SelectionConfirmed += OnSelectionEnd;
        _entity.UnscribeEventsOnDisable += OnDisable;
    }
    public void OnEntitySelected(BaseEntityController entity)
    {
        if (entity == _entity)
        {
            _entity.AnimHandler.PlaySelected();
        }
        else
        {
            OnSelectionEnd();
        }
    }
    public void OnSelectionEnd()
    {
        _entity.AnimHandler.PlayUnselected();
    }

    private void OnDisable()
    {
        _entity.SelectionChannel.NewTargetSelected -= OnEntitySelected;
        _entity.SelectionChannel.SelectionEnd -= OnSelectionEnd;
        _entity.SelectionChannel.SelectionConfirmed -= OnSelectionEnd;
        _entity.UnscribeEventsOnDisable -= OnDisable;
    }
}
