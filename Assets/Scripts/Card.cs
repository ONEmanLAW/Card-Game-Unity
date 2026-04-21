using UnityEngine;

public class Card : MonoBehaviour
{
    public CardScriptableObjectScript cardObject;
    SpriteRenderer spriteRenderer;
    Vector3 initialPos;
    bool selected = false;
    [HideInInspector] public int handPosId;
    public int boardPosId;

    private void Awake() {
        cardObject = Resources.Load(gameObject.name.Replace("(Clone)", "")) as CardScriptableObjectScript;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseEnter() {
        if (initialPos == Vector3.zero)
        {
            initialPos = transform.position;
        }
        if (selected) return;

        spriteRenderer.sortingOrder = 5;

        // Déplace la carte vers le haut
        iTween.MoveTo(gameObject, initialPos + transform.up / 20, .1f);
    }

    private void onMouseExit() {
        if (selected) return;

        spriteRenderer.sortingOrder = 1;
        iTween.MoveTo(gameObject, initialPos, .1f);
    }

    private void OnMouseUp() {
        // clic
    }
}
