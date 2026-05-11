using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    // =========================================
    // AUDIO SOURCES
    // =========================================

    [Header("Audio Sources")]
    public AudioSource effectsSource;
    public AudioSource musicSource;

    // =========================================
    // MUSICA
    // =========================================

    [Header("Música de Fundo")]
    public AudioClip musicaFundo;

    [Header("Velocidade Música Dia")]
    public float pitchDia = 1f;

    [Header("Velocidade Música Noite")]
    public float pitchNoite = 0.75f;

    // =========================================
    // SONS GERAIS
    // =========================================

    [Header("Som de Compra")]
    public AudioClip somCompra;

    [Header("Som de Diálogo")]
    public AudioClip somDialogo;

    [Header("Som Interruptor Ligar")]
    public AudioClip somInterruptorOn;

    [Header("Som Interruptor Desligar")]
    public AudioClip somInterruptorOff;

    [Header("Som Preparar Café")]
    public AudioClip somPrepararCafe;

    [Header("Som Beber Café")]
    public AudioClip somBeberCafe;

    [Header("Som Noite")]
    public AudioClip somNoite;

    // =========================================
    // SONS DAS CLASSES
    // =========================================

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

    // =========================================
    // CONTROLE INTERNO
    // =========================================

    private bool modoNoiteAtivo = false;

    // =========================================
    // START
    // =========================================

    void Start()
    {
        if (musicSource != null && musicaFundo != null)
        {
            musicSource.clip = musicaFundo;
            musicSource.loop = true;
            musicSource.pitch = pitchDia;
            musicSource.Play();

            Debug.Log("Música iniciada!");
        }
    }

    // =========================================
    // SOM POR CLASSE
    // =========================================

    public void TocarSomClasse(Classe classeAtual)
    {
        if (effectsSource == null)
            return;

        AudioClip clip = null;

        switch (classeAtual)
        {
            case Classe.Nenhuma:
                clip = PegarSomAleatorio(sonsPadrao);
                break;

            case Classe.Python:
                clip = PegarSomAleatorio(sonsPython);
                break;

            case Classe.Java:
                clip = PegarSomAleatorio(sonsJava);
                break;

            case Classe.CSharp:
                clip = PegarSomAleatorio(sonsCSharp);
                break;

            case Classe.HolyC:
                clip = PegarSomAleatorio(sonsHolyC);
                break;
        }

        if (clip != null)
        {
            effectsSource.pitch = Random.Range(0.95f, 1.05f);
            effectsSource.PlayOneShot(clip);
        }
    }

    // =========================================
    // SOM COMPRA
    // =========================================

    public void TocarSomCompra()
    {
        if (effectsSource == null || somCompra == null)
            return;

        effectsSource.pitch = 1f;
        effectsSource.PlayOneShot(somCompra);
    }

    // =========================================
    // SOM DIALOGO
    // =========================================

    public void TocarSomDialogo()
    {
        if (effectsSource == null || somDialogo == null)
            return;

        effectsSource.pitch = 1f;
        effectsSource.PlayOneShot(somDialogo);
    }

    // =========================================
    // INTERRUPTOR ON
    // =========================================

    public void TocarInterruptorOn()
    {
        if (effectsSource == null || somInterruptorOn == null)
            return;

        effectsSource.pitch = 1f;
        effectsSource.PlayOneShot(somInterruptorOn);
    }

    // =========================================
    // INTERRUPTOR OFF
    // =========================================

    public void TocarInterruptorOff()
    {
        if (effectsSource == null || somInterruptorOff == null)
            return;

        effectsSource.pitch = 1f;
        effectsSource.PlayOneShot(somInterruptorOff);
    }

    // =========================================
    // PREPARAR CAFÉ
    // =========================================

    public void TocarPrepararCafe()
    {
        if (effectsSource == null || somPrepararCafe == null)
            return;

        effectsSource.pitch = 1f;
        effectsSource.PlayOneShot(somPrepararCafe);
    }

    // =========================================
    // BEBER CAFÉ
    // =========================================

    public void TocarBeberCafe()
    {
        if (effectsSource == null || somBeberCafe == null)
            return;

        effectsSource.pitch = 1f;
        effectsSource.PlayOneShot(somBeberCafe);
    }

    // =========================================
    // MODO NOITE
    // =========================================

    public void EntrarModoNoite()
    {
        if (modoNoiteAtivo)
            return;

        modoNoiteAtivo = true;

        if (effectsSource != null && somNoite != null)
        {
            effectsSource.pitch = 1f;
            effectsSource.PlayOneShot(somNoite);
        }

        if (musicSource != null)
        {
            musicSource.pitch = pitchNoite;
        }
    }

    // =========================================
    // VOLTAR DIA
    // =========================================

    public void VoltarModoDia()
    {
        if (!modoNoiteAtivo)
            return;

        modoNoiteAtivo = false;

        if (musicSource != null)
        {
            musicSource.pitch = pitchDia;
        }
    }

    // =========================================
    // SOM ALEATÓRIO
    // =========================================

    private AudioClip PegarSomAleatorio(AudioClip[] lista)
    {
        if (lista == null || lista.Length == 0)
            return null;

        return lista[Random.Range(0, lista.Length)];
    }

    // =========================================
    // FUNÇÕES PRONTAS
    // =========================================

    public void CliquePython()
    {
        TocarSomClasse(Classe.Python);
    }

    public void CliqueJava()
    {
        TocarSomClasse(Classe.Java);
    }

    public void CliqueCSharp()
    {
        TocarSomClasse(Classe.CSharp);
    }

    public void CliqueHolyC()
    {
        TocarSomClasse(Classe.HolyC);
    }

    public void CliquePadrao()
    {
        TocarSomClasse(Classe.Nenhuma);
    }
}