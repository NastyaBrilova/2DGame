using UnityEngine;

public class SelectChars : MonoBehaviour
{
    private Vector2[] PosSelectMenu = new Vector2[] { new Vector2(0.5f, 0f), new Vector2(0.5f, -1.7f), new Vector2(0.5f, -3.43f) };
    public int SelectID = 0, SelectUse = -1;
    public GameObject selectMenu, promptPanel;
    private GameObject[] chars;
    private GameObject managerScript;
    private void Start()
    {
        // chars = GameObject.FindGameObjectsWithTag("Chars");
        chars = selectMenu.GetComponent<ClickSelectMenu>().charCell;
        managerScript = GameObject.FindGameObjectWithTag("GameController");
    }
    private void OnMouseUp()
    {
        if (PlayerInfo.PromptID == 0) promptPanel.GetComponent<PromptScript>().SetTextPrompt(0, false);

        int clickselect = selectMenu.GetComponent<ClickSelectMenu>().preSelectID;
        ManagerScript manager = managerScript.GetComponent<ManagerScript>();
        if (SelectID == 0) manager.SetTextInfo("Нельзя изменить главного героя");
        else if (SelectID == 2 && !PlayerInfo.playerSkills[2]) manager.SetTextInfo("Лидерских качеств не хватает для 3 союзника!");
        else if(clickselect != -1)
        {
            if (SelectID == clickselect)
            {
                clickselect = -1;
                selectMenu.SetActive(false);
            }
            else
            {
                
                //{
                    clickselect = SelectID;
                    selectMenu.transform.localPosition = PosSelectMenu[SelectID];
                    for (int i = 0; i < chars.Length; i++) chars[i].GetComponent<SelectCharType>().selectCharBox = gameObject;
                //}
            }
        }
        else
        {
            //if (SelectID == 0) manager.SetTextInfo("Нельзя изменить ГГ");
            //else if(SelectID == 2 && !PlayerInfo.playerSkills[2]) manager.SetTextInfo("Лидерских качеств не хватает для 3 союзника!");
            //else
            //{
                for (int i = 0; i < chars.Length; i++)
                {
                    if (ManagerScript.charHave[i])
                    {
                        if (!selectMenu.GetComponent<ClickSelectMenu>().charuse[i])
                        {
                            chars[i].GetComponent<SelectCharType>().active = true;
                            chars[i].GetComponent<SelectCharType>().selectCharBox = gameObject;
                            chars[i].GetComponent<SpriteRenderer>().sprite = selectMenu.GetComponent<ClickSelectMenu>().charSprites[i+1];
                            chars[i].GetComponent<SpriteRenderer>().color = Color.white;
                        }
                        else
                        {
                            chars[i].GetComponent<SpriteRenderer>().color = Color.cyan;
                        }
                    }
                    else
                    {
                        chars[i].GetComponent<SpriteRenderer>().sprite = selectMenu.GetComponent<ClickSelectMenu>().charSprites[5];
                    }
                }
                clickselect = SelectID;
                selectMenu.transform.localPosition = PosSelectMenu[SelectID];
                selectMenu.SetActive(true);
            //}
        }
        selectMenu.GetComponent<ClickSelectMenu>().preSelectID = clickselect;
    }
}