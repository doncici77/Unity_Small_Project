using UnityEngine;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{

    public Text one;  // UI Text ������Ʈ
    public Text two;
    public float aSpeed = 0.01f;
    private bool increasing = true;  // true�� ���İ� ����, false�� ����
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

        one.color = color1; // UI �ؽ�Ʈ�� ���� ���� ����
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
