using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    private GameObject[] enemyBox;
    private void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        enemyBox = GameObject.FindGameObjectsWithTag("Enemy");
        print("ìGÇÃêîÅF" + enemyBox.Length);
        if (enemyBox.Length == 1)
        {
            gameObject.SetActive(true);
        }
        else if(enemyBox.Length == 0)
        {
            SceneManager.LoadScene("GameClearScene");
        }

    }
    
}