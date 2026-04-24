using System;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] Transform[] cardPos;
    [SerializeField] GameObject[] handCards;

    private static Hand instance = null;
    public static Hand Instance => instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void InitializeHand(GameObject[] cards)
    {
        handCards = cards;

        for (int i = 0; i < 4; i++)
        {
            GameObject card = Instantiate(handCards[i], cardPos[i].position, cardPos[i].rotation);
            Card cardComponent = card.GetComponent<Card>();

            if (cardComponent != null)
            {
                cardComponent.handPosId = i;
            }

            SetCardPosInHand(i, card);
        }
    }

    public void RemoveFromHand(int index)
    {
        handCards[index] = Deck.Instance.GetCardFromDeck();

        GameObject card = Instantiate(handCards[index], cardPos[index].position, cardPos[index].rotation);
        Card cardComponent = card.GetComponent<Card>();

        if (cardComponent != null)
        {
            cardComponent.handPosId = index;
        }

        SetCardPosInHand(index, card);
        Invoke("GoBackToHeadView", 1f);
    }

    public void GoBackToHeadView()
    {
        CamController.Instance.GoToHeadView();
    }

    private void SetCardPosInHand(int index, GameObject card)
    {
        iTween.Stop(card);
        card.transform.position = cardPos[index].position;
        card.transform.rotation = cardPos[index].rotation;
    }
}