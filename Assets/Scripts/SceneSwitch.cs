using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public void SceneToLoad(int index)
    {
        SceneManager.LoadScene(index);
        Time.timeScale = 1;
    }
}
