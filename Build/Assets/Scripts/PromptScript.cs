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
                "�� ������� ������ ��������! ������� �� ������ �����, ���� ������� ���� ��������� ���������",
                "������ ��������� �����, ������� ����� �������. ����� ������ ��� ������� ������ <b>\"�����\"</b>",
                "����� ��������� ���� �����. ���� ��������� ���������� - ���������� ����� �� �����",
                "�������: ��������� ����� �������� ��� - �� ���� ������� <b>�������</b> ��. � �� �����, ��� ����� �������� ��� ����� <b>�����</b> (1 �����)",
                "��� ��������� ����� �� �������, �� �� ������ �������, ��� ������ ������. ��� ����� �������� �� ������ �������� ���������.",
                "�� �������� 1 ���� ������! ���� �������� ���� ������ - ��������� �� ����� � ������� � <b>\"���������\"</b>"
            };
            gameObject.SetActive(true);
            textInfo.text = textforPrompt[textID];
            PlayerInfo.PromptID = textID;
            transform.localPosition = new Vector2(posX, posY);
            if(timer) StartCoroutine(ResetTextInfo());
            switch (type)
            {
                case 0: // ������� ����
                    GetComponent<SpriteRenderer>().sprite = typeSprite[0];
                    GetComponent<SpriteRenderer>().flipY = false;
                    headBG.transform.localPosition = new Vector2(0, 2.64f);
                    textInfo.transform.localPosition = new Vector2(0, 0.1137085f);
                    break;
                case 1: // ������� �����
                    GetComponent<SpriteRenderer>().sprite = typeSprite[1];
                    GetComponent<SpriteRenderer>().flipX = true;
                    GetComponent<SpriteRenderer>().flipY = false;
                    headBG.transform.localPosition = new Vector2(0.58f, 2.64f);
                    textInfo.transform.localPosition = new Vector2(0.53f, 0.1137085f);
                    break;
                case 2: // ������� ������
                    GetComponent<SpriteRenderer>().sprite = typeSprite[1];
                    GetComponent<SpriteRenderer>().flipX = GetComponent<SpriteRenderer>().flipY = false;
                    headBG.transform.localPosition = new Vector2(-0.53f, 2.64f);
                    textInfo.transform.localPosition = new Vector2(-0.56f, 0.1137085f);
                    break;
                case 3: // ������� �����
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
