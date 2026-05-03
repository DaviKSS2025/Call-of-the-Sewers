using UnityEngine;

[CreateAssetMenu(fileName = "RunChance", menuName = "Player/RunChance")]
public class RunChance : ScriptableObject
{
    [SerializeField] private float _runChance;

    public float RunChancePercentage
    {
        get => _runChance;
    }
}
