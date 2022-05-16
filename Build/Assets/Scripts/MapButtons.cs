using UnityEngine;
using UnityEngine.SceneManagement;

public class MapButtons : MonoBehaviour
{
    // Start is called before the first frame update
    public void UseButton(int type = 0)
    {
        switch (type)
        {
            case 0:
                SceneManager.LoadScene("Meditation", LoadSceneMode.Single);
                break;
            case 1:
                SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
                break;
        }
    }
}
