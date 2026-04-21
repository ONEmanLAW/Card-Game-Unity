using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField] Transform headView;
    [SerializeField] Transform boardView;

    private static CamController instance = null;
    public static CamController Instance => instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update() { 
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            GoToHeadView();
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            GoToBoardView();
        }
    }

    public void GoToHeadView()
    {
        iTween.MoveTo(gameObject, headView.position, .5f);
        iTween.RotateTo(gameObject, headView.rotation.eulerAngles, .5f);
    }

    public void GoToBoardView() {
        iTween.MoveTo(gameObject, boardView.position, .5f); 
        iTween.RotateTo(gameObject, boardView.rotation.eulerAngles, .5f);
    }

    
}