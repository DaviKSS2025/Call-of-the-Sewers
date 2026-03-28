using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeController : MonoBehaviour
{
    [SerializeField] private SceneChangeChannel _sceneChangeChannel;

    private void OnEnable()
    {
        _sceneChangeChannel.SceneChanged += OnSceneChanged;
    }

    private void OnDisable()
    {
        _sceneChangeChannel.SceneChanged -= OnSceneChanged;
    }

    private void OnSceneChanged()
    {
        SceneManager.LoadScene(SaveManager.Instance.Data.CurrentMapName.ToString());
    }
}
