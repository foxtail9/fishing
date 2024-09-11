using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public GameObject front;
    public GameObject back;
    public SpriteRenderer frontImage;
    public Animator anim;

    AudioSource audioSource;
    public AudioClip clip;

    public int index = 0;

    public void Setting(int idx)
    {
        index = idx;
        frontImage.sprite = Resources.Load<Sprite>($"rtan{index}");
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OpenCard()
    {

        if (GameManager.Instance.secondCard != null) return;

        audioSource.PlayOneShot(clip);

        anim.SetBool("isOpen", true);
        Invoke("OpenCardInvoke", 0.1f);
        
        if(GameManager.Instance.firstCard == null)
        {
            GameManager.Instance.firstCard = this;
        }
        else
        {
            GameManager.Instance.secondCard = this;
            GameManager.Instance.isMatched();
        }
    }

    void OpenCardInvoke()
    {
        front.SetActive(true);
        back.SetActive(false);
    }

    public void DestroyCard()
    {
        Invoke("DestroyCardInvoke", 1.0f);
    }

    void DestroyCardInvoke()
    {
        Destroy(gameObject);
    }

    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 1.0f);
    }

    void CloseCardInvoke()
    {
        anim.SetBool("isOpen", false);
        front.SetActive(false);
        back.SetActive(true);
    }
}
