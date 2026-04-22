using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Deck : MonoBehaviour
{
    [SerializeField] GameObject[] cards;
    [SerializeField] Queue<GameObject> playerDeck = new Queue<GameObject>();
    int cardsInDeck = 40;

    private static Deck instance = null;
    public static Deck Instance => instance;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        InitGame();
    }

    private void InitGame() {
        for (int i = 0; i < cardsInDeck; i++) {
            playerDeck.Enqueue(cards[UnityEngine.Random.Range(0, cards.Length)]);
        }

        GameObject[] initialCards = new GameObject[4];

        for(int i = 0; i < 4; i++) {
            initialCards[i] = playerDeck.Dequeue();
        }

        Hand.Instance.InitializeHand(initialCards);

    }

    // a mettre en place
    private void ResetHand() {
        GameObject[] initialCards = new GameObject[4];

        for(int i = 0; i < 4; i++) {
            initialCards[i] = playerDeck.Dequeue();
        }

        Hand.Instance.InitializeHand(initialCards);
    }

    public GameObject GetCardFromDeck() {
        return playerDeck.Dequeue();
    }
}
