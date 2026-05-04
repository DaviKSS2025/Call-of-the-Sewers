using UnityEngine;

[CreateAssetMenu(fileName = "MovementSpeed", menuName = "Scriptable Objects/MovementSpeed")]
public class MovementSpeed : ScriptableObject
{
    [SerializeField] private float _walkingSpeed;

    public float WalkingSpeed
    {
        get => _walkingSpeed;
    }
}
