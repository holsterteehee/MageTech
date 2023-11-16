using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextSceneOnTouch : MonoBehaviour
{
    public string nextSceneName; // The name of the next scene to load.

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}