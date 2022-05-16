using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static int PlayerProgressMap = 0;

    /* private void Start()
     {
         UpdateMapLevels();
     }
     public void UpdateMapLevels()
     {
         GameObject[] boxes = GameObject.FindGameObjectsWithTag("LevelBox");
         for (int i = 0; i < boxes.Length; i++)
         {
             int box = boxes[i].GetComponent<BoxOnMap>().BoxID;
             if (PlayerProgressMap > box)
             {
                 boxes[i].GetComponent<SpriteRenderer>().color = Color.blue;
             }
             else if (PlayerProgressMap < box)
             {
                 boxes[i].GetComponent<SpriteRenderer>().color = Color.grey;
             }
             else boxes[i].GetComponent<SpriteRenderer>().color = Color.white;
         }
     }*/
}
