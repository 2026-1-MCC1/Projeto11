using UnityEngine;
using TMPro;

public class TextoFlutuante : MonoBehaviour
{
    public float velocidade = 3f;
    public TextMeshPro texto;
    ClickSpawner clickSpawner;
    public int valor;
    Player player;

    private void Start()
    {
        clickSpawner = FindFirstObjectByType<ClickSpawner>();
        player = FindFirstObjectByType<Player>();
    }
    public void DefinirValor()
    {
        valor = clickSpawner.valorFinal;    
        texto.text = "+" + (player.multiplicadorPontos * player.multiplicadorClasse * player.multiplicadorCiclo).ToString();
    }

    void Update()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
        this.GetComponent<TMP_Text>().text =  "+ " + (player.multiplicadorPontos * player.multiplicadorClasse * player.multiplicadorCiclo)  + "";
        valor = clickSpawner.valorFinal;

        transform.Translate(Vector3.up * velocidade * Time.deltaTime);

        Destroy(gameObject, 2f);
        
        
    }
}