using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // 카메라의 스크롤 제한 값
    public static float left_limit = 0.0f;
    public static float right_limit = 16.0f;
    public float top_limit = 5.0f;
    public float bottom_limit = 0.0f;

    public float forceScrollSpeedX = 0.5f; // 1초간 움직일 X 방향의 거리
    public float forceScrollSpeedY = 0.5f; // 1초간 움직일 Y 방향의 거리

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
            // 플레이어 탐색
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player == null)
            {
                return;
            }

            float x = player.transform.position.x; // 플레이어 좌표 기준
            float y = player.transform.position.y; // 플레이어 좌표 기준
            float z = transform.position.z; // 카메라 좌표 기준

            // 가로 방향에 대한 동기화
            if (x < left_limit)
            {
                x = left_limit;
            }
            else if (x > right_limit)
            {
                x = right_limit;
            }

            // 세로 방향에 대한 동기화
            if (y < bottom_limit)
            {
                y = bottom_limit;
            }
            else if (y > top_limit)
            {
                y = top_limit;
            }

            // 현제의 카메라 위티를 Vector3로 표현
            Vector3 vector3 = new Vector3(x, y, z);

            // 카메라의 위치를 설정한 값으로 적용
            transform.position = vector3;
        }
        else if(cameraNo == 1)
        {
            gameObject.transform.position = new Vector3(0, -23);
        }
    }
}
