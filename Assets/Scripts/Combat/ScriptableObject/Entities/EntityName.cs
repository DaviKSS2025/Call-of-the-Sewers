using UnityEngine;

[CreateAssetMenu(fileName = "EntityName", menuName = "Character Names/EntityName")]
public class EntityName : ScriptableObject
{
    [SerializeField] private string _name;

    public string Name
    {
        get => _name;
    }
}
