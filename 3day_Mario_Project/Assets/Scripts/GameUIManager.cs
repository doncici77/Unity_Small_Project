using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public Text playTimeText;
    public Text coinCountText;
    public Text stageText;
    public static int coin = 0;
    float playTime = 0;

    public Text clearText1;
    public Text clearText2;
    public static bool goalState = false;

    void Update()
    {
        playTimeText.text = $"PLAY TIME \n{(int)playTime}";
        playTime += Time.deltaTime;

        coinCountText.text = $"COIN X {coin}";

        if (goalState)
        {

            Color color1 = clearText1.color;
            Color color2 = clearText2.color;

            color1.a += 0.004f;
            color2.a += 0.004f;

            clearText1.color = color1; // UI 텍스트의 실제 색상 변경
            clearText2.color = color2;

            if(color1.a >= 1)
            {
                StartCoroutine("GameOff");
            }
        }

        IEnumerator GameOff()
        {
            yield return new WaitForSeconds(2);
            Application.Quit();
        }
    }
}
