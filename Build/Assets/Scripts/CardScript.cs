using UnityEngine;
using TMPro;

public class CardScript : MonoBehaviour
{
    //private GameObject manager;
    private GameObject enemySelect = null;
    public int cardID = 0, animTimer = 0, dragCard = 0;
    public bool healCard = false;
    
    private Vector3 screenPosition;
    private Vector2 startPos;
    private GameObject[] chars;
    public TMP_Text textDamage;
    public GameObject cardImage, soundManager, promptPanel;
    private int enterTriggerCount = 0;

    [SerializeField] private GameObject skipscript;

    private ManagerScript managerScript;
    private void Start()
    {
        startPos = transform.position;
        //manager = GameObject.FindGameObjectWithTag("GameController");
        managerScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<ManagerScript>();
        chars = GameObject.FindGameObjectsWithTag("Player");
    }
    private void FixedUpdate()
    {
        if (animTimer > 0) animTimer--;
        else if (dragCard == 2 && animTimer == 0)
        {
            transform.position = Vector2.Lerp(transform.position, startPos, 0.1f);
            transform.localScale = new Vector2(0.4f, 0.4f);
            if (transform.position.x == startPos.x || (transform.position.x - startPos.x < 0.001f && transform.position.y - startPos.y < 0.001f)) dragCard = 0;
        }
    }
    private void OnMouseDrag()
    {
        if (managerScript.cardRollback[managerScript.currentPlayer.GetComponent<PlayerFightScript>().charType, cardID] > 0)
        {
            managerScript.GetComponent<ManagerScript>().SetTextInfo("Данную карту нельзя использовать в этом раунде");
        }
        else
        {
            dragCard = 1;
            screenPosition = Input.mousePosition;
            screenPosition.z = 10f;
            //cardImage.transform.position = new Vector3(cardImage.transform.position.x, cardImage.transform.position.y, 10f);
            transform.localScale = new Vector2(0.45f, 0.45f);
            transform.position = Camera.main.ScreenToWorldPoint(screenPosition);
        }
    }
    private void OnMouseDown()
    {
        soundManager.GetComponent<SoundManager>().PlaySound(0);
    }
    private void OnMouseUp()
    {
        if (dragCard == 1)
        {
            dragCard = 2;
            GameObject currentplayer = managerScript.currentPlayer;

            if (currentplayer != null)
            {
                if (enemySelect != null)
                {
                    if(PlayerInfo.PromptID == 2) promptPanel.GetComponent<PromptScript>().SetTextPrompt(3, true, -46.62f, 54.4f, 2, true);
                    PlayerFightScript playerscript = currentplayer.GetComponent<PlayerFightScript>();
                    int chartype = playerscript.charType;
                    int damage = managerScript.CardTypeInfo[ManagerScript.cardForChars[chartype, cardID], 0];
                    if (!healCard)
                    {
                        EnemyFightScript enemyscript = enemySelect.GetComponent<EnemyFightScript>();
                        if (PlayerInfo.playerSkills[1] && chartype == 0) enemyscript.enemyHP -= damage+5;
                        else enemyscript.enemyHP -= damage;
                        int cardtype = managerScript.CardTypeInfo[ManagerScript.cardForChars[chartype, cardID], 1];
                        if (cardtype == 0)
                        {
                            playerscript.charHP -= Mathf.RoundToInt(managerScript.damageForEnemy[cardID, enemyscript.enemyType] / 2);
                            managerScript.UpdateHealthBar(currentplayer, false);
                            soundManager.GetComponent<SoundManager>().PlaySound(1);
                        }
                        else if (cardtype == 1)
                        {
                            managerScript.cardRollback[chartype, cardID] = 2;
                            soundManager.GetComponent<SoundManager>().PlaySound(3);
                        }
                    }
                    else
                    {
                        enemySelect.GetComponent<PlayerFightScript>().charHP += damage;
                        managerScript.cardRollback[chartype, cardID] = 2;
                        soundManager.GetComponent<SoundManager>().PlaySound(4);
                    }
                    managerScript.UpdateHealthBar(enemySelect, !healCard);
                    enemySelect = null;
                    enterTriggerCount = 0;
                    playerscript.usedOnRound = true;
                    currentplayer.GetComponent<SpriteRenderer>().color = Color.grey;
                    int full = 0;
                    for (int i = 0; i < chars.Length; i++)
                    {
                        if (chars[i] == null || chars[i].GetComponent<PlayerFightScript>().usedOnRound) continue;
                        managerScript.currentPlayer = chars[i];
                        managerScript.UpdateCards(chars[i]);
                        full++;
                        break;
                    }
                    if (full == 0) skipscript.GetComponent<Skipturn>().SkipTurn();
                }
            }
            else managerScript.GetComponent<ManagerScript>().SetTextInfo("Выберите персонажа");
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (dragCard == 1)
        {
            GameObject current = managerScript.currentPlayer;
            int chartype = current.GetComponent<PlayerFightScript>().charType, cardtype = ManagerScript.cardForChars[chartype, cardID];
            if (!healCard)
            {
                if (col.CompareTag("Enemy"))
                {
                    if (enemySelect != null) ResetHPBar(enemySelect);
                    enemySelect = col.gameObject;
                    EnemyFightScript enemyscript = enemySelect.GetComponent<EnemyFightScript>();

                    if (PlayerInfo.playerSkills[1] && chartype == 0) enemyscript.fakeDamage = managerScript.CardTypeInfo[cardtype, 0]+5;
                    else enemyscript.fakeDamage = managerScript.CardTypeInfo[cardtype, 0];

                    int fakeHP = enemyscript.enemyHP - enemyscript.fakeDamage;
                    if (fakeHP < 0) fakeHP = 0;
                    FakeDamageForHealthBar(enemySelect, fakeHP, false, true);
                    if (managerScript.CardTypeInfo[cardtype, 1] == 0)
                    {
                        int damage = Mathf.RoundToInt(managerScript.damageForEnemy[cardID, enemyscript.enemyType] / 2);
                        FakeDamageForHealthBar(current, damage, false, false);
                    }
                }
            }
            else
            {
                if (col.CompareTag("Player"))
                {
                    if (enemySelect != null) ResetHPBar(enemySelect);
                    enemySelect = col.gameObject;
                    PlayerFightScript playerscript = enemySelect.GetComponent<PlayerFightScript>();
                    playerscript.fakeDamage = managerScript.CardTypeInfo[cardtype, 0];
                    int fakeHP = playerscript.fakeDamage, maxhp = playerscript.MaxCharHP;
                    if (fakeHP > maxhp) fakeHP = maxhp;
                    FakeDamageForHealthBar(enemySelect, fakeHP, true, false);
                }
            }
            enterTriggerCount++;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (dragCard == 1)
        {
            ResetHPBar(enemySelect);
            if (!healCard && --enterTriggerCount <= 0)
            {
                if (col.CompareTag("Enemy"))
                {
                    enemySelect = null;
                }
            }
            else
            {
                if (col.CompareTag("Player"))
                {
                    enemySelect = null;
                }
            }
        }
    }
    private void ResetHPBar(GameObject enemy)
    {
        if(enemy != null)
        {
            if(!healCard)
            {
                EnemyFightScript enemyscript = enemy.GetComponent<EnemyFightScript>();

                enemyscript.fakeDamage = 0;
                enemyscript.imageHP.fillAmount = 1f / enemyscript.MaxEnemyHP * enemyscript.enemyHP; // MaxEnemyHP / 100 * enemyHP
                enemyscript.barHP.text = "" + enemyscript.enemyHP;

                GameObject current = managerScript.currentPlayer;
                PlayerFightScript playerscript = current.GetComponent<PlayerFightScript>();

                if (managerScript.CardTypeInfo[ManagerScript.cardForChars[playerscript.charType, cardID], 1] == 0)
                {
                    playerscript.imageHP.fillAmount = 1f / playerscript.MaxCharHP * playerscript.charHP;
                    playerscript.barHP.text = "" + playerscript.charHP;
                }
            }
            else
            {
                PlayerFightScript playerscript = enemy.GetComponent<PlayerFightScript>();

                playerscript.fakeDamage = 0;
                playerscript.viewDamage.fillAmount = 1f / playerscript.MaxCharHP * playerscript.charHP; // MaxEnemyHP / 100 * enemyHP
                playerscript.barHP.text = "" + playerscript.charHP;
            }
        }
    }
    void FakeDamageForHealthBar(GameObject currentchar, float hp, bool isHeal, bool isEnemy)
    {
        if (isEnemy)
        {
            EnemyFightScript enemyscript = currentchar.GetComponent<EnemyFightScript>();
            if (isHeal)
            {
                enemyscript.viewDamage.color = new Color(0.156371f, 0.5176471f, 0);
                enemyscript.barHP.text = enemyscript.enemyHP + " + " + hp;
            }
            else
            {
                enemyscript.imageHP.fillAmount = 1f / enemyscript.MaxEnemyHP * hp;
                enemyscript.viewDamage.color = new Color(0.3584906f, 0.006949624f, 0);
                enemyscript.barHP.text = enemyscript.enemyHP + " - " + enemyscript.fakeDamage;
            }
        }
        else
        {
            PlayerFightScript playerscript = currentchar.GetComponent<PlayerFightScript>();
            if (isHeal)
            {
                playerscript.viewDamage.fillAmount = 1f / playerscript.MaxCharHP * (hp + playerscript.charHP);
                playerscript.viewDamage.color = new Color(0.156371f, 0.5176471f, 0);
                playerscript.barHP.text = playerscript.charHP + " + " + hp;
            }
            else
            {
                playerscript.imageHP.fillAmount = 1f / playerscript.MaxCharHP * (playerscript.charHP - hp);
                playerscript.viewDamage.color = new Color(0.3584906f, 0.006949624f, 0);
                playerscript.barHP.text = playerscript.charHP + " - " + hp;
            }
        }
    }
}
