using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Player : MonoBehaviour
{
    // Referências para a câmera e o transform do jogador
    public Transform _transform;
    public Transform cameraTransform;

    Vector2 rotacaoMouse;

    public float sensibilidade;
    public float velocidade = 5.0f;

    private Rigidbody rb;
    private Vector3 movimento;

    ClickSpawner clickSpawner;

    // Configurações para o raycast
    public float maxDistance = 10f;
    public LayerMask hitLayers;

    // Variáveis para o sistema de pontos e upgrades
    int pontos = 0;

    public TextMeshProUGUI textoPontos;
    public TextMeshProUGUI textoPontos2;
    public TextMeshProUGUI textoMultiplicador;

    int multiplicadorPontos = 1;

    int clicksAuto = 0;
    float tempoAuto = 0f;
    float intervaloAuto = 1f;

    public TextMeshProUGUI textoAutoClick;

    int pontosMaximos = 500;

    public TextMeshProUGUI textoLimite;

    // HUD para mostrar os preços dos upgrades
    public TextMeshPro precoMulti;
    public TextMeshPro precoAuto;
    public TextMeshPro precoLimite;

    int custoMulti;
    int custoAuto;
    int custoLimite;

    // Classes
    public TextMeshProUGUI ClasseTexto;

    int multiplicadorClasse;
    int clicksautomaticosclasse;
    int limiteclasse;

    // Variáveis para as luzes e a janela
    public Light luzQuarto;
    public Light luzSol;
    public Light luzComputador;

    public Renderer janelaRenderer;

    int multiplicadorCiclo = 1;

    // Pra travar a câmera no portal
    public bool travarCamera = false;
    public bool moveble = true;

    // materiais padrão
    public Material materialparedepadrao;
    public Material materialchaopadrao;
    public Texture portapadrao;
    public Material forrocamapadrao;
    public Material janeladiapadrao;
    public Material janelanoitepadrao;

    // materiais realistas
    public Material materialparederealista;
    public Material materialchaorealista;
    public Material janelarealistadia;
    public Material janelarealistanoite;
    public Texture portarealista;
    public Material forrocamarelista;

    int realista;

    // materiais mono
    int mono;

    public Material materialparedemono;
    public Material materialchaomono;
    public Material janeladiamono;
    public Material janelanoitemono;
    public Texture portamono;
    public Material forrocamamono;

    // materiais hyperpop
    int hyperpop;

    public Material materialparedehyperpop;
    public Material materialchaohyperpop;
    public Material janeladiahyperpop;
    public Material janelanoitehyperpop;
    public Texture portahyperpop;
    public Material forrocamahyperpop;

    // renderer dos objetos
    public Renderer portaRenderer;
    public Renderer parede1Renderer;
    public Renderer parede2Renderer;
    public Renderer parede3Renderer;
    public Renderer parede4Renderer;
    public Renderer parede5Renderer;
    public Renderer parede6Renderer;
    public Renderer parede7Renderer;
    public Renderer parede8Renderer;
    public Renderer chaoRenderer;
    public Renderer forroRenderer;
    public Renderer forro2Renderer;
    public Renderer cama1Renderer;
    public Renderer cama2Renderer;

    // HUDManager
    private HUDManager HUDManager;

    public bool compraoneoff;
    public bool hudmenu;
    public bool hudconfig;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        pontosMaximos = 500;

        multiplicadorClasse = 1;
        clicksautomaticosclasse = 1;
        limiteclasse = 1;

        Screen.fullScreen = true;

        custoMulti = 25 * multiplicadorPontos;
        custoAuto = 10;
        custoLimite = pontosMaximos;

        textoMultiplicador.text = " (H) Multiplicador: " + (multiplicadorPontos * multiplicadorCiclo * multiplicadorClasse);
        textoAutoClick.text = " (J) Clicks Automaticos: " + clicksAuto * clicksautomaticosclasse;
        textoLimite.text = " (K) Limite: " + pontosMaximos;

        precoAuto.text = "Preço: " + custoAuto;
        precoMulti.text = "Preço: " + custoMulti;
        precoLimite.text = "Preço: " + custoLimite;

        TexturasPadrao();

        janelaRenderer.material = janeladiapadrao;

        clickSpawner = FindFirstObjectByType<ClickSpawner>();

        HUDManager = FindAnyObjectByType<HUDManager>();

        hudmenu = HUDManager.hudmenuoneoff;
    }

    void Update()
    {
        // INPUT DE MOVIMENTO
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        movimento = new Vector3(x, 0f, y).normalized;

        hudmenu = HUDManager.hudmenuoneoff;
        compraoneoff = HUDManager.Upgrade;
        hudconfig = HUDManager.hudconfigoneoff;

        // CAMERA
        if (!travarCamera)
        {
            Vector2 controleMouse = new Vector2(
                Input.GetAxis("Mouse X"),
                Input.GetAxis("Mouse Y")
            );

            rotacaoMouse = new Vector2(
                rotacaoMouse.x + controleMouse.x * sensibilidade * Time.deltaTime,
                rotacaoMouse.y + controleMouse.y * sensibilidade * Time.deltaTime
            );

            _transform.eulerAngles = new Vector3(
                _transform.eulerAngles.x,
                rotacaoMouse.x,
                _transform.eulerAngles.z
            );

            rotacaoMouse.y = Mathf.Clamp(rotacaoMouse.y, -80, 80);

            cameraTransform.localEulerAngles = new Vector3(
                -rotacaoMouse.y,
                cameraTransform.localEulerAngles.y,
                cameraTransform.localEulerAngles.z
            );
        }

        // RESTANTE DO SEU CÓDIGO CONTINUA NORMAL...
    }

    void FixedUpdate()
    {
        if (moveble)
        {
            Vector3 direcao = transform.TransformDirection(movimento);

            Vector3 novaPosicao =
                rb.position + direcao * velocidade * Time.fixedDeltaTime;

            rb.MovePosition(novaPosicao);
        }
    }

    public void ResetarCamera()
    {
        rotacaoMouse = Vector2.zero;

        _transform.eulerAngles = Vector3.zero;

        cameraTransform.localEulerAngles = Vector3.zero;
    }

    public void TexturasRealistas()
    {
        portaRenderer.material.mainTexture = portarealista;

        parede1Renderer.material = materialparederealista;
        parede2Renderer.material = materialparederealista;
        parede3Renderer.material = materialparederealista;
        parede4Renderer.material = materialparederealista;
        parede5Renderer.material = materialparederealista;
        parede6Renderer.material = materialparederealista;
        parede7Renderer.material = materialparederealista;
        parede8Renderer.material = materialparederealista;

        chaoRenderer.material = materialchaorealista;

        forroRenderer.material = forrocamarelista;
        forro2Renderer.material = forrocamarelista;

        cama1Renderer.material = materialchaorealista;
        cama2Renderer.material = materialchaorealista;

        realista = 1;
        mono = 0;
        hyperpop = 0;
    }

    public void TexturasPadrao()
    {
        portaRenderer.material.mainTexture = portapadrao;

        parede1Renderer.material = materialparedepadrao;
        parede2Renderer.material = materialparedepadrao;
        parede3Renderer.material = materialparedepadrao;
        parede4Renderer.material = materialparedepadrao;
        parede5Renderer.material = materialparedepadrao;
        parede6Renderer.material = materialparedepadrao;
        parede7Renderer.material = materialparedepadrao;
        parede8Renderer.material = materialparedepadrao;

        chaoRenderer.material = materialchaopadrao;

        forro2Renderer.material = forrocamapadrao;
        forroRenderer.material = forrocamapadrao;

        cama1Renderer.material = materialchaopadrao;
        cama2Renderer.material = materialchaopadrao;

        realista = 0;
        mono = 0;
        hyperpop = 0;
    }

    public void TexturasMono()
    {
        portaRenderer.material.mainTexture = portamono;

        parede1Renderer.material = materialparedemono;
        parede2Renderer.material = materialparedemono;
        parede3Renderer.material = materialparedemono;
        parede4Renderer.material = materialparedemono;
        parede5Renderer.material = materialparedemono;
        parede6Renderer.material = materialparedemono;
        parede7Renderer.material = materialparedemono;
        parede8Renderer.material = materialparedemono;

        chaoRenderer.material = materialchaomono;

        forro2Renderer.material = forrocamamono;
        forroRenderer.material = forrocamamono;

        cama1Renderer.material = materialchaomono;
        cama2Renderer.material = materialchaomono;

        mono = 1;
        realista = 0;
        hyperpop = 0;
    }

    public void Texturashyperpop()
    {
        portaRenderer.material.mainTexture = portahyperpop;

        parede1Renderer.material = materialparedehyperpop;
        parede2Renderer.material = materialparedehyperpop;
        parede3Renderer.material = materialparedehyperpop;
        parede4Renderer.material = materialparedehyperpop;
        parede5Renderer.material = materialparedehyperpop;
        parede6Renderer.material = materialparedehyperpop;
        parede7Renderer.material = materialparedehyperpop;
        parede8Renderer.material = materialparedehyperpop;

        chaoRenderer.material = materialchaohyperpop;

        forro2Renderer.material = forrocamahyperpop;
        forroRenderer.material = forrocamahyperpop;

        cama1Renderer.material = materialchaohyperpop;
        cama2Renderer.material = materialchaohyperpop;

        mono = 0;
        realista = 0;
        hyperpop = 1;
    }

    public void TravarControle(bool estado)
    {
        travarCamera = estado;
        moveble = !estado;
    }
}