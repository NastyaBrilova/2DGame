using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    // Start is called before the first frame update
    public void UseButton(int type = 0)
    {
        switch(type)
        {
            case 0:
                SceneManager.LoadScene("Map", LoadSceneMode.Single);
                break;
            case 1:
                Debug.Log("В разработке");
                break;
            case 2:
                Application.Quit();
                break;
        }
    }
}
