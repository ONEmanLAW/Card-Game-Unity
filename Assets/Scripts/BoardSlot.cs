using System.Runtime.CompilerServices;
using UnityEngine;

public class BoardSlot : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] Color normalColor;
    [SerializeField] Color hoverColor;
    [SerializeField] int slotId;
    bool hasCard = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseEnter()
    {
        if (GameManager.Instance == null) return;
        if (GameManager.Instance.selectedCard == null) return;

        spriteRenderer.color = hoverColor;
    }

    private void OnMouseExit()
    {
        spriteRenderer.color = normalColor;
    }

    private void OnMouseUp()
    {
        if (GameManager.Instance == null) return;
        if (Board.Instance == null) return;
        if (GameManager.Instance.selectedCard == null) return;

        //AudioManager.Instance.PlaySFX(1); // TODO : ajouter un son de pose de carte

        GameObject selectedCard = GameManager.Instance.selectedCard;
        Card selectedCardComponent = selectedCard.GetComponent<Card>();

        if (selectedCardComponent == null)
        {
            Debug.LogError("L'objet selected n'a pas de composant Card.", selectedCard);
            return;
        }

        if (selectedCardComponent.cardObject == null)
        {
            Debug.LogError("cardObject est null sur la carte.", selectedCard);
            return;
        }

        int cost = selectedCardComponent.cardObject.cost;
        GameObject cardOnSlot = Board.Instance.playerCards[slotId];

        Debug.Log("Slot " + slotId + " | cost = " + cost + " | sacrifices = " + GameManager.Instance.sacrifices + " | cardOnSlot = " + cardOnSlot);

        if (cost == 0)
        {
            if (cardOnSlot != null) return;
            PlayCardOnSlot(selectedCard);
            return;
        }

        if (GameManager.Instance.sacrifices < cost)
        {
            if (cardOnSlot == null) return;

            cardOnSlot.SetActive(false);
            Destroy(cardOnSlot);
            Board.Instance.playerCards[slotId] = null;
            GameManager.Instance.sacrifices++;

            Debug.Log("Sacrifices : " + GameManager.Instance.sacrifices + " / " + cost);
            return;
        }

        if (cardOnSlot != null) return;

        GameManager.Instance.sacrifices = 0;
        PlayCardOnSlot(selectedCard);
    }

    private void PlayCardOnSlot(GameObject card)
    {
        hasCard = true;

        iTween.MoveTo(card, transform.position, .5f);
        iTween.RotateTo(card, transform.rotation.eulerAngles, .25f);

        SpriteRenderer cardRenderer = card.GetComponent<SpriteRenderer>();
        if (cardRenderer != null)
        {
            cardRenderer.sortingOrder = 1;
        }

        Card cardComponent = card.GetComponent<Card>();
        if (cardComponent != null)
        {
            Board.Instance.playerCards[slotId] = card;
            cardComponent.boardPosId = slotId;
            Hand.Instance.RemoveFromHand(cardComponent.handPosId);
        }

        Collider cardCollider = card.GetComponent<Collider>();
        if (cardCollider != null)
        {
            cardCollider.enabled = false;
        }

        GameManager.Instance.selectedCard = null;
    }
}

// Joueur qui veut jouer une carte a 2 sacrifices le jeu ne va pas lui empecher
// Ducoup joueur va ce retrouver bloquer a ne pas pouvoir faire un deuxieme sacrifice et ne pas pouvoir jouer sa carte.
// Mettre en place un reset de quelque seconde pour que le joueur puisse retirer ca carte si il se rend compte qu'il a fait une erreur.
