using UnityEngine;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{

    public Text one;  // UI Text 오브젝트
    public Text two;
    public float aSpeed = 0.01f;
    private bool increasing = true;  // true면 알파값 증가, false면 감소
    bool soundAble = true;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Color color1 = one.color;
        Color color2 = two.color;

        if (increasing)
        {
            color1.a += aSpeed;
            color2.a += aSpeed;
            if (color1.a >= 0.9f)
            {
                increasing = false;
            }
        }
        else
        {
            color1.a -= aSpeed;
            color2.a -= aSpeed;
            if (color1.a <= 0.1f)
            {
                increasing = true;
            }
        }

        one.color = color1; // UI 텍스트의 실제 색상 변경
        two.color = color2;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(soundAble)
            {
                audioSource.Play();
                soundAble = false;
            }
        }
    }
}
