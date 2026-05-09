using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

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
    public GameObject canvascelular; // Novo: referência para o objeto com a animação do celular

    public TextMeshProUGUI textoPontos;

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

    public bool Upgrade;

    [Header("Sliders")]
    public Slider slidersensi;
    public Slider slideFOV;

    [Header("Textos (TMP)")]
    public TextMeshProUGUI textoFOV;
    public TextMeshProUGUI textoSens;

    private ExitButton exitButton;


    void Start()
    {
        exitButton = FindAnyObjectByType<ExitButton>();
        // Estado inicial
        hudMenu.SetActive(true);
        hudPrincipal.SetActive(false);
        hudSecundaria.SetActive(false);
        hudconfig.SetActive(false);

        hudprincipaloneoff = false;
        hudsecundariaoneoff = false;
        hudmenuoneoff = true;
        hudconfigoneoff = false;

        Upgrade = false;

        // Camera
        playerCamera = Camera.main;

        // Valores iniciais
        slideFOV.value = playerCamera.fieldOfView;
        slidersensi.value = player.sensibilidade;

        // Atualiza textos na inicialização
        AtualizarTextoFOV(slideFOV.value);
        AtualizarTextoSens(slidersensi.value);

        // Eventos sliders
        slideFOV.onValueChanged.AddListener(MudarFOV);
        slidersensi.onValueChanged.AddListener(MudarSensibilidade);

        // Botões
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
        hudMenu.SetActive(false);
        hudconfig.SetActive(true);

        hudconfigoneoff = true;

        // Atualiza sliders e textos
        slideFOV.value = playerCamera.fieldOfView;
        slidersensi.value = player.sensibilidade;

        AtualizarTextoFOV(slideFOV.value);
        AtualizarTextoSens(slidersensi.value);
    }

    void SairDoJogo()
    {
        Debug.Log("Saindo do jogo...");
        
        if (exitButton != null)
        {
            exitButton.ExitGame();
        }
        else
        {
            // Se não encontrar o ExitButton, sai do jogo diretamente
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
        if (hudprincipaloneoff)
        {
            hudPrincipal.SetActive(false);
            hudSecundaria.SetActive(true);

            // Tocar animação de entrada do celular
            Animator animCelular = canvascelular.GetComponent<Animator>();
            if (animCelular != null)
            {
                animCelular.Play("CelularEntrada"); // Substitua "CelularEntrada" pelo nome exato da sua animação
            }

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
            // Iniciar coroutine para tocar animação de saída e desativar após
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
            // Aguarde a duração da animação
            yield return new WaitForSeconds(animCelular.GetCurrentAnimatorStateInfo(0).length);
        }

        // Agora desative a HUD após a animação
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
        textoPontos.gameObject.SetActive(true);

        if (player != null)
        {
            player.TravarControle(false); // Como está desativando, controle liberado
        }
    }
    
    void VoltarMenuPrincipal()
    {
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
        // Lógica para alternar HUD de classe
        Debug.Log("Alternando HUD de Classe");
        hudClasse.SetActive(true);
        hudbotoes.SetActive(false);
        voltar.gameObject.SetActive(true);
        textoPontos.gameObject.SetActive(true);
        hudclasseoneoff = true;
    }

    void AlternarHUDTextura()
    {
        // Lógica para alternar HUD de textura
        Debug.Log("Alternando HUD de Textura");
        hudbotoes.SetActive(false);
        hudTextura.SetActive(true);
        voltar.gameObject.SetActive(true);
        textoPontos.gameObject.SetActive(true);
        hudtexturaoneoff = true;
    }

    void AlternarHUDUpgrade()
    {
        // Lógica para alternar HUD de upgrade
        Debug.Log("Alternando HUD de Upgrade");
        hudbotoes.SetActive(false);
        hudUpgrade.SetActive(true);
        voltar.gameObject.SetActive(true);
        textoPontos.gameObject.SetActive(true);
        hudupgradeoneoff = true;
    }  

    void AlternarHUDShop()
    {
        // Lógica para alternar HUD de shop
        Debug.Log("Alternando HUD de Shop");
        hudbotoes.SetActive(false);
        hudShop.SetActive(true);
        voltar.gameObject.SetActive(true);
        textoPontos.gameObject.SetActive(true);
        hudshoponeoff = true;
    }

    void VoltarMenuCelular()
    {
        // Lógica para voltar ao menu do celular
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