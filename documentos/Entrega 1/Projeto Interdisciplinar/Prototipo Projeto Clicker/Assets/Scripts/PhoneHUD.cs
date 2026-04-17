using UnityEngine;
using TMPro;

public class PhoneHUD : MonoBehaviour
{
    public GameObject telaPrincipal;
    public GameObject telaLoja;

    public TextMeshProUGUI textoPontos;

    private int pontos;

    void Start()
    {
        AbrirTelaPrincipal();
    }

    public void AbrirTelaPrincipal()
    {
        telaPrincipal.SetActive(true);
        telaLoja.SetActive(false);
    }

    public void AbrirLoja()
    {
        telaPrincipal.SetActive(false);
        telaLoja.SetActive(true);
    }

    public void AdicionarPontos(int valor)
    {
        pontos += valor;
        AtualizarUI();
    }

    public void GastarPontos(int valor)
    {
        if (pontos >= valor)
        {
            pontos -= valor;
            AtualizarUI();
        }
    }

    void AtualizarUI()
    {
        textoPontos.text = "Pontos: " + pontos;
    }
}