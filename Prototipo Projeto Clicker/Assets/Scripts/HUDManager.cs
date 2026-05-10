using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using NUnit.Framework.Internal;

public class HUDManager : MonoBehaviour
{
    [Header("Referências das HUDs")]
    public GameObject hudPrincipal;
    public GameObject hudSecundaria;
    public GameObject hudMenu;
    public GameObject hudconfig;
    public GameObject hudClasse;
    public GameObject hudTextura;
    public GameObject hudUpgrade;
    public GameObject hudShop;
    public GameObject hudbotoes;
    public GameObject canvascelular;

    public TextMeshProUGUI textoPontos;
    public TextMeshProUGUI textoC;

    public bool hudprincipaloneoff;
    public bool hudsecundariaoneoff;
    public bool hudmenuoneoff;
    public bool hudconfigoneoff;
    public bool hudupgradeoneoff;
    public bool hudshoponeoff;
    public bool hudclasseoneoff;
    public bool hudtexturaoneoff;

    [Header("Referência do Player")]
    public Player player;
    public Camera playerCamera;

    [Header("Botões")]
    public Button botãoinicio;
    public Button botãosair;
    public Button botãoconfig;
    public Button botãovoltar;
    public Button botãoupgrade;
    public Button botãoclasse;
    public Button botãotextura;
    public Button botãoshop;
    public Button voltar;
    public Button comprarmoedapaga;
    public Button botãoconfig2;

    public bool Upgrade;

    [Header("Sliders")]
    public Slider slidersensi;
    public Slider slideFOV;

    [Header("Textos (TMP)")]
    public TextMeshProUGUI textoFOV;
    public TextMeshProUGUI textoSens;

    private ExitButton exitButton;

    public Image celularidle;

    void Start()
    {
        exitButton = FindAnyObjectByType<ExitButton>();

        hudMenu.SetActive(true);
        hudPrincipal.SetActive(false);
        hudSecundaria.SetActive(false);
        hudconfig.SetActive(false);

        hudprincipaloneoff = false;
        hudsecundariaoneoff = false;
        hudmenuoneoff = true;
        hudconfigoneoff = false;

        Upgrade = false;

        playerCamera = Camera.main;

        slideFOV.value = playerCamera.fieldOfView;
        slidersensi.value = player.sensibilidade;

        AtualizarTextoFOV(slideFOV.value);
        AtualizarTextoSens(slidersensi.value);

        slideFOV.onValueChanged.AddListener(MudarFOV);
        slidersensi.onValueChanged.AddListener(MudarSensibilidade);

        botãoinicio.onClick.AddListener(IniciarJogo);
        botãoconfig.onClick.AddListener(Configuracoes);
        botãosair.onClick.AddListener(SairDoJogo);
        botãovoltar.onClick.AddListener(VoltarMenuPrincipal);
        botãoclasse.onClick.AddListener(AlternarHUDClasse);
        botãotextura.onClick.AddListener(AlternarHUDTextura);
        botãoupgrade.onClick.AddListener(AlternarHUDUpgrade);
        botãoshop.onClick.AddListener(AlternarHUDShop);
        voltar.onClick.AddListener(VoltarMenuCelular);
        comprarmoedapaga.onClick.AddListener(Compramoedapaga);
        botãoconfig2.onClick.AddListener(Configuracoes);
        hudClasse.SetActive(false);
        hudTextura.SetActive(false);
        hudUpgrade.SetActive(false);
        hudShop.SetActive(false);
        voltar.gameObject.SetActive(false);
    }

    void Update()
    {
        if (hudmenuoneoff)
        {
            player.TravarControle(true);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            AlternarHUD();
        }
    }

    // =========================
    // BOTÕES
    // =========================

    void IniciarJogo()
    {
        StartCoroutine(AnimacaoInicio());
    }

    IEnumerator AnimacaoInicio()
    {
        Animator animInicio = botãoinicio.GetComponent<Animator>();

        if (animInicio != null)
        {
            animInicio.Play("animacaobotao", 0, 0f);
            yield return new WaitForSeconds(0.5f);
        }

        celularidle.gameObject.SetActive(true);
        hudMenu.SetActive(false);
        hudPrincipal.SetActive(true);
        hudconfig.SetActive(false);

        hudprincipaloneoff = true;
        hudmenuoneoff = false;
        hudconfigoneoff = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Upgrade = false;

        if (player != null)
        {
            player.TravarControle(false);
        }
    }

    void Configuracoes()
    {
        StartCoroutine(AnimacaoConfig());
    }

    IEnumerator AnimacaoConfig()
    {
        
        hudPrincipal.SetActive(false);
        hudSecundaria.SetActive(false);
        Upgrade = false;
        hudbotoes.SetActive(false);
        hudClasse.SetActive(false);
        hudTextura.SetActive(false);
        hudUpgrade.SetActive(false);
        hudShop.SetActive(false);
        voltar.gameObject.SetActive(false);
        textoPontos.gameObject.SetActive(false);
        Animator animConfig = botãoconfig.GetComponent<Animator>();

        if (animConfig != null)
        {
            animConfig.Play("animacaobotao", 0, 0f);
            yield return new WaitForSeconds(0.5f);
        }

        hudMenu.SetActive(false);
        hudconfig.SetActive(true);

        hudconfigoneoff = true;

        slideFOV.value = playerCamera.fieldOfView;
        slidersensi.value = player.sensibilidade;

        AtualizarTextoFOV(slideFOV.value);
        AtualizarTextoSens(slidersensi.value);
    }

    void SairDoJogo()
    {
        StartCoroutine(AnimacaoSair());
    }

    IEnumerator AnimacaoSair()
    {
        Animator animFim = botãosair.GetComponent<Animator>();

        if (animFim != null)
        {
            animFim.Play("animacaobotao", 0, 0f);
            yield return new WaitForSeconds(0.5f);
        }

        Debug.Log("Saindo do jogo...");

        if (exitButton != null)
        {
            exitButton.ExitGame();
        }
        else
        {
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }

    // =========================
    // SLIDERS
    // =========================

    void MudarFOV(float valor)
    {
        playerCamera.fieldOfView = valor;
        AtualizarTextoFOV(valor);
    }

    void MudarSensibilidade(float valor)
    {
        player.sensibilidade = valor;
        AtualizarTextoSens(valor);
    }

    // =========================
    // TEXTOS
    // =========================

    void AtualizarTextoFOV(float valor)
    {
        textoFOV.text = "FOV: " + valor.ToString("F0");
    }

    void AtualizarTextoSens(float valor)
    {
        textoSens.text = "Sens: " + valor.ToString("F0");
    }

    // =========================
    // HUD SECUNDÁRIA
    // =========================

    void AlternarHUD()
    {
        celularidle.gameObject.SetActive(false);
        player.congelarPosicao = true;
        if (hudprincipaloneoff)
        {
            hudPrincipal.SetActive(false);
            hudSecundaria.SetActive(true);

            Animator animCelular = canvascelular.GetComponent<Animator>();

            if (animCelular != null)
            {
                animCelular.Play("CelularEntrada");
            }

            textoC.gameObject.SetActive(true);

            hudsecundariaoneoff = true;
            hudprincipaloneoff = false;

            Upgrade = true;

            hudbotoes.SetActive(true);
            hudClasse.SetActive(false);
            hudTextura.SetActive(false);
            hudUpgrade.SetActive(false);
            hudShop.SetActive(false);

            voltar.gameObject.SetActive(false);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            textoPontos.gameObject.SetActive(false);
        }
        else if (hudsecundariaoneoff)
        {
            StartCoroutine(DesativarCelularComAnimacao());
        }

        if (player != null)
        {
            player.TravarControle(hudsecundariaoneoff);
        }
    }

    private IEnumerator DesativarCelularComAnimacao()
    {
        voltar.gameObject.SetActive(false);
        textoPontos.gameObject.SetActive(false);

        Animator animCelular = canvascelular.GetComponent<Animator>();

        if (animCelular != null)
        {
            animCelular.Play("CelularSaida");
            yield return new WaitForSeconds(1.2f);
        }

        hudPrincipal.SetActive(true);
        hudSecundaria.SetActive(false);

        hudprincipaloneoff = true;
        hudsecundariaoneoff = false;

        Upgrade = false;

        hudbotoes.SetActive(false);
        hudClasse.SetActive(false);
        hudTextura.SetActive(false);
        hudUpgrade.SetActive(false);
        hudShop.SetActive(false);

        voltar.gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        celularidle.gameObject.SetActive(true);

        textoPontos.gameObject.SetActive(true);

        if (player != null)
        {
            player.TravarControle(false);
        }
        player.congelarPosicao = false;
    }

    void VoltarMenuPrincipal()
    {
        StartCoroutine(AnimacaoVoltar());
    }

    IEnumerator AnimacaoVoltar()
    {
        Animator animVoltar = botãovoltar.GetComponent<Animator>();

        if (animVoltar != null)
        {
            animVoltar.Play("animacaobotao", 0, 0f);
            yield return new WaitForSeconds(0.5f);
        }

        hudMenu.SetActive(true);
        hudPrincipal.SetActive(false);
        hudSecundaria.SetActive(false);
        hudconfig.SetActive(false);

        hudprincipaloneoff = false;
        hudsecundariaoneoff = false;
        hudmenuoneoff = true;
        hudconfigoneoff = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Upgrade = false;

        if (player != null)
        {
            player.TravarControle(true);
        }
    }


    void AlternarHUDClasse()
    {
        textoC.gameObject.SetActive(false);
        Debug.Log("Alternando HUD de Classe");

        hudClasse.SetActive(true);
        hudbotoes.SetActive(false);

        voltar.gameObject.SetActive(true);
        textoPontos.gameObject.SetActive(true);

        hudclasseoneoff = true;
    }

    void AlternarHUDTextura()
    {
        textoC.gameObject.SetActive(false);
        Debug.Log("Alternando HUD de Textura");

        hudbotoes.SetActive(false);
        hudTextura.SetActive(true);

        voltar.gameObject.SetActive(true);
        textoPontos.gameObject.SetActive(true);

        hudtexturaoneoff = true;
    }

    void AlternarHUDUpgrade()
    {
        textoC.gameObject.SetActive(false);
        Debug.Log("Alternando HUD de Upgrade");

        hudbotoes.SetActive(false);
        hudUpgrade.SetActive(true);

        voltar.gameObject.SetActive(true);
        textoPontos.gameObject.SetActive(true);

        hudupgradeoneoff = true;
    }

    void AlternarHUDShop()
    {
        textoC.gameObject.SetActive(false);
        Debug.Log("Alternando HUD de Shop");

        hudbotoes.SetActive(false);
        hudShop.SetActive(true);

        voltar.gameObject.SetActive(true);
        textoPontos.gameObject.SetActive(true);

        hudshoponeoff = true;
    }

    void VoltarMenuCelular()
    {
        textoC.gameObject.SetActive(true);
        Debug.Log("Voltando ao Menu do Celular");

        hudClasse.SetActive(false);
        hudTextura.SetActive(false);
        hudUpgrade.SetActive(false);
        hudShop.SetActive(false);

        hudbotoes.SetActive(true);

        voltar.gameObject.SetActive(false);
        textoPontos.gameObject.SetActive(false);

        hudupgradeoneoff = false;
        hudclasseoneoff = false;
        hudtexturaoneoff = false;
        hudshoponeoff = false;
    }

    void Compramoedapaga()
    {
        if (player.pontos >= 10000)
        {
            player.moedapaga += 10;
            player.textoMoedaPaga.text = "R$: " + player.moedapaga;
            player.pontos -= 10000;
        }
    }
}