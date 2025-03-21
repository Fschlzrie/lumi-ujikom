using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneName;
    public float delay = 1f; // default

    // Untuk dipakai di UnityEvent tanpa parameter
    public void LoadSceneWithDelayFromInspector()
    {
        StartCoroutine(LoadSceneCoroutine(sceneName, delay));
    }

    // Bisa juga manual (lebih fleksibel, tapi gak bisa di Inspector Invoke)
    public void LoadSceneWithDelay(string scene, float d)
    {
        StartCoroutine(LoadSceneCoroutine(scene, d));
    }

    private IEnumerator LoadSceneCoroutine(string scene, float d)
    {
        yield return new WaitForSeconds(d);
        SceneManager.LoadScene(scene);
    }

    public void SwitchScene(string name){
        SceneManager.LoadScene(name);
    }

    public void Exit()
    {
        Debug.Log("Game is exiting...");
        Application.Quit();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
