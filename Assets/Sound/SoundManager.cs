using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip select;
    [SerializeField] private AudioClip connect;
    [SerializeField] private AudioClip release;
    
    private SoundManager() { }
    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    public void PlayOnSelect() => Play(select);
    public void PlayOnConnect() => Play(connect);
    public void PlayOnRelease() => Play(release);

    private void Play(AudioClip clip)
    {
        if (clip == null) return;
        audioSource.pitch = Random.Range(.95f, 1.05f);
        audioSource.PlayOneShot(clip);
    }
}
