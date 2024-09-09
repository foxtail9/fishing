using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Text timeTxt;
    float time = 0.0f;
    public Card firstCard;
    public Card secondCard;
    public int cardCount = 0;
    public GameObject endTxt;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        if (time >= 30.0f)
        {
            endTxt.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }
    public void checkMatched()
    {
        if(firstCard.index == secondCard.index)
        {
            firstCard.DestroyCard();
            secondCard.DestroyCard();
        }
        else
        {
            firstCard.CloseCard();
            secondCard.CloseCard();
        }
        firstCard = null;
        secondCard = null;
    }


    public void isMatched()
    {
        if(firstCard.index == secondCard.index)
        {
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            cardCount -= 2;
            if(cardCount == 0)
            {
                endTxt.SetActive(true);
                Time.timeScale = 0.0f;
            }
        }
        else
        {
            firstCard.CloseCard();
            secondCard.CloseCard();
        }
        firstCard = null;
        secondCard = null;
    }


}
