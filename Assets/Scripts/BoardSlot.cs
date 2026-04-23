using System.Runtime.CompilerServices;
using UnityEngine;

public class BoardSlot : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] Color normalColor;
    [SerializeField] Color hoverColor;
    [SerializeField] int slotId;
    bool hasCard = false;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseEnter() {
        if (GameManager.Instance.selectedCard == null) return;
        spriteRenderer.color = hoverColor;
    }
    private void OnMouseExit() {
        spriteRenderer.color = normalColor;
    }

    private void OnMouseUp()
    {
        if (GameManager.Instance == null) return;
        if (GameManager.Instance.selectedCard == null) return;

        GameObject card = GameManager.Instance.selectedCard;

        Card cardComponent = card.GetComponent<Card>();
        if (cardComponent == null)
        {
            Debug.LogError("L'objet sélectionné n'a pas de composant Card.", card);
            return;
        }

        if (cardComponent.cardObject == null)
        {
            Debug.LogError("cardObject est null sur cette carte.", card);
            return;
        }

        if (cardComponent.cardObject.cost == 0)
        {
            if (Board.Instance.playerCards[slotId] != null) return;
            PlayCardOnSlot(card);
        }
        else if (cardComponent.cardObject.cost == 1)
        {
            if (Board.Instance.playerCards[slotId] == null) return;

            Destroy(Board.Instance.playerCards[slotId]);
            Board.Instance.playerCards[slotId] = null;
            PlayCardOnSlot(card);
        }
        else
        {
            if (Board.Instance.playerCards[slotId] == null) return;

            Destroy(Board.Instance.playerCards[slotId]);
            Board.Instance.playerCards[slotId] = null;

            if (GameManager.Instance.sacrifices == 1)
            {
                GameManager.Instance.sacrifices = 0;
                PlayCardOnSlot(card);
            }
            else
            {
                GameManager.Instance.sacrifices = 1;
            }
        }
    }

    private GameObject PlayCardOnSlot(GameObject card) {
        hasCard = true;
        iTween.MoveTo(card, transform.position, .5f);
        iTween.RotateTo(card, transform.rotation.eulerAngles, .25f);
        card.GetComponent<SpriteRenderer>().sortingOrder = 1;

        Hand.Instance.RemoveFromHand(card.GetComponent<Card>().handPosId);
        GameManager.Instance.selectedCard = null;
        card = null;
        return card;
    }
}

// Joueur qui veut jouer une carte a 2 sacrifices le jeu ne va pas lui empecher
// Ducoup joueur va ce retrouver bloquer a ne pas pouvoir faire un deuxieme sacrifice et ne pas pouvoir jouer sa carte.
// Mettre en place un reset de quelque seconde pour que le joueur puisse retirer ca carte si il se rend compte qu'il a fait une erreur.
