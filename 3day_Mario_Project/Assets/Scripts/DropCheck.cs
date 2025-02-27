using UnityEngine;

public class DropCheck : MonoBehaviour
{
    public bool canDrop = false;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            canDrop = true;
        }
    }
}
