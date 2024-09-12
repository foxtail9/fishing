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
        
        int stageLevel = int.Parse(tempStr);

        PlayerPrefs.SetInt($"GameScene{stageLevel + 1}", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("StageSelect");
    }

    public void SelectStage()
    {
        SceneManager.LoadScene("StageSelect");
    }
}
