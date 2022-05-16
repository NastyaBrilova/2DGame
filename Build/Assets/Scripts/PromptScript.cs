using UnityEngine;
using TMPro;
using System.Collections;

public class PromptScript : MonoBehaviour
{
    public Sprite[] typeSprite;
    public GameObject headBG;
    public TMP_Text textInfo;

    public void SetTextPrompt(int textID, bool on, float posX = 0, float posY = 0, int type = 0, bool timer = false)
    {
        if(on)
        {
            string[] textforPrompt =
            {
                "Вы открыли нового союзника! Нажмите на иконку слева, чтоб увидеть всех доступных союзников",
                "Справа находятся враги, которых нужно одолеть. Чтобы начать бой нажмите кнопку <b>\"Старт\"</b>",
                "Здесь находятся Ваши карты. Чтоб атаковать противника - перетащите карту на врага",
                "Помните: используя карты ближнего боя - Вы тоже теряете <b>немного</b> ОЗ. В то время, как карты дальнего боя имеют <b>откат</b> (1 раунд)",
                "Все персонажи ходят по очереди, но Вы можете выбрать, кто пойдет первым. Для этого кликните на любого союзного персонажа.",
                "Вы получили 1 очко навыка! Чтоб улучшить Ваши умения - вернитесь на карту и зайдите в <b>\"Медитацию\"</b>"
            };
            gameObject.SetActive(true);
            textInfo.text = textforPrompt[textID];
            PlayerInfo.PromptID = textID;
            transform.localPosition = new Vector2(posX, posY);
            if(timer) StartCoroutine(ResetTextInfo());
            switch (type)
            {
                case 0: // Стрелка вниз
                    GetComponent<SpriteRenderer>().sprite = typeSprite[0];
                    GetComponent<SpriteRenderer>().flipY = false;
                    headBG.transform.localPosition = new Vector2(0, 2.64f);
                    textInfo.transform.localPosition = new Vector2(0, 0.1137085f);
                    break;
                case 1: // Стрелка влево
                    GetComponent<SpriteRenderer>().sprite = typeSprite[1];
                    GetComponent<SpriteRenderer>().flipX = true;
                    GetComponent<SpriteRenderer>().flipY = false;
                    headBG.transform.localPosition = new Vector2(0.58f, 2.64f);
                    textInfo.transform.localPosition = new Vector2(0.53f, 0.1137085f);
                    break;
                case 2: // Стрелка вправо
                    GetComponent<SpriteRenderer>().sprite = typeSprite[1];
                    GetComponent<SpriteRenderer>().flipX = GetComponent<SpriteRenderer>().flipY = false;
                    headBG.transform.localPosition = new Vector2(-0.53f, 2.64f);
                    textInfo.transform.localPosition = new Vector2(-0.56f, 0.1137085f);
                    break;
                case 3: // Стрелка вверх
                    GetComponent<SpriteRenderer>().sprite = typeSprite[0];
                    GetComponent<SpriteRenderer>().flipY = true;
                    headBG.transform.localPosition = new Vector2(0, -3.25f);
                    textInfo.transform.localPosition = new Vector2(0, -0.2f);
                    break;
            }
        }
        else
        {
            PlayerInfo.PromptID = -1;
            textInfo.text = " ";
            gameObject.SetActive(false);
        }
    }
    IEnumerator ResetTextInfo()
    {
        yield return new WaitForSeconds(10);
        SetTextPrompt(0, false);
    }
}
