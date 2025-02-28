using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Load();
        }
    }

    void Load()
    {
        SceneManager.LoadScene(sceneName);
    }
}
