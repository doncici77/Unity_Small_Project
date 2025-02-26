using UnityEngine;

public class DropCheck : MonoBehaviour
{
    public static bool canDrop = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            canDrop = true;
        }
        else
        {
            canDrop = false;
        }
    }
}
