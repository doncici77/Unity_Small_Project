using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public Text playTimeText;
    public Text coinCountText;
    public Text stageText;
    public static int coin = 0;
    float playTime = 0;

    void Start()
    {
        
    }

    void Update()
    {
        playTimeText.text = $"PLAY TIME \n{(int)playTime}";
        playTime += Time.deltaTime;

        coinCountText.text = $"COIN X {coin}";
    }
}
