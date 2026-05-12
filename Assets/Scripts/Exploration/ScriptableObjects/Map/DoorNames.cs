using UnityEngine;

[CreateAssetMenu(fileName = "DoorNames", menuName = "Itens/DoorNames")]
public class DoorNames : ScriptableObject
{
    [SerializeField] private string _doorName;

    public string DoorName => _doorName;
}
