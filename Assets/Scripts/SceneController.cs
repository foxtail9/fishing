using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SceneController : MonoBehaviour
{
    public GameObject StageLev;
    public GameObject StageLock;
    public Text NowStage_Text;
    public Text UnlockStage;

    public int NowStage_level = 1;
    public int MaxStage_level = 3;

    bool isUnLock;

    public enum Achive { GameScene, GameScene1, GameScene2, GameScene3, GameScene4 }
    public Achive[] achives;


    void Awake()
    {
        achives = (Achive[])Enum.GetValues(typeof(Achive));

        if (!PlayerPrefs.HasKey("MyData"))
            init();
    }


    void init()
    {
        //스테이지 0이면 잠긴것, 1이면 이전에 클리어 한것
        PlayerPrefs.SetInt("MyData", 1);

        PlayerPrefs.SetInt(achives[1].ToString(), 1);

        for (int i = 2; i <= MaxStage_level; i++)
        {
            PlayerPrefs.SetInt(achives[i].ToString(), 0);
        }
        PlayerPrefs.Save();
    }

    public void Update()
    {
        NowStage_Text.text = $"스테이지 {NowStage_level}  /  {MaxStage_level}";
        UnlockStage.text = NowStage_level.ToString();
    }

    public void Left_Stage()
    {
        if (NowStage_level > 1)
            NowStage_level -= 1;
        else
            NowStage_level = 3;

        UnlockState();
    }

    public void Right_Stage()
    {
        if (NowStage_level < 3)
            NowStage_level += 1;
        else
            NowStage_level = 1;

        UnlockState();
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene($"GameScene{NowStage_level}");
    }

    public void UnlockState()
    {
        String ahiveName = achives[NowStage_level].ToString();

        isUnLock = PlayerPrefs.GetInt(ahiveName) == 1;

        if (!isUnLock)
        {
            StageLock.SetActive(true);
            StageLev.SetActive(false);
        }
        else
        {
            StageLock.SetActive(false);
            StageLev.SetActive(true);
        }
    }
}