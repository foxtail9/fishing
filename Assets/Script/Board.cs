using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Board : MonoBehaviour
{
    public GameObject card;

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
        arr = arr.OrderBy(x => Random.Range(0f, 7f)).ToArray();

        for (int i = 0; i < 16; i++)
        {
            GameObject go = Instantiate(card, this.transform);

            float x = (i / 4) * 1.4f - 2.1f;
            float y = (i % 4) * 1.4f - 3.0f;

            go.transform.position = new Vector2(0f, -0.91f);
            Vector2 targetPosition = new Vector2(x, y);
            cardList.Add(go, targetPosition);
            go.GetComponent<Card>().Setting(arr[i]);
        }
        GameManager.Instance.cardCount = arr.Length;
    }

    IEnumerator DistributeCard(float waitSeconds)
    {
        yield return new WaitForSeconds(0.5f);
        foreach (KeyValuePair<GameObject, Vector2> card in cardList)
        {
            GameObject generatedCard = card.Key;
            Vector2 cardTargetPosition = card.Value;
            generatedCard.transform.position = Vector2.Lerp(generatedCard.transform.position, cardTargetPosition, Time.deltaTime * 5f);
            yield return new WaitForSeconds(waitSeconds);
        }
        yield return new WaitForSeconds(0.7f);
        isCardDistributed = true;
    }
}