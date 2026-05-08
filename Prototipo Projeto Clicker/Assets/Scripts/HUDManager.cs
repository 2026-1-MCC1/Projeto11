using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    public bool hudprincipaloneoff;
    public bool hudsecundariaoneoff;
    public bool hudmenuoneoff;
    public bool hudconfigoneoff;

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
        }
        else if (hudsecundariaoneoff)
        {
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

        }

        if (player != null)
        {
            player.TravarControle(hudsecundariaoneoff);
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
    }

    void AlternarHUDTextura()
    {
        // Lógica para alternar HUD de textura
        Debug.Log("Alternando HUD de Textura");
        hudbotoes.SetActive(false);
        hudTextura.SetActive(true);
        voltar.gameObject.SetActive(true);
    }

    void AlternarHUDUpgrade()
    {
        // Lógica para alternar HUD de upgrade
        Debug.Log("Alternando HUD de Upgrade");
        hudbotoes.SetActive(false);
        hudUpgrade.SetActive(true);
        voltar.gameObject.SetActive(true);
    }  

    void AlternarHUDShop()
    {
        // Lógica para alternar HUD de shop
        Debug.Log("Alternando HUD de Shop");
        hudbotoes.SetActive(false);
        hudShop.SetActive(true);
        voltar.gameObject.SetActive(true);
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
    }

}