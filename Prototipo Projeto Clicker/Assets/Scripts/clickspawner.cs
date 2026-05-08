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

            Vector3 pos = transform.position;
            pos.y += 3f;

            GameObject textoObj = Instantiate(textoPrefab, textspaw.position, Quaternion.identity);


            TextoFlutuante tf = textoObj.GetComponent<TextoFlutuante>();

            tf.valor = valorBase * multiplicador * multiplicadorCiclo * multiplicadorClasse;
        }
    }
}