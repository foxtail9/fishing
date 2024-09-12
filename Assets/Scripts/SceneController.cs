using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SceneController : MonoBehaviour
{

    public int min_Level = 1;
    public int NowStage_level = 1;
    public int MaxStage_level = 3;
    public Text NowStage_Text;

    public Text UnlockStage;

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
        //�������� 0�̸� ����, 1�̸� ������ Ŭ���� �Ѱ�
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
        NowStage_Text.text = $"�������� {NowStage_level}  /  {MaxStage_level}";
        UnlockStage.text = NowStage_level.ToString();
    }


   

    public void Left_Stage(){ 
    
        if(NowStage_level > min_Level)
            NowStage_level -= 1;
        else
            NowStage_level = MaxStage_level;

        UnlockState();
    }

    public void Right_Stage() {

        if (NowStage_level < MaxStage_level)
            NowStage_level += 1;
        else
            NowStage_level = min_Level;

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