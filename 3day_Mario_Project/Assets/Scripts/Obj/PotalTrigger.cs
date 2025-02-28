using UnityEngine;

public class PotalTrigger : MonoBehaviour
{
    public enum Dir
    {
        Down,
        Right
    }

    public Transform potalSpawnPos;
    public Sprite nextBackground;
    public int cameraNo;
    bool canPotal = false;
    public Dir dir;
    Transform player;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    private void Update()
    {
        if(canPotal)
        {
            if(dir == Dir.Down)
            {
                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    SetPotal();
                    audioSource.Play();
                }
            }
            else if(dir == Dir.Right)
            {
                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    SetPotal();
                    audioSource.Play();
                }
            }
        }
    }

    void SetPotal()
    {
        player.position = potalSpawnPos.position;
        CameraManager.background.sprite = nextBackground;
        CameraManager.cameraNo = cameraNo;
        canPotal = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            canPotal = true;
            player = collision.gameObject.GetComponent<Transform>();
        }
    }
}
