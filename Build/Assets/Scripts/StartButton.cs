using UnityEngine;

public class StartButton : MonoBehaviour
{
    private GameObject manager;
    public bool startedLevel = false;
    public GameObject promptPanel;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameController");
    }
    public void StartLevel()
    {
        if (PlayerInfo.PromptID == -1 || PlayerInfo.PromptID == 1)
        {
            manager.GetComponent<ManagerScript>().StartLevel(manager.GetComponent<ManagerScript>().selectCharUse);
            startedLevel = true;
            gameObject.SetActive(false);
            if(PlayerInfo.PromptID == 1) promptPanel.GetComponent<PromptScript>().SetTextPrompt(0, false);
        }
    }
}
