using UnityEngine;

public class SelectCharType : MonoBehaviour
{
    public int charMenuID = 0;
    public bool active;
    public GameObject selectCharBox, selectMenu;
    private GameObject manager;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameController");
    }
    private void OnMouseUp()
    {
        if(active)
        {
            SelectChars selectchars = selectCharBox.GetComponent<SelectChars>();
            ClickSelectMenu selectmenu = selectMenu.GetComponent<ClickSelectMenu>();
            if (selectchars.SelectUse != -1) // если слот уже был занят
            {
                selectmenu.charuse[selectchars.SelectUse-1] = false;
                selectchars.SelectUse = -1;
            }
            int charid = charMenuID + 1;
            manager.GetComponent<ManagerScript>().selectCharUse[selectchars.SelectID] = charid;
            selectCharBox.GetComponent<SpriteRenderer>().sprite = selectmenu.charSprites[charid]; //ManagerScript.charColor[charid];
            selectmenu.charuse[charMenuID] = true;
            selectMenu.SetActive(false);
            selectmenu.preSelectID = -1;
            selectchars.SelectUse = charid;
            active = false;
        }
    }
}
