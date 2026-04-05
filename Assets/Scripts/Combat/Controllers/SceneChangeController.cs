using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeController : MonoBehaviour
{
    [SerializeField] private SceneChangeChannel _sceneChangeChannel;

    private void OnEnable()
    {
        _sceneChangeChannel.SceneChanged += OnSceneChanged;
        _sceneChangeChannel.GoToTargetScene += OnSceneStartFromMenu;
        _sceneChangeChannel.NewGameStarted += OnNewGameStarted;
    }

    private void OnDisable()
    {
        _sceneChangeChannel.SceneChanged -= OnSceneChanged;
        _sceneChangeChannel.GoToTargetScene -= OnSceneStartFromMenu;
        _sceneChangeChannel.NewGameStarted -= OnNewGameStarted;
    }

    private void OnSceneChanged()
    {
        SceneManager.LoadScene(SaveManager.Instance.Data.CurrentMapName.ToString());
    }
    private void OnSceneStartFromMenu(SceneNames targetMap)
    {
        SceneManager.LoadScene(targetMap.ToString());
    }
    private void OnNewGameStarted()
    {
        SceneManager.LoadScene(SceneNames.ChangeName.ToString());
    }
}
