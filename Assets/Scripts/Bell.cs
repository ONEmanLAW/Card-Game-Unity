using UnityEngine;
using System.Collections;

public class Bell : MonoBehaviour
{
    [SerializeField] AudioClip bellClip;

    private static Bell instance = null;
    public static Bell Instance => instance;

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

    private void OnMouseUp()
    {
        if (GameManager.Instance == null) return;
        if (!GameManager.Instance.isPlayerTurn) return;
        if (GameManager.Instance.gameEnded) return; // no bell sound

        ChangeGamePhase();
    }

    public void ChangeGamePhase() {

        AudioManager.Instance.PlaySFX(1); // bell
        GameManager.Instance.nbActions = 0;

        if (GameManager.Instance.isPlayerTurn)
        {
            GameManager.Instance.isPlayerTurn = false;
            Invoke(nameof(StartBattlePhase1), 0.25f);
        }
        else
        {
            GameManager.Instance.isPlayerTurn = true;
            StartCoroutine(StartCompBattlePhase());
        }
    }

    public IEnumerator StartCompBattlePhase()
    {
        yield return new WaitForSeconds(0.5f);

        foreach (GameObject card in Board.Instance.computerCards)
        {
            if (card == null) continue;

            iTween.MoveFrom(card, card.transform.position + Vector3.forward / 2, 1f);
            int idToFight = card.GetComponent<Card>().boardPosId;

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
        }

        GameManager.Instance.sacrifices = 0;
        GameManager.Instance.nbActions = 0;
        GameManager.Instance.selectedCard = null;
        GameManager.Instance.sacrificeSummonUsedThisTurn = false;

        CamController.Instance.GoToHeadView();
    }

    public void StartBattlePhase1()
    {
        CamController.Instance.GoToBoardView();
        StartCoroutine(StartBattlePhase2());
    }

    public IEnumerator StartBattlePhase2()
    {
        yield return new WaitForSeconds(0.5f);

        foreach (GameObject card in Board.Instance.playerCards)
        {
            if (card == null) continue;

            int idToFight = card.GetComponent<Card>().boardPosId;
            iTween.MoveFrom(card, card.transform.position - Vector3.forward / 2, 1f);

            if (Board.Instance.computerCards[idToFight] != null)
            {
                if (card.GetComponent<Card>().cardObject.atk >= Board.Instance.computerCards[idToFight].GetComponent<Card>().cardObject.def)
                {
                    Destroy(Board.Instance.computerCards[idToFight]);
                    Board.Instance.computerCards[idToFight] = null;
                }
            }
            else
            {
                GameManager.Instance.HitComputer(card.GetComponent<Card>().cardObject.atk);
                yield return new WaitForSeconds(0.5f);
            }
        }

        Invoke(nameof(PlayCompCard), 1f);
    }

    public void PlayCompCard()
    {
        ComputerAI.Instance.PlayCard();
    }
}