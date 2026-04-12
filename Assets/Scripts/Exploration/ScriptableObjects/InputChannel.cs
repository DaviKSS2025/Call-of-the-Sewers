using UnityEngine;
using System;
[CreateAssetMenu(fileName = "InputChannel", menuName = "Channels/InputChannel")]
public class InputChannel : ScriptableObject
{
    public Action<Vector2> OnMove;
    public Action OnInteract;
    public Action OnSubmit;
    public Action OnMenuToggle;
    public Action OnUIRight;
    public Action OnUILeft;
    public Action OnUICancel;
    public void RaiseMove(Vector2 value) => OnMove?.Invoke(value);
    public void RaiseInteract() => OnInteract?.Invoke();
    public void RaiseSubmit() => OnSubmit?.Invoke();
    public void RaiseMenuToggle() => OnMenuToggle?.Invoke();
    public void RaiseUIRight() => OnUIRight?.Invoke();
    public void RaiseUILeft() => OnUILeft?.Invoke();
    public void RaiseUICancel() => OnUICancel?.Invoke();
}