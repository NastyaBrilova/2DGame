using UnityEngine;

public class Skipturn : MonoBehaviour
{
    private ManagerScript managerScript;
    private void Start()
    {
        managerScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<ManagerScript>();
    }
    public void SkipTurn()
    {
        if(managerScript.currentPlayer != null)
        {
            for (int i = 0; i < managerScript.cards.Length; i++)
            {
                managerScript.cards[i].SetActive(false);
            }
            managerScript.currentPlayer = null;
            managerScript.EnemyStep();
            managerScript.SetTextInfo("Ваш ход завершен");
        }
        else Debug.Log("Ход уже был завершен");
    }
}