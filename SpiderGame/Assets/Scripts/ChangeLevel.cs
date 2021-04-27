using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    private int currentScene;
    readonly string myFilePath = @"D:\Documents_D\save.txt"/*@"C:\Documents\SpiderSave\save.txt"*/;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Spider")
        {
            //move workward a level
            currentScene++;

            if (currentScene < 3)
            {
                File.WriteAllText(myFilePath, string.Empty);
                //save
                using (StreamWriter saveFile = new StreamWriter(myFilePath))
                {
                    saveFile.Write(currentScene);
                    print("saved");
                }
            }

            //go to next level
            SceneManager.LoadScene(currentScene);
        }
    }
}
