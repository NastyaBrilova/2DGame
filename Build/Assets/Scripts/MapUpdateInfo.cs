using UnityEngine;
using TMPro;

public class MapUpdateInfo : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text[] textInfo;
    void Start()
    {
        textInfo[0].text = "" + PlayerInfo.playerLvl;
        textInfo[1].text = PlayerInfo.playerExp + "/" + PlayerInfo.playerLvl*1000;
        textInfo[2].text = "" + PlayerInfo.playerSkillPoints;
    }
}
