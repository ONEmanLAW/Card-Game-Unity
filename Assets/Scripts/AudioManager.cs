using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] sfx;
    [SerializeField] AudioClip[] voices;

    private static AudioManager instance = null;
    public static AudioManager Instance => instance;

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

    public void PlayClip(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void PlaySFX(int id)
    {
        audioSource.PlayOneShot(sfx[id]);
    }

    public void PlayRandomVoice(int idMin, int idMax)
    {
        audioSource.PlayOneShot(voices[Random.Range(idMin, idMax)]);
    }
}
