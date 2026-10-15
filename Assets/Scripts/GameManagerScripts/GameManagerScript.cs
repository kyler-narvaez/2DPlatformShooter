using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    //private bool isPaused;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject skillTreeMenu;

    void Update()
    {
        CheckGameOver();

        CheckIfGamePaused();
    }

    public void ShowSkillTree()
    {
        skillTreeMenu.SetActive(true);
        skillTreeMenu.GetComponent<PlayerSkillTree>().SetValues();
    }

    public void HideSkillTree()
    {
        skillTreeMenu.SetActive(false);
    }

    private void CheckIfGamePaused()
    {
        if (Time.timeScale == 0f)
        {
            pauseMenu.SetActive(true);
        }
        else
        {
            pauseMenu.SetActive(false);
        }
    }

    //Check if player exists
    private void CheckGameOver()
    {
        if (GameObject.Find("Player") == null)
        {
            gameOverMenu.SetActive(true);
            GameObject[] enemyKiller = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemyKiller)
            {
                Destroy(enemy);
            }
        }
    }

    
}
