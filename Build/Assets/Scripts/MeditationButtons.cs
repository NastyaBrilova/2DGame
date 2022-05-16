using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class MeditationButtons : MonoBehaviour
{
    private int typeSkill = -1;
    public TMP_Text textInfo;
    public GameObject[] buttons;
    public void UseButton(int type = 0)
    {
        switch (type)
        {
            case 0:
            {
                if (PlayerInfo.playerSkillPoints > 0)
                {
                    if (typeSkill != -1)
                    {
                        if (!PlayerInfo.playerSkills[typeSkill])
                        {
                            PlayerInfo.playerSkillPoints--;
                            PlayerInfo.playerSkills[typeSkill] = true;
                            typeSkill = -1;
                            SetTextInfo("Навык успешно изучен!");
                        }
                        else SetTextInfo("У Вас уже есть данное умение.");
                    }
                    else SetTextInfo("Сначала выберите умение.");
                }
                else SetTextInfo("Недостаточно очков для прокачки.");
                break;
            }
            case 1:
            {
                SceneManager.LoadScene("Map", LoadSceneMode.Single);
                break;
            } 
            case 2:
            case 3:
            case 4:
            {
                typeSkill = type - 2;
                if (typeSkill == 0)
                {
                    buttons[0].GetComponent<Image>().color = new Color(0.4386792f, 0.7300079f, 1);
                    buttons[1].GetComponent<Image>().color = buttons[2].GetComponent<Image>().color = Color.white;
                }
                else if (typeSkill == 1)
                {
                    buttons[1].GetComponent<Image>().color = new Color(0.4386792f, 0.7300079f, 1);
                    buttons[0].GetComponent<Image>().color = buttons[2].GetComponent<Image>().color = Color.white;
                }
                else if (typeSkill == 2)
                {
                    buttons[2].GetComponent<Image>().color = new Color(0.4386792f, 0.7300079f, 1);
                    buttons[0].GetComponent<Image>().color = buttons[1].GetComponent<Image>().color = Color.white;
                }
                break;
            }
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