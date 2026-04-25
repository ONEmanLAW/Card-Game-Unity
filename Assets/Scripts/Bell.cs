using UnityEngine;
using System.Collections;

public class Bell : MonoBehaviour
{
    [SerializeField] AudioClip bellClip;

    private static Bell instance = null;
    public static Bell Instance => instance;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnMouseUp() {
        ChangeGamePhase();
    }

    public void ChangeGamePhase() {
        if (GameManager.Instance.isPlayerTurn) {
            GameManager.Instance.isPlayerTurn = !GameManager.Instance.isPlayerTurn;
            Invoke("StartBattlePhase1", 0.25f);
        }
        else {
            GameManager.Instance.isPlayerTurn = !GameManager.Instance.isPlayerTurn;
            StartCoroutine("StartCompBattlePhase");
        }
    }

    public IEnumerator StartCompBattlePhase() {
        yield return new WaitForSeconds(0.5f);
        int id = 0;

        foreach (GameObject card in Board.Instance.computerCards) {
            if (card  == null) continue;
            iTween.MoveFrom(card, card.transform.position + Vector3.forward / 2, 1f);
            int idToFight = card.GetComponent<Card>().boardPosId;

            // Attaque carte en face a faire
            if (Board.Instance.playerCards[idToFight] != null)
            {
                if (card.GetComponent<Card>().cardObject.atk >= Board.Instance.playerCards[idToFight].GetComponent<Card>().cardObject.def)
                {
                    Destroy(Board.Instance.playerCards[idToFight]);
                    Board.Instance.playerCards[idToFight] = null;
                }
            }
            else
            {
                GameManager.Instance.HitComputer(card.GetComponent<Card>().cardObject.atk);

                yield return new WaitForSeconds(0.5f);
            }
            Invoke("PlayMyTurnVoice", 0.5f);
        }
    }

    public void StartBattlePhase1() {
        CamController.Instance.GoToBoardView();
        StartCoroutine("StartBattlePhase2");
    }

    public IEnumerator StartBattlePhase2() {
        yield return new WaitForSeconds(0.5f);

        foreach (GameObject card in Board.Instance.playerCards) {
            if (card == null) continue;
            int idToFight = card.GetComponent<Card>().boardPosId;
            iTween.MoveFrom(card, card.transform.position - Vector3.forward / 2, 1f);

            // Attaque carte en face

            if (Board.Instance.computerCards[idToFight] != null) {
                if (card.GetComponent<Card>().cardObject.atk >= Board.Instance.computerCards[idToFight].GetComponent<Card>().cardObject.def) {
                    Destroy(Board.Instance.computerCards[idToFight]);
                    Board.Instance.computerCards[idToFight] = null;
                }
            } else {
                GameManager.Instance.HitComputer(card.GetComponent<Card>().cardObject.atk);

                yield return new WaitForSeconds(0.5f);
            }
            Invoke("PlayMyTurnVoice", 0.5f);

        }
    }

    public void PlayMyTurnVoice() {
        // jouer le son
        Invoke("PlayMyTurnVoice", 1f);
    }

    public void PlayCompCard() {

    }

}
