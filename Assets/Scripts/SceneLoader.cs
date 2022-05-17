using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RetryLvl1()
    {
        SceneManager.LoadScene("Level 1");
    }

    //public void SelectLevel()
    //{
    //    SceneManager.LoadScene("LevelMenu");
    //}

    //public void Level_1()
    //{
    //    SceneManager.LoadScene("SampleScene");
    //}

    //public void Level_2()
    //{
    //    SceneManager.LoadScene("Level2");
    //}

    //public void Level_3()
    //{
    //    SceneManager.LoadScene("Level3");
    //}

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
}

