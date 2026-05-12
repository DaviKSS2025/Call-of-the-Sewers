using UnityEngine;

[CreateAssetMenu(fileName = "IdleChance", menuName = "Enemies/IdleChance")]
public class IdleChance : ScriptableObject
{
    [SerializeField] private int _idleChance;

    public int IdleChancePercentage
    {
        get => _idleChance;
    }
}
