using TMPro;
using UnityEngine;

public class BackGroudSound : MonoBehaviour
{
    AudioSource audioSource;
    PlayerMove player;
    public AudioClip gameClearSound;
    bool soundSet = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
    }

    void Update()
    {
        if (player.goGoal && soundSet)
        {
            audioSource.loop = false;
            audioSource.clip = gameClearSound;
            audioSource.Play();
            soundSet = false;
        }
    }
}
