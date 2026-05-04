using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BackgroundDatabase", menuName = "Databases/BackgroundDatabase")]
public class BackgroundDatabase : ScriptableObject
{
    [SerializeField] private List<BackgroundData> _backgrounds;

    public Sprite GetBackgroundSprite(SceneNames lastScene)
    {
        foreach (BackgroundData background in _backgrounds)
        {
            if (background.SceneName == lastScene)
            {
                return background.BackgroundSprite;
            }
        }
        return null;
    }
}
