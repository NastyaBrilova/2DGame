using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ManagerScript : MonoBehaviour
{
    public static Color[] charColor = { Color.red, Color.green, Color.blue, Color.yellow, Color.white, Color.black };
    public Sprite[] charSprites, enemySprites;
    public static bool[] charHave = { false, false, false, false, false };
    public int[] selectCharUse = { 0, -1, -1 }, EnemyUse = { -1, -1 };
    private Vector2[] startPosChars = { new Vector2(-2.59f, -0.89f), new Vector2(-6.78f, -0.9f), new Vector2(-7.34f, -0.9f) };
    private Vector2[] startPosEnemy = { new Vector2(3.2f, -0.89f), new Vector2(6.37f, -0.89f), new Vector2(7.59f, -0.9f) };
    public GameObject PrefabPlayer, PrefabEnemy, SelectMenu, Interface, currentPlayer,
        loseMenu, winMenu, pauseButton, soundManager, promptPanel;
    public GameObject[] cards, chars;
    public int[,] cardRollback = { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
    private bool gameEnd = false;

    public TMP_Text textInfo;

    public PlayerInfo playerinfo;

    //public Sprite[] cardSprite;

    public int[,] damageForEnemy = // дамаг, который наносят враги игроку
    {
        {8,15,15,18}, // 0 card, enemy
        {8,15,15,18}, // 1 card, enemy
        {8,15,15,18} // 2 card, enemy
        //{13,17,21,25} // 3 card, enemy
    };

    public Sprite[] cardSprite;
    public int[,] CardTypeInfo = // damage, type (0 - close combat, 1 - distant battle, 2 - heal card), needlevelforusecard
    {
        {15,0},
        {20,0},
        {25,0},
        {35,0},
        {40,0},

        {20,1},
        {20,1},
        {25,1},
        {30,1},

        {20,2},
        {35,2}
    };
    public static int[,] cardForChars = // char, cards карты, которые используют персонажи
    {
        {1,5, 9},
        {2,6, 6},
        {0,10, 10},
        {5,10, 7}
    };

    private int[,] enemyOnLevel =
    {
        {0, -1},
        {0, 0},
        {1, 0},
        {1, 1},
        {2, -1},
        {2, 0},
        {2, 1},
        {2, 2},
        {3, -1}
    };

    GameObject[] Enemys;

    private void Start()
    {
        EnemyUse[0] = enemyOnLevel[PlayerScript.PlayerProgressMap, 0];
        EnemyUse[1] = enemyOnLevel[PlayerScript.PlayerProgressMap, 1];
        StartScene(EnemyUse, 2);
        //SetTextInfo("Уровень: " + PlayerScript.PlayerProgressMap+1);
    }
    public void StartLevel(int[] character) // & Spawn Chars
    {
        int full = 0;
        for (int i = 0; i < character.Length; i++)
        {
            if (character[i] == -1) continue;
            GameObject player = Instantiate(PrefabPlayer, startPosChars[full], Quaternion.identity);
            player.GetComponent<SpriteRenderer>().sprite = charSprites[selectCharUse[i]];
            PlayerFightScript playerscript = player.GetComponent<PlayerFightScript>();
            playerscript.charType = selectCharUse[i];
            if (i == 0) playerscript.charHP = PlayerInfo.playerSkills[0] ? 65 : 50; // Если это ГГ
            else
            {
                if (selectCharUse[i] == 2) playerscript.charHP = 50;
                else playerscript.charHP = 35;
            }
            playerscript.barHP.text = "" + playerscript.charHP;
            playerscript.slotForChar = full;
            full++;
        }
        if (full == 0) Debug.Log("We have a problems #01");
        chars = GameObject.FindGameObjectsWithTag("Player");
        currentPlayer = chars[0];
        SelectMenu.SetActive(false);
        Interface.SetActive(true);
        cards = GameObject.FindGameObjectsWithTag("Cards");
        Enemys = GameObject.FindGameObjectsWithTag("Enemy");
        UpdateCards(currentPlayer);
        switch (PlayerScript.PlayerProgressMap)
        {
            case 0:
                promptPanel.GetComponent<PromptScript>().SetTextPrompt(2, true, -263.65f, 93.4f, 0);
                break;
            case 2:
                if(selectCharUse[1] != -1)
                {
                    promptPanel.GetComponent<PromptScript>().SetTextPrompt(4, true, 100.51f, 59.61608f, 1, true);
                }
                break;
        }
    }
    public void StartScene(int[] enemys, int map = 0) // Spawn Enemy & Change map
    {
        for (int i = 0; i < enemys.Length; i++)
        {
            if (enemys[i] == -1) continue;
            GameObject enemy = Instantiate(PrefabEnemy, startPosEnemy[i], Quaternion.identity);
            enemy.GetComponent<SpriteRenderer>().sprite = enemySprites[EnemyUse[i]];
            EnemyFightScript enemyscript = enemy.GetComponent<EnemyFightScript>();
            enemyscript.enemyType = EnemyUse[i];
            if (EnemyUse[i] == 3) enemyscript.enemyHP = 200;
            else enemyscript.enemyHP = 25*(EnemyUse[i]+1);
            enemyscript.enemyID = i;
            enemyscript.barHP.text = "" + enemyscript.enemyHP;
            enemyscript.slotForEnemy = i;
            switch(PlayerScript.PlayerProgressMap)
            {
                case 0:
                    promptPanel.GetComponent<PromptScript>().SetTextPrompt(1, true, 0, -52f, 0);
                    break;
                case 2:
                    promptPanel.GetComponent<PromptScript>().SetTextPrompt(0, true, -352.18f, -79.9f, 1);
                    break;
            }
        }
    }
    public void UpdateCards(GameObject current)
    {
        int chartype = current.GetComponent<PlayerFightScript>().charType;
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].GetComponent<CardScript>().cardImage.GetComponent<Image>().sprite = cardSprite[cardForChars[chartype, i]];

            if (cardRollback[currentPlayer.GetComponent<PlayerFightScript>().charType, cards[i].GetComponent<CardScript>().cardID] > 0) cards[i].GetComponent<CardScript>().cardImage.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            else cards[i].GetComponent<CardScript>().cardImage.GetComponent<Image>().color = new Color(1, 1, 1, 1);

            if (CardTypeInfo[cardForChars[chartype, i], 1] != 2)
            {
                if(PlayerInfo.playerSkills[1] && chartype == 0) cards[i].GetComponent<CardScript>().textDamage.text = "Наносит <b><color=green>" + (CardTypeInfo[cardForChars[chartype, i], 0]+5) + "</color> ед.</b> урона";
                else cards[i].GetComponent<CardScript>().textDamage.text = "Наносит <b>" + CardTypeInfo[cardForChars[chartype, i], 0] + " ед.</b> урона";
            }
            else cards[i].GetComponent<CardScript>().textDamage.text = "Восстановит <b>" + CardTypeInfo[cardForChars[chartype, i], 0] + " ед.</b> ОЗ";
            cards[i].GetComponent<CardScript>().healCard = CardTypeInfo[cardForChars[chartype, i], 1] == 2 ? true : false;
        }
        if ((chartype == 0 && !PlayerInfo.playerSkills[2]) || chartype != 0)
        {
            //cards[0].transform.localPosition = new Vector3(-4.18f, -2.48f, 30);
            //cards[0].GetComponent<CardScript>().startPos = new Vector3(-4.18f, -2.48f, 30);
            //cards[1].GetComponent<CardScript>().startPos = new Vector3(-1.49f, -2.48f, 30);
            cards[2].SetActive(false);
        }
        else cards[2].SetActive(true);
    }
    public void UpdateHealthBar(GameObject character, bool enemy)
    {
        if (enemy)
        {
            EnemyFightScript enemyscript = character.GetComponent<EnemyFightScript>();
            int hp = enemyscript.enemyHP, maxhp = enemyscript.MaxEnemyHP;
            if (hp > 0)
            {
                if (hp > maxhp) enemyscript.enemyHP = hp = maxhp;
                enemyscript.imageHP.fillAmount = enemyscript.viewDamage.fillAmount = 1f / maxhp * hp;
                enemyscript.barHP.text = "" + hp;
            }
            else
            {
                enemyscript.enemyHP = 0;
                EnemyUse[enemyscript.slotForEnemy] = -1;
                Object.Destroy(character);
                int full = 0;
                for (int i = 0; i < EnemyUse.Length; i++)
                {
                    if (EnemyUse[i] == -1) continue;
                    full++;
                }
                if (full == 0) PlayerWinner(500);
            }
        }
        else
        {
            PlayerFightScript playerscript = character.GetComponent<PlayerFightScript>();
            int hp = playerscript.charHP, maxhp = playerscript.MaxCharHP;
            if (hp > 0)
            {
                if (hp > maxhp) playerscript.charHP = hp = maxhp;
                playerscript.imageHP.fillAmount = playerscript.viewDamage.fillAmount = 1f / maxhp * hp; // MaxEnemyHP / 100 * enemyHP
                playerscript.barHP.text = "" + hp;
            }
            else
            {
                playerscript.charHP = 0;
                int slotchar = playerscript.slotForChar;
                selectCharUse[slotchar] = -1;
                chars[slotchar] = null;
                Object.Destroy(character);
                int full = 0;
                for (int i = 0; i < selectCharUse.Length; i++)
                {
                    if (selectCharUse[i] == -1) continue;
                    full++;
                }
                if (full == 0) TheEnd();
            }
        }
    }
    public void EnemyStep()
    {
        if (!gameEnd)
        {
            //GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
            int full = 0;
            for (int i = 0; i < Enemys.Length; i++)
            {
                if (EnemyUse[i] == -1 || Enemys[i].GetComponent<EnemyFightScript>().enemyUsed) continue;
                StartCoroutine(FightEnemy(Enemys[i]));
                full++;
                break;
            }
            if (full == 0)
            {
                for (int i = 0; i < Enemys.Length; i++)
                {
                    if (EnemyUse[i] == -1) continue;
                    Enemys[i].GetComponent<EnemyFightScript>().enemyUsed = false;
                }
                for (int i = 0; i < cards.Length; i++)
                {
                    if (i == 2 && !PlayerInfo.playerSkills[2]) continue;
                    cards[i].SetActive(true);
                    cards[i].transform.position = new Vector2(-8.0f, -3.2527f);
                    CardScript cardscript = cards[i].GetComponent<CardScript>();
                    cardscript.animTimer = (i + 1) * 5; //cards[i].GetComponent<CardScript>().animTimer = (i + 1) * 5;
                    cardscript.dragCard = 2;
                    soundManager.GetComponent<SoundManager>().PlaySound(2);
                }
                for (int i = 0; i < chars.Length; i++)
                {
                    if (chars[i] == null) continue;
                    chars[i].GetComponent<SpriteRenderer>().color = Color.white;
                    chars[i].GetComponent<PlayerFightScript>().usedOnRound = false;
                    for (int s = 0; s < cards.Length; s++) cardRollback[chars[i].GetComponent<PlayerFightScript>().charType, s]--;
                }
                int findchar = FindCharForUse();
                currentPlayer = chars[findchar];
                UpdateCards(chars[findchar]);
                SetTextInfo("Ход врагов завершен");
            }
        }
        else
        {
            gameEnd = false;
            SetTextInfo("Ход врагов завершен [преждевременно]");
        }
    }
    private int FindCharForUse()
    {
        for (int i = 0; i < chars.Length; i++)
        {
            if (chars[i] == null) continue;
            return i;
        }
        return -1;
    }
    IEnumerator FightEnemy(GameObject enemy)
    {
        int randchar = SearchCharForAttack();
        yield return new WaitForSeconds(2);
        if(randchar != -1)
        {
            int enemytype = enemy.GetComponent<EnemyFightScript>().enemyType;
            chars[randchar].GetComponent<PlayerFightScript>().charHP -= damageForEnemy[Random.Range(0, 3), enemytype];
            enemy.GetComponent<EnemyFightScript>().enemyUsed = true;
            UpdateHealthBar(chars[randchar], false);
            EnemyStep();
            soundManager.GetComponent<SoundManager>().PlaySound(enemytype+5);
            //Debug.Log("EnemyStep");
        }
        else TheEnd();
    }

    private int tryFind = 0;
    private int SearchCharForAttack()
    {
        int randchar = Random.Range(0, chars.Length);
        if (selectCharUse[randchar] == -1)
        {
            if (++tryFind > 4)
            {
                tryFind = 0;
                return FindCharForUse();
            }
            else
            {
                tryFind = 0;
                return SearchCharForAttack();
            }
        }
        else
        {
            tryFind = 0;
            //Debug.Log("Рандом сработал");
            return randchar;
        }
    }
    private void TheEnd()
    {
        if (!gameEnd)
        {
            promptPanel.GetComponent<PromptScript>().SetTextPrompt(0, false);
            Interface.SetActive(false);
            loseMenu.SetActive(true);
            pauseButton.SetActive(false);
            gameEnd = true;
            SetTextInfo("Вы проиграли!");
        }
    }
    private void PlayerWinner(int exp)
    {
        if (!gameEnd)
        {
            promptPanel.GetComponent<PromptScript>().SetTextPrompt(0, false);
            switch (PlayerScript.PlayerProgressMap)
            {
                case 1:
                    charHave[0] = true;
                    promptPanel.GetComponent<PromptScript>().SetTextPrompt(5, true, -616.25f, -238f, 2, true);
                    break;
                case 3:
                    charHave[1] = true;
                    SetTextInfo("Вы открыли нового союзника!");
                    break;
            }
            PlayerScript.PlayerProgressMap++;
            Interface.SetActive(false);
            pauseButton.SetActive(false);
            gameEnd = true;
            playerinfo.GivePlayerExp(exp);
            winMenu.SetActive(true);
        }
    }

    public void SetTextInfo(string text)
    {
        textInfo.text = text;
        StartCoroutine(ResetTextInfo());
    }

    IEnumerator ResetTextInfo()
    {
        yield return new WaitForSeconds(5);
        textInfo.text = " ";
    }
}
