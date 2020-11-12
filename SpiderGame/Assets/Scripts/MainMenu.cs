using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject spider;
    public GameObject mainMenu;
    //public 
    public GameObject levelSelMenu;
    public GameObject[] levelButton = new GameObject [3];
    //public 
    GameObject pauseMenu;
    int currentLevel = 1;
    //string filePath = @"C:\Documents\save.txt";
    readonly string myFilePath = @"D:\Documents_D\save.txt";

    // Start is called before the first frame update
    void Start()
    {
        //set up the main menu, otherwise set up the pause menu
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            //mainMenu = GameObject.Find("Main Menu");
            //levelSelMenu = GameObject.Find("LevelSelect");
            mainMenu.SetActive(true);
            levelSelMenu.SetActive(false);
        }
        else if (SceneManager.GetActiveScene().name != "WinScreen")
        {
            pauseMenu = GameObject.Find("PauseMenu");
            pauseMenu.SetActive(false);
        }
        
        currentLevel = int.Parse(System.IO.File.ReadAllText(myFilePath));
        print(currentLevel);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu" || SceneManager.GetActiveScene().name != "WinScreen")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseMenu.SetActive(true);
                spider.GetComponent<SpiderMove>().canMove = false;
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(currentLevel);
    }

    public void OpenLevelSelect()
    {
        mainMenu.SetActive(false);
        levelSelMenu.SetActive(true);

        for (int i = 0; i < 3; ++i)
        {
            int levelIndex = i;
            levelIndex++;
            if (levelIndex > currentLevel)
            {
                levelButton[(i)].GetComponent<Button>().interactable = false;
            }
        }
    }

    public void Select0()
    {
        SceneManager.LoadScene(1);
    }

    public void Select1()
    {
        SceneManager.LoadScene(2);
    }

    public void Select2()
    {
        SceneManager.LoadScene(3);
    }

    public void BackToMain()
    {
        levelSelMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void MainFromWin()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Resume()
    {
        spider.GetComponent<SpiderMove>().canMove = true;
        pauseMenu.SetActive(false);
    }
}
