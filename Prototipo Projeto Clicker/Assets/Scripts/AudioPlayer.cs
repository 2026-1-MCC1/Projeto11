using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource effectsSource;  
    public AudioSource musicSource;    

    [Header("Música de Fundo")]
    public AudioClip musicaFundo;

    [Header("Som de Compra")]
    public AudioClip somCompra;

    [Header("Sons Padrão")]
    public AudioClip[] sonsPadrao;

    [Header("Sons Python")]
    public AudioClip[] sonsPython;

    [Header("Sons Java")]
    public AudioClip[] sonsJava;

    [Header("Sons CSharp")]
    public AudioClip[] sonsCSharp;

    [Header("Sons HolyC")]
    public AudioClip[] sonsHolyC;

    void Start()
    {
        // ?? Música de fundo automática
        if (musicSource != null && musicaFundo != null)
        {
            musicSource.clip = musicaFundo;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    // ??? SOM DE CLIQUE (POR CLASSE)
    public void TocarSomClasse(Classe classeAtual)
    {
        AudioClip clip = null;

        switch (classeAtual)
        {
            case Classe.Nenhuma:
                if (sonsPadrao.Length > 0)
                    clip = sonsPadrao[Random.Range(0, sonsPadrao.Length)];
                break;

            case Classe.Python:
                if (sonsPython.Length > 0)
                    clip = sonsPython[Random.Range(0, sonsPython.Length)];
                break;

            case Classe.Java:
                if (sonsJava.Length > 0)
                    clip = sonsJava[Random.Range(0, sonsJava.Length)];
                break;

            case Classe.CSharp:
                if (sonsCSharp.Length > 0)
                    clip = sonsCSharp[Random.Range(0, sonsCSharp.Length)];
                break;

            case Classe.HolyC:
                if (sonsHolyC.Length > 0)
                    clip = sonsHolyC[Random.Range(0, sonsHolyC.Length)];
                break;
        }

        if (clip != null && effectsSource != null)
        {
            effectsSource.pitch = Random.Range(0.95f, 1.05f);
            effectsSource.PlayOneShot(clip);
        }
    }

    // ?? SOM DE COMPRA
    public void TocarSomCompra()
    {
        if (somCompra != null && effectsSource != null)
        {
            effectsSource.pitch = 1f;
            effectsSource.PlayOneShot(somCompra);
        }
    }
}