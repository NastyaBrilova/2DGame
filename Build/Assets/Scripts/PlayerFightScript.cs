using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerFightScript : MonoBehaviour
{
    public int charType = 0, charHP = 55, MaxCharHP, slotForChar = -1, fakeDamage = 0;
    private ManagerScript managerScript;
    public bool usedOnRound = false; // , updateHealth = false
    public Image imageHP, viewDamage;
    public TMP_Text barHP;
    
    private void Start()
    {
        managerScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<ManagerScript>();
        MaxCharHP = charHP;
    }
    private void OnMouseUp()
    {
        if (!usedOnRound)
        {
            managerScript.currentPlayer = gameObject;
            managerScript.UpdateCards(gameObject);
        }
        else managerScript.SetTextInfo("Персонаж уже был использован!");
    }
}
