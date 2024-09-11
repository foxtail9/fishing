using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class Retrybutton : MonoBehaviour
{
    Scene scene;

    public void Retry()
    {
        scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void GoStage()
    {
        scene = SceneManager.GetActiveScene();
        string stageName = scene.name;

        string tempStr = Regex.Replace(stageName, @"\D", "");
        
        int rstInt = int.Parse(tempStr);

        PlayerPrefs.SetInt($"GameScene{rstInt + 1}", 1);
        PlayerPrefs.Save();

        if (rstInt < 3)
        {
            SceneManager.LoadScene($"GameScene{rstInt + 1}");
        }
    }

    public void SelectStage()
    {
        SceneManager.LoadScene("StageSelect");
    }
}
