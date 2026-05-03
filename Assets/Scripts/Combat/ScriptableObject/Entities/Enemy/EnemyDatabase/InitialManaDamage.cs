using UnityEngine;

[CreateAssetMenu(fileName = "InitialManaDamage", menuName = "Enemies/InitialManaDamage")]
public class InitialManaDamage : ScriptableObject
{
    [SerializeField] private int _manaDamage;

    public int ManaDamage
    {
        get => _manaDamage;
    }
}
