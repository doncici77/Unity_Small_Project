using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine("LoadScene");
        }
    }

    void Load()
    {
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1);
        Load();
    }
}
