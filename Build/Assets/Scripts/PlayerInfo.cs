using UnityEngine;
using TMPro;

public class PlayerInfo : MonoBehaviour
{
    public static int playerLvl = 1, playerExp = 0, playerSkillPoints = 0, PromptID = -1;
    public static bool[] playerSkills = { false, false, false };

    private GameObject manager;

    public TMP_Text textInfo;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameController");
    }
    public void GivePlayerExp(int exp)
    {
        ManagerScript managerScript = manager.GetComponent<ManagerScript>();
        playerExp += exp;
        textInfo.text = playerExp + "/" + playerLvl * 1000;
        if (playerExp >= playerLvl*1000)
        {
            playerLvl++;
            playerSkillPoints++;
            playerExp = 0;
            managerScript.SetTextInfo("Новый уровень: " + playerLvl + "\nВы получили очко навыка!");
        }
    }
}
