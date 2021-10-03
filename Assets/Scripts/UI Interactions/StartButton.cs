using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    [SerializeField] private string _sceneToLoad;

    public void OnStartClick()
    {
        SceneManager.LoadSceneAsync(_sceneToLoad);
    }
}
