using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    // Name of the next scene
    public string nextSceneName = "Level 1";

    private void OnMouseDown()
    {
        // Load the next scene when the GameObject is clicked
        SceneManager.LoadScene(nextSceneName);
    }
}
