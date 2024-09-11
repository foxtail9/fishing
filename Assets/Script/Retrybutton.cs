using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Retrybutton : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void SelectStage()
    {
        SceneManager.LoadScene("StageSelect");
    }
}
