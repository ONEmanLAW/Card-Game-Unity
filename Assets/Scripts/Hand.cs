using System;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] Transform[] cardPos;
    [SerializeField] GameObject[] handCards;

    private static Hand instance = null;
    public static Hand Instance => instance;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void InitializeHand(GameObject[] cards) {
        handCards = cards;

        for (int i = 0; i < 4; i++) {
            GameObject card = Instantiate(handCards[i]);
            card.GetComponent<Card>().handPosId = i;
            SetCardPosInHand(i, card);
        }
    }

    public void RemoveFromHand(int index)
    {
        CamController.Instance.GoToHeadView();
        handCards[index] = Deck.Instance.GetCardFromDeck();

        GameObject card = Instantiate(handCards[index]);
        
        SetCardPosInHand(index, card);
    }

    private void SetCardPosInHand(int index, GameObject card)
    {
        iTween.MoveTo(card, cardPos[index].position, .5f);
        iTween.RotateTo(card, cardPos[index].rotation.eulerAngles, .5f);
    }
}
