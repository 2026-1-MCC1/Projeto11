using UnityEngine;

public class ClickSpawner : MonoBehaviour
{
    public GameObject textoPrefab;

    public int valorBase = 1;
    public int multiplicador = 1;
    public int multiplicadorClasse = 1;
    public int multiplicadorCiclo = 1;
    public Transform textspaw;
    private HUDManager HUDmanager;
    private Player player;
    public int valorFinal;

    // Novo: raio para spawn aleatório
    public float raioAleatorio = 1f;

    void Start()
    {
        HUDmanager = FindAnyObjectByType<HUDManager>();
        player = FindAnyObjectByType<Player>();
    }

    void Update()
    {
        multiplicadorClasse = player.multiplicadorClasse;
        multiplicadorCiclo = player.multiplicadorCiclo;
        multiplicador = player.multiplicadorPontos;
    }

    void OnMouseDown()
    {
        if (HUDmanager.hudprincipaloneoff == true)
        {
            multiplicadorClasse = player.multiplicadorClasse;
            multiplicadorCiclo = player.multiplicadorCiclo;
            multiplicador = player.multiplicadorPontos;

            // Calcular posição aleatória perto de textspaw
            Vector3 offset = Random.insideUnitSphere * raioAleatorio;
            offset.y = 0; // Manter no plano horizontal, ajuste se necessário
            Vector3 spawnPosition = textspaw.position + offset;

            GameObject textoObj = Instantiate(textoPrefab, spawnPosition, Quaternion.identity);

            TextoFlutuante tf = textoObj.GetComponent<TextoFlutuante>();

            tf.valor = valorBase * multiplicador * multiplicadorCiclo * multiplicadorClasse;
        }
    }
}