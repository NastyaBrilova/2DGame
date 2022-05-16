using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicScript : MonoBehaviour
{
    public AudioSource _audio;
    private bool isPlay = false;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main Menu")
        {
            if (isPlay)
            {
                Destroy();
                Object.Destroy(gameObject);
            }
            else isPlay = true;
        }
        else if (scene.name == "Level") _audio.mute = true;
        else _audio.mute = false;
    }

    void Destroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
