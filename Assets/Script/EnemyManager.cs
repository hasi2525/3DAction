using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    private GameObject[] enemyBox;
    void Update()
    {
        enemyBox = GameObject.FindGameObjectsWithTag("Enemy");
        print("“G‚Ì”F" + enemyBox.Length);
        if (enemyBox.Length == 0)
        {
            SceneManager.LoadScene("GameClearScene");
        }

    }
    
}