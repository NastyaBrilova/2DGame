using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButtons : MonoBehaviour
{
    public GameObject pauseMenu, Interface, startButton;
    private bool isPaused = false;
    public void UseButton(int type = 0)
    {
        switch (type)
        {
            case 0:
                if(!isPaused)
                {
                    Time.timeScale = 0;
                    pauseMenu.SetActive(true);
                    Interface.SetActive(false);
                    isPaused = true;
                }
                else
                {
                    Time.timeScale = 1;
                    pauseMenu.SetActive(false);
                    if(startButton.GetComponent<StartButton>().startedLevel) Interface.SetActive(true);
                    isPaused = false;
                }
                //isPaused = !isPaused;
                break;
            case 1:
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
                isPaused = false;
                if (startButton.GetComponent<StartButton>().startedLevel) Interface.SetActive(true);
                break;
            case 2:
                isPaused = false;
                Time.timeScale = 1;
                SceneManager.LoadScene("Map", LoadSceneMode.Single);
                break;
            case 3:
                //isPaused = false;
                //Time.timeScale = 1;
                Debug.Log("В разработке!");
                break;
            case 4:
                isPaused = false;
                Time.timeScale = 1;
                SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
                break;
        }
    }
}
