using UnityEngine;

[CreateAssetMenu(fileName = "BackgroundData", menuName = "Scriptable Objects/BackgroundData")]
public class BackgroundData : ScriptableObject
{
    [SerializeField] private SceneNames _sceneName;
    [SerializeField] private Sprite _backgroundSprite;

    public Sprite BackgroundSprite => _backgroundSprite;
    public SceneNames SceneName => _sceneName;

}
