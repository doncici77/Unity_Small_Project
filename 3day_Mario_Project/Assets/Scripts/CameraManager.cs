using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // ī�޶��� ��ũ�� ���� ��
    public static float left_limit = 0.0f;
    public static float right_limit = 16.0f;
    public float top_limit = 5.0f;
    public float bottom_limit = 0.0f;

    public float forceScrollSpeedX = 0.5f; // 1�ʰ� ������ X ������ �Ÿ�
    public float forceScrollSpeedY = 0.5f; // 1�ʰ� ������ Y ������ �Ÿ�

    public static SpriteRenderer background;
    public static int cameraNo = 0;

    void Start()
    {
        background = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        if (cameraNo == 0)
        {
            // �÷��̾� Ž��
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player == null)
            {
                return;
            }

            float x = player.transform.position.x; // �÷��̾� ��ǥ ����
            float y = player.transform.position.y; // �÷��̾� ��ǥ ����
            float z = transform.position.z; // ī�޶� ��ǥ ����

            // ���� ���⿡ ���� ����ȭ
            if (x < left_limit)
            {
                x = left_limit;
            }
            else if (x > right_limit)
            {
                x = right_limit;
            }

            // ���� ���⿡ ���� ����ȭ
            if (y < bottom_limit)
            {
                y = bottom_limit;
            }
            else if (y > top_limit)
            {
                y = top_limit;
            }

            // ������ ī�޶� ��Ƽ�� Vector3�� ǥ��
            Vector3 vector3 = new Vector3(x, y, z);

            // ī�޶��� ��ġ�� ������ ������ ����
            transform.position = vector3;
        }
        else if(cameraNo == 1)
        {
            gameObject.transform.position = new Vector3(0, -23);
        }
    }
}
