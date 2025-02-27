using UnityEngine;

public class DropCheck : MonoBehaviour
{
    public static bool canDrop = false;



    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            Debug.Log("¶³¾îÁü À§Çè");
            canDrop = true;
        }
    }
}
