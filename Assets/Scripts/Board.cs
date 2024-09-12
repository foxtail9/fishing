using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class Board : MonoBehaviour
{

    public int Max_Stage = 3;
    public int Stage_CardNum;
    public int stageLevel;

    public GameObject card;

    [HideInInspector]
    public bool isCardDistributed;
      
    Dictionary<GameObject, Vector2> cardList = new Dictionary<GameObject, Vector2>();


    void Start()
    {
        isCardDistributed = false;
        GenerateCard();
    }

    void Update()
    {
        if (isCardDistributed == false)
        {
            StartCoroutine(DistributeCard(0.05f));
        }
    }

    public void GenerateCard()
    {

        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        StageCardNum();

        Debug.Log(stageLevel);
        int[] stage_arr = arr.Take(Stage_CardNum).ToArray();
        
        stage_arr = stage_arr.OrderBy(x => Random.Range(0f, Stage_CardNum)).ToArray();



        for (int i = 0; i < Stage_CardNum; i++)
        {
            GameObject go = Instantiate(card, this.transform);
            go.transform.position = new Vector2(0f, -0.91f);

            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i / 4) * 1.4f - 3.0f;

            Vector2 targetPosition = new Vector2(x, y);
            cardList.Add(go, targetPosition);

            go.GetComponent<Card>().Setting(arr[i]);
        }
        GameManager.Instance.cardCount = stage_arr.Length;
    }

    IEnumerator DistributeCard(float waitSeconds)
    {
        yield return new WaitForSeconds(0.5f);
        foreach (KeyValuePair<GameObject, Vector2> card in cardList)
        {
            GameObject generatedCard = card.Key;
            Vector2 cardTargetPosition = card.Value;
            generatedCard.transform.position = Vector2.Lerp(generatedCard.transform.position,
                                                            cardTargetPosition,
                                                            Time.deltaTime * 5f);
            yield return new WaitForSeconds(waitSeconds);
        }
        yield return new WaitForSeconds(0.7f);
        isCardDistributed = true;
    }

    public void StageCardNum()
    {

        Scene scene;
        scene = SceneManager.GetActiveScene();
        string stageName = scene.name;
        string tempStr = Regex.Replace(stageName, @"\D", "");
        stageLevel = int.Parse(tempStr);

        if (stageLevel > Max_Stage)
            stageLevel = Max_Stage;

        Stage_CardNum = 1 << stageLevel +1;




    }


}