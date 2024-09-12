using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Board : MonoBehaviour
{
    public GameObject card;

    [HideInInspector]
    public bool isCardDistributed;

    Dictionary<GameObject, Vector2> cardList = new Dictionary<GameObject, Vector2>();

    public int Max_Stage = 3;
    public int Stage_CardNum;
    public int stageLevel;


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

        int[] stage_arr = arr.Take(Stage_CardNum).ToArray();
        stage_arr = stage_arr.OrderBy(x => Random.Range(0f, stageLevel * 2 - 1)).ToArray();

        for (int i = 0; i < Stage_CardNum; i++)
        {
            GameObject go = Instantiate(card, this.transform);
            go.transform.position = new Vector2(0f, -0.91f);

            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i / 4) * 1.4f - 3.0f;

            Vector2 targetPosition = new Vector2(x, y);
            cardList.Add(go, targetPosition);

            go.GetComponent<Card>().Setting(stage_arr[i]);
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
            if (Vector2.Distance(generatedCard.transform.position, cardTargetPosition) <= 0.01f)
            {
                generatedCard.transform.position = cardTargetPosition;
            }
            yield return new WaitForSeconds(waitSeconds);
        }
        yield return new WaitForSeconds(1.0f);
        isCardDistributed = true;
    }


    public void StageCardNum()
    {
        Scene scene;
        scene = SceneManager.GetActiveScene();
        string stageName = scene.name;
        string tempStr = Regex.Replace(stageName, @"\D", "");
        stageLevel = int.Parse(tempStr);

        Stage_CardNum = (stageLevel + 1) * 4;
    }
}