using UnityEngine;

public class Card : MonoBehaviour
{
    public CardScriptableObjectScript cardObject;
    SpriteRenderer spriteRenderer;
    Vector3 initialPos;
    bool selected = false;
    [HideInInspector] public int handPosId;
    public int boardPosId;

    private void Awake()
    {
        cardObject = Resources.Load(gameObject.name.Replace("(Clone)", "")) as CardScriptableObjectScript;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseEnter() {

        AudioManager.Instance.PlaySFX(0); // hoverCard

        // Survol
        if (initialPos == Vector3.zero)
            initialPos = transform.position;
        if (selected) return;

        spriteRenderer.sortingOrder = 5;
        iTween.MoveTo(gameObject, initialPos + transform.up / 20, .1f);
    }

    private void OnMouseExit() {

        AudioManager.Instance.PlaySFX(0); // hoverCard
        // Stop survol
        if (selected) return;

        spriteRenderer.sortingOrder = 1;
        iTween.MoveTo(gameObject, initialPos, .1f);
    }

    private void OnMouseUp() {

        if (GameManager.Instance.nbActions >= 2) return;

        if (cardObject != null && cardObject.cost > 0 && GameManager.Instance.sacrificeSummonUsedThisTurn) {
            Debug.Log("Carte avec sacrifice déjà jouée ce tour.");
            return;
        }

        AudioManager.Instance.PlaySFX(0); // hoverCard
        if (selected) return;
        selected = true;
        CamController.Instance.GoToBoardView();
        GameManager.Instance.selectedCard = gameObject;
    }
}
