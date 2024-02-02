using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMainScene : MonoBehaviour
{
    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainGame");
    }
}