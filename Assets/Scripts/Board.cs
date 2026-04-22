using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject[] playerCards;
    public GameObject[] computerCards;

    private static Board instance = null;
    public static Board Instance => instance;
    
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
