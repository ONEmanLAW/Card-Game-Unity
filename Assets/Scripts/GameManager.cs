using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance => instance;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public bool isPlayerTurn = true;
    public GameObject selectedCard = null;
    public int playerLife = 10;
    public int computerLife = 10;
    [SerializeField] TMP_Text playerLifeText;
    [SerializeField] TMP_Text computerLifeText;
    public int sacrifices = 0;

    public void HitComputer(int atk) {
        computerLife -= atk;
        computerLifeText.text = computerLife.ToString();
        if (computerLife <= 0) {
            // Fin
        }
    }

    public void HitPlayer(int atk) {
        playerLife -= atk;
        playerLifeText.text = playerLife.ToString();
        if (playerLife <= 0) {
            // Fin
        }
    }
}
