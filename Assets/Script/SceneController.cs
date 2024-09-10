using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController: MonoBehaviour
{

    public int NowStage_level;
    public int MaxStage_level = 3;
    public Text NowStage_Text;



    public void Update()
    {
        NowStage_Text.text = $"스테이지 {NowStage_level}  /  {MaxStage_level}";




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
        SceneManager.LoadScene($"GameScene{NowStage_level}");
    


    }



}
