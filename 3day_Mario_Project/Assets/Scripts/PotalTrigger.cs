using UnityEngine;

public class PotalTrigger : MonoBehaviour
{
    public Transform potalSpawnPos;
    public Sprite nextBackground;
    public int cameraNo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && 
            (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)))
        {
            collision.gameObject.transform.position = potalSpawnPos.position;
            CameraManager.background.sprite = nextBackground;
            CameraManager.cameraNo = cameraNo;
        }
    }
}
