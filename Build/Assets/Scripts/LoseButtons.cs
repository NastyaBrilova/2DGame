using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseButtons : MonoBehaviour
{
    public GameObject Interface;
    public void UseButton(int type = 0)
    {
        Interface.SetActive(false);
        switch (type)
        {
            case 0:
                SceneManager.LoadScene("Level", LoadSceneMode.Single);
                break;
            case 1:
                SceneManager.LoadScene("Map", LoadSceneMode.Single);
                break;
            case 2:
                SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
                break;
        }
    }
}
