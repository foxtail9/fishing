using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject gameover;
    public GameObject endTxt;
    public GameObject board;
    public GameObject fail;

    public Board thisBoard;
    public Card firstCard;
    public Card secondCard;

    public Text timeTxt;

    AudioSource audioSource;
    public AudioClip clip;

    float time = 0.0f;
    public int cardCount = 0;
    bool isfail = false;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        Time.timeScale = 1.0f;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (thisBoard.isCardDistributed == true)
        {
            time += Time.deltaTime;
        }
        timeTxt.text = time.ToString("N2");

        if (time >= 30.0f)
        {
            gameover.SetActive(true);
            Time.timeScale = 0.0f;
            board.SetActive(false);
        }
    }

    public void isMatched()
    {
        if (firstCard.index == secondCard.index)
        {
            audioSource.PlayOneShot(clip);
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            cardCount -= 2;

            if (cardCount == 0)
            {
                endTxt.SetActive(true);
                Time.timeScale = 0.0f;
                board.SetActive(false);
            }
        }
        else
        {
            isfail = true;

            if (isfail)
            {
                fail.SetActive(true);
                StartCoroutine(DisableFailAfterDelay(1.0f));

                isfail = false;
            }

            firstCard.CloseCard();
            secondCard.CloseCard();
        }

        firstCard = null;
        secondCard = null;
    }

    private IEnumerator DisableFailAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        fail.SetActive(false);
    }
}
