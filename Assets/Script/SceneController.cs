using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SceneController: MonoBehaviour
{

    public int NowStage_level = 1;
    public int MaxStage_level = 3;
    public Text NowStage_Text;

    public Text UnlockStage;

    public GameObject StageLev;
    public GameObject StageLock;

    int isUnLock;

    enum Achive { unlock2, unlock3, unlock4}
    Achive[] achives;



    void Awake()
    {
        achives = (Achive[])Enum.GetValues(typeof(Achive));

        if (!PlayerPrefs.HasKey("MyData"))
            init();

    }


    void Start()
    {
    
        

    }



    void init()
    {

        //스테이지 0이면 잠긴것, 1이면 이전에 클리어 한것
        PlayerPrefs.SetInt("MyData", 1);

        foreach (Achive achive in achives)
        {
            PlayerPrefs.SetInt(achive.ToString(), 0);
        
        }


    }
    




    public void Update()
    {
        NowStage_Text.text = $"스테이지 {NowStage_level}  /  {MaxStage_level}";
        UnlockStage.text = NowStage_level.ToString();

    }


    
 


    public void Left_Stage(){ 
    
        if(NowStage_level > 1)
            NowStage_level -= 1;
        else
            NowStage_level = 3;
    
    }

    public void Right_Stage() {

        if (NowStage_level < 3)
            NowStage_level += 1;

        else
            NowStage_level = 1;

    }


    public void LoadLevel()
    {
        SceneManager.LoadScene($"GameScene {NowStage_level}");
    


    }


    public void UnlockState()
    {



        String ahiveName = achives[NowStage_level].ToString();
        isUnLock = PlayerPrefs.GetInt(ahiveName);

        if (isUnLock == 0)
        {
            StageLock.SetActive(false);
            StageLev.SetActive(true);

        }

        else
        {
            StageLock.SetActive(true);
            StageLev.SetActive(false);

        }


    }



    // 게임 클리어 시 정보를 저장하는 내용 만들어야.
    

}
