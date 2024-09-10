using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Board : MonoBehaviour
{
    public Transform cards;
    public GameObject card;
    public GameObject cardMix;

    public bool isCardDistributed;
    float speed = 0.1f;

    Dictionary<GameObject, Vector2> cardList = new Dictionary<GameObject, Vector2>();

    // Start is called before the first frame update
    void Start()
    {
        isCardDistributed = false;
        GenerateCard();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCardDistributed == false)
        {
            StartCoroutine(DistributeCard(0.03f));
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
        foreach (KeyValuePair<GameObject, Vector2> card in cardList)
        {
            GameObject generatedCard = card.Key;
            Vector2 cardTargetPosition = card.Value;
            generatedCard.transform.position = Vector2.Lerp(generatedCard.transform.position, cardTargetPosition, 0.1f);
            yield return new WaitForSeconds(waitSeconds);
        }
        isCardDistributed = true;
    }
}
