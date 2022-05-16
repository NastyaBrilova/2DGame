using UnityEngine;

public class DragObject : MonoBehaviour
{
    // Start is called before the first frame update
    private int dragTimer = 0;
    private bool dragonUp = true;
   // public int attackType = 0;
   // private bool isAttack = true;
    private Vector2 startPos;
    void Start()
    {
        dragTimer = Random.Range(1, 100);
        startPos = transform.position;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(--dragTimer <= 0)
        {
            if(dragonUp)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + 0.001f);
                if (transform.position.y > startPos.y + 0.05) dragonUp = false;
            }
            else
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - 0.001f);
                if (transform.position.y <= startPos.y) dragonUp = true;
            }
        }
        /*if(attackType != 0)
        {
            if (isAttack)
            {
                transform.position = new Vector2(attackType == 1 ? transform.position.x + 0.015f : transform.position.x - 0.015f, transform.position.y);
                if (transform.position.x > startPos.x + 0.25f)
                {
                    attackType = 0;
                    isAttack = false;
                }
            }
            else
            {
                transform.position = new Vector2(attackType == 1 ? transform.position.x - 0.015f : transform.position.x + 0.015f, transform.position.y);
                if (transform.position.x <= startPos.x) isAttack = true;
            }
        }*/
    }
}
