using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class BackgroundCombatImage : MonoBehaviour
{
    [SerializeField] private BackgroundDatabase _database;
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }
    private void Start()
    {
        _image.sprite = _database.GetBackgroundSprite(MapDataController.Instance.RuntimeExplorationData.CurrentMapName);
    }
}
