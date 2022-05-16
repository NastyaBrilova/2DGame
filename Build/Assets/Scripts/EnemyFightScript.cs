using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyFightScript : MonoBehaviour
{
    public int enemyID = 0, enemyType = 0, enemyHP = 25, MaxEnemyHP, fakeDamage = 0;
    public bool enemyUsed;
    public Image imageHP, viewDamage;
    public TMP_Text barHP;
    public int slotForEnemy = -1;

    private void Start()
    {
        MaxEnemyHP = enemyHP;
    }
}