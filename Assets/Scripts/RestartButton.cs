using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class RestartButton : MonoBehaviour
{
    public float delay = 1f;

    public void RestartScene()
    {
        StartCoroutine(RestartAfterDelay());
    }

    IEnumerator RestartAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
}
