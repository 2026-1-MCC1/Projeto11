using UnityEngine;

public class Sonoro : MonoBehaviour
{
    public static Sonoro instance;

    [Header("Audio Source")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    [Header("Audio Clips")]
    public AudioClip background;
    public AudioClip death;
    public AudioClip checkpoint;
    public AudioClip walltouch;
    public AudioClip portalin;
    public AudioClip portalout;

    private void Awake()
    {
        // Se já existe um, destrói esse aqui
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (musicSource != null && background != null)
        {
            musicSource.clip = background;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (SFXSource != null)
        {
            SFXSource.PlayOneShot(clip);
        }
    }
}

