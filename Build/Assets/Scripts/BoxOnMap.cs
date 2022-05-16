using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BoxOnMap : MonoBehaviour
{
    public int BoxID = 0;
    public Sprite[] statsLevel;

    private void Start()
    {
        if (PlayerScript.PlayerProgressMap - BoxID == 0) GetComponentInParent<Image>().sprite = statsLevel[1];
        else if(PlayerScript.PlayerProgressMap > BoxID) GetComponentInParent<Image>().sprite = statsLevel[0];
        else GetComponentInParent<Image>().sprite = statsLevel[2];
    }
    private void OnMouseUp()
    {
        if (PlayerScript.PlayerProgressMap - BoxID == 0)
        {
            SceneManager.LoadScene("Level", LoadSceneMode.Single);
        }
    }
}


/*  private GameObject[] boxes;
    boxes = GameObject.FindGameObjectsWithTag("LevelBox");
 * 
 *  gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
           PlayerScript.PlayerProgressMap++;
           for (int i = 0; i < boxes.Length; i++)
           {
               if (boxes[i].GetComponent<BoxOnMap>().BoxID > BoxID)
               {
                   boxes[i].GetComponent<SpriteRenderer>().color = Color.white;
                   break;
               }
           }*/
