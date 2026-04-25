using UnityEngine;

public class ComputerAI : MonoBehaviour
{
    [SerializeField] GameObject[] cards;
    int[] tablePos = { 0, 1, 2, 3 };
    int turn = 1;

    private static ComputerAI instance = null;
    public static ComputerAI Instance => instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
            instance = this;
        DontDestroyOnLoad(gameObject);
    }

    
    void Shuffle(int[] i) {
        for (int t = 0; t < i.Length; t++) {
            int tep = i[t];
            int r = Random.Range(t, i.Length);
            i[t] = i[r];
            i[r] = tep;
        }
    }

    int CountCards(GameObject[] table) {
        int count = 0;
        foreach (GameObject card in table) {
            if (card != null) { 
                Card c = card.GetComponent<Card>();
                if(c.cardObject.cost < 2) {
                    count++;
                }
            }
        }
        return count;
    }

    public void PlayCard()
    {
        int count = 0;

        // melange une fois sur deux
        if (turn % 2 == 0) {
            Shuffle(tablePos);
        } else { 
            count = CountCards(Board.Instance.computerCards);
        }
        
        int i = Random.Range(0, 1); // Dragonfly ou Squirrel

        // Jouer une carte forte
        if (count >= 2 && i == 0) {
            int nbSacrifices = 0;

            foreach (var pos in tablePos) { 
                if (Board.Instance.computerCards[pos] != null && nbSacrifices < 2) {
                    Destroy(Board.Instance.computerCards[pos]);
                    Board.Instance.computerCards[pos] = null;
                    nbSacrifices++;
                    if (nbSacrifices == 2) { 
                        Transform cardDest = GameObject.Find("CompPos" + pos).transform;
                        GameObject card = Instantiate(cards[3]); // dragon
                        Board.Instance.computerCards[pos] = card;
                        PlaySelectedCard(pos, cardDest, card);
                        break;
                    }
                }  
            }
            count = 0;
        } else {
            foreach (var pos in tablePos) {
                Transform cardDest = GameObject.Find("CompPos" + pos).transform;

                if (Board.Instance.computerCards[pos] == null)
                {
                    GameObject card = Instantiate(cards[i]);
                    Board.Instance.computerCards[pos] = card;
                    PlaySelectedCard(pos, cardDest, card);
                    break;
                }
                else
                {
                    if (Random.Range(0, 100) >= 50)
                    {
                        Destroy(Board.Instance.computerCards[pos]);
                        Board.Instance.computerCards[pos] = null;

                        GameObject card = Instantiate(cards[2]); // Wolf
                        Board.Instance.computerCards[pos] = card;
                        PlaySelectedCard(pos, cardDest, card);
                        break;
                    }
                }
            }     
        }
        Invoke("RingBell", .5f);
    }

    private static void PlaySelectedCard(int pos, Transform cardDest, GameObject card) {
        iTween.MoveTo(card, cardDest.position, .5f);
        iTween.RotateTo(card, cardDest.rotation.eulerAngles, .25f);
        card.GetComponent<SpriteRenderer>().sortingOrder = 1;
        card.GetComponent<Card>().boardPosId = pos;
    }

    public void RingBell() {
        turn++;
        Bell.Instance.ChangeGamePhase();
    }
}
