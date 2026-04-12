using UnityEngine;

[CreateAssetMenu(fileName = "StatusEffectData", menuName = "Scriptable Objects/StatusEffectData")]
public abstract class StatusEffectData : ScriptableObject
{
    public Sprite StatusSprite;
    public abstract StatusEffectInstance CreateInstance(BaseEntityController target, int duration);
}
