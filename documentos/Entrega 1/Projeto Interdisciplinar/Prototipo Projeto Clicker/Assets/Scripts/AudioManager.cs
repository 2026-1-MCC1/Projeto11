using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] somPadrao;
    public float volume = 1f;

    public static AudioManager instancia;
    internal static object instance;

    private void Awake()
    {
        instancia = this;
    }

    public void TocarSomClick3D(Vector3 posicao)
    {
        if (somPadrao.Length == 0) return;

        AudioClip clip = somPadrao[0];

        GameObject temp = new GameObject("SomTemp");

        temp.transform.position = posicao;

        AudioSource source = temp.AddComponent<AudioSource>();

        source.clip = clip;
        source.volume = volume;
        source.spatialBlend = 1f;

        source.Play();

        Destroy(temp, clip.length);
    }
}