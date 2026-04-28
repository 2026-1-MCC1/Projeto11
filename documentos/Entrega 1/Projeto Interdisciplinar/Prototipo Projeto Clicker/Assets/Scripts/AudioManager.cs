using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Clips por classe")]
    public AudioClip[] somPadrao;
    public AudioClip[] somPython;
    public AudioClip[] somCSharp;
    public AudioClip[] somJava;
    public AudioClip[] somHolyC;

    public float volume = 1f;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    AudioClip PegarClipAleatorio(AudioClip[] lista)
    {
        if (lista == null || lista.Length == 0)
            return null;

        return lista[Random.Range(0, lista.Length)];
    }

    public void TocarSomClick3D(Classe classeAtual, Vector3 posicao)
    {
        AudioClip clip = null;

        switch (classeAtual)
        {
            case Classe.Python:
                clip = PegarClipAleatorio(somPython);
                break;

            case Classe.CSharp:
                clip = PegarClipAleatorio(somCSharp);
                break;

            case Classe.Java:
                clip = PegarClipAleatorio(somJava);
                break;

            case Classe.HolyC:
                clip = PegarClipAleatorio(somHolyC);
                break;

            default:
                clip = PegarClipAleatorio(somPadrao);
                break;
        }

        if (clip != null)
        {
            GameObject temp = new GameObject("SomTemp");
            temp.transform.position = posicao;

            AudioSource source = temp.AddComponent<AudioSource>();
            source.clip = clip;
            source.volume = volume;
            source.pitch = Random.Range(0.9f, 1.1f);
            source.spatialBlend = 1f;
            source.Play();

            Destroy(temp, clip.length);
        }
    }
}