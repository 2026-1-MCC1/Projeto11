using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDComputador : MonoBehaviour
{
    public Image circuloAutoClick;
    public TextMeshPro textoClicksAutoTela;

    private Player player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<Player>(); // Encontra o objeto Player na cena
    }

    // Update is called once per frame
    void Update()
    {
        // Atualiza a barra circular
    if (circuloAutoClick != null)
    {
        circuloAutoClick.fillAmount = player.tempoAuto / player.intervaloAuto;
    }

    // Atualiza o número no centro
    if (textoClicksAutoTela != null)
    {
        textoClicksAutoTela.text = player.clicksAuto.ToString();
    }   
    }
}
