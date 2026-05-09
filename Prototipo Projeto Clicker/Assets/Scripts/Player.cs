using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using System.Collections;

public class Player : MonoBehaviour
{
    //variavel especifica para corrigir o bug do celular
    // Variável para congelar completamente o personagem
    public bool congelarPosicao = false;

    // Guarda a posição travada
    private Vector3 posicaoCongelada;

    // Referências para a câmera e o transform do jogador
    public Transform _transform;
    public Transform cameraTransform;

    Vector2 rotacaoMouse;

    public float sensibilidade;
    public float velocidade = 5.0f;

    ClickSpawner clickSpawner;
    CharacterController characterController;

    // Configurações para o raycast
    public float maxDistance = 10f;
    public LayerMask hitLayers;

    // Variáveis para o sistema de pontos e upgrades
    public int pontos = 0;

    public TextMeshProUGUI textoPontos;
    public TextMeshProUGUI textoPontos2;
    public TextMeshProUGUI textoMultiplicador;

    public int multiplicadorPontos = 1;

    int clicksAuto = 0;
    float tempoAuto = 0f;
    float intervaloAuto = 1f;

    public TextMeshProUGUI textoAutoClick;

    public int pontosMaximos = 500;

    public TextMeshProUGUI textoLimite;

    int custoNoite = 100;

    public TextMeshProUGUI textoCustoNoite;
    public TextMeshProUGUI textoChanceNoite;

    // HUD para mostrar os preços dos upgrades
    public TextMeshProUGUI precoMulti;
    public TextMeshProUGUI precoAuto;
    public TextMeshProUGUI precoLimite;

    int custoMulti;
    int custoAuto;
    int custoLimite;

    // Classes
    public TextMeshProUGUI ClasseTexto;

    public int multiplicadorClasse;
    public int clicksautomaticosclasse;

    int limiteclasse;

    public bool possuirclassepython;
    public bool possuirclassecsharp;
    public bool possuirclassejava;
    public bool possuirclasseholyc;

    // Variáveis para as luzes e a janela
    public Light luzQuarto;
    public Light luzSol;
    public Light luzComputador;

    public Renderer janelaRenderer;

    public int multiplicadorCiclo = 1;

    // Pra travar a câmera no portal
    public bool travarCamera = false;
    public bool moveble = true;
    public bool moverhorizontal = true;

    // Variáveis de posse de textura
    public bool possuirtexturarealista;
    public bool possuirtexturapadrao;
    public bool possuirtexturamonocromatica;
    public bool possuirtexturahyperpop;

    // Materiais padrão
    public Material materialparedepadrao;
    public Material materialchaopadrao;
    public Texture portapadrao;
    public Material forrocamapadrao;
    public Material janeladiapadrao;
    public Material janelanoitepadrao;

    // Materiais realistas
    public Material materialparederealista;
    public Material materialchaorealista;
    public Material janelarealistadia;
    public Material janelarealistanoite;
    public Texture portarealista;
    public Material forrocamarelista;
    public Material janeladiarealista;
    public Material janelanoiterealista;

    int realista;

    // Materiais mono
    int mono;

    public Material materialparedemono;
    public Material materialchaomono;
    public Material janeladiamono;
    public Material janelanoitemono;
    public Texture portamono;
    public Material forrocamamono;

    // Materiais hyperpop
    int hyperpop;

    public Material materialparedehyperpop;
    public Material materialchaohyperpop;
    public Material janeladiahyperpop;
    public Material janelanoitehyperpop;
    public Texture portahyperpop;
    public Material forrocamahyperpop;

    // Renderer dos objetos
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

    // Variáveis para o sistema de dia e noite
    public float tempoNoite = 0f;
    public bool eventoNoiteAtivo = false;
    public float tempoEventoNoite = 0f;

    public bool bonusAtivo = false;
    public float tempoBonus = 0f;

    public float chance = 7f;

    // Compras de moeda paga
    public int moedapaga = 0;
    public TextMeshProUGUI textoMoedaPaga;

    void Start()
    {
        posicaoCongelada = transform.position;
        characterController = GetComponent<CharacterController>();

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
        textoChanceNoite.text = "(N) Chance de Noite: " + chance + "%";

        precoAuto.text = "Preço: " + custoAuto;
        precoMulti.text = "Preço: " + custoMulti;
        precoLimite.text = "Preço: " + custoLimite;

        textoCustoNoite.text = "Preço: " + custoNoite;
        textoMoedaPaga.text = "R$: 0";

        TexturasPadrao();

        janelaRenderer.material = janeladiapadrao;

        clickSpawner = FindFirstObjectByType<ClickSpawner>();

        HUDManager = FindAnyObjectByType<HUDManager>();

        hudmenu = HUDManager.hudmenuoneoff;
    }

    void Update()
    {
        hudmenu = HUDManager.hudmenuoneoff;
        compraoneoff = HUDManager.Upgrade;
        hudconfig = HUDManager.hudconfigoneoff;
        textoPontos.text = "Pontos: " + pontos;
        textoPontos2.text = textoPontos.text;
        
        if (congelarPosicao)
        {
            transform.position = posicaoCongelada;

            if (characterController != null)
            {
            characterController.enabled = false;
            }

        return;
        }
        else
        {
            if (characterController != null && !characterController.enabled)
            {
                characterController.enabled = true;
            }

        posicaoCongelada = transform.position;
        }

        // CAMERA
        if (travarCamera == false)
        {
            Vector2 controleMouse = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            rotacaoMouse = new Vector2(rotacaoMouse.x + controleMouse.x * sensibilidade * Time.deltaTime, rotacaoMouse.y + controleMouse.y * sensibilidade * Time.deltaTime);

            _transform.eulerAngles = new Vector3(_transform.eulerAngles.x, rotacaoMouse.x, _transform.eulerAngles.z);

            rotacaoMouse.y = Mathf.Clamp(rotacaoMouse.y, -80, 80);

            cameraTransform.localEulerAngles = new Vector3(-rotacaoMouse.y,
                                                           cameraTransform.localEulerAngles.y,
                                                           cameraTransform.localEulerAngles.z);
        }

        // MOVIMENTO
        if (moveble == true)
        {
            float moverVertical = Input.GetAxis("Vertical");
            float moverHorizontal = Input.GetAxis("Horizontal");

            Vector3 movimento = new Vector3(moverHorizontal, 0.0f, moverVertical);

            // Normaliza o vetor para evitar movimento mais rápido na diagonal
            if (movimento.magnitude > 1)
            {
                movimento.Normalize();
            }

            // Usa CharacterController para movimentação com colisão
            if (characterController != null && characterController.enabled)
            {
                characterController.Move(transform.TransformDirection(movimento) * velocidade * Time.deltaTime);
            }
        }
        //Compra de itens com teclado
        if (Input.GetKeyDown(KeyCode.H) && (compraoneoff == true) && (HUDManager.hudupgradeoneoff == true)) //trava de segurança
        {
            if (pontos >= custoMulti)
            {
                pontos -= custoMulti;
                multiplicadorPontos += 1;

                Debug.Log("Multiplicador: " + multiplicadorPontos);
                Debug.Log("Pontos restantes: " + pontos);


                textoMultiplicador.text = " (H) Multiplicador: " + (multiplicadorPontos * multiplicadorCiclo * multiplicadorClasse);
                textoPontos.text = "Pontos: " + pontos;
                custoMulti = 25 * multiplicadorPontos;
                precoMulti.text = "Preço: " + custoMulti;
            }
            else
            {
                Debug.Log("Pontos insuficientes!");
            }
        }

        if (Input.GetKeyDown(KeyCode.J) && (compraoneoff == true) && (HUDManager.hudupgradeoneoff == true)) //trava de segurança
        {
            if (pontos >= custoAuto)
            {
                pontos -= custoAuto;
                clicksAuto += 1;
                Debug.Log("Clicks automáticos: " + clicksAuto);
                Debug.Log("Pontos restantes: " + pontos);
                textoPontos.text = "Pontos: " + pontos;
                textoAutoClick.text = " (J) Clicks Automaticos: " + (clicksAuto * clicksautomaticosclasse);
                custoAuto = 10 * clicksAuto;
                precoAuto.text = "Preço: " + custoAuto;
            }
            else
            {
                Debug.Log("Pontos insuficientes!");
            }
        }
        if (Input.GetKeyDown(KeyCode.K) && (compraoneoff == true) && (HUDManager.hudupgradeoneoff == true)) //trava de segurança
        {
            if (pontos >= custoLimite)
            {
                pontos -= custoLimite;
                pontosMaximos += 500 * limiteclasse;
                Debug.Log("Limite aumentado para: " + pontosMaximos);
                Debug.Log("Pontos restantes: " + pontos);
                textoPontos.text = "Pontos: " + pontos;
                textoLimite.text = " (K) Limite: " + (pontosMaximos * limiteclasse);
                custoLimite = pontosMaximos / limiteclasse;
                precoLimite.text = "Preço: " + custoLimite;
            }
            else
            {
                Debug.Log("Pontos insuficientes!");
            }
        }
        if (Input.GetKeyDown(KeyCode.N) && (compraoneoff == true) && (HUDManager.hudupgradeoneoff == true)) //trava de segurança
        {
            if (pontos >= custoNoite)
            {
                pontos -= custoNoite;
                chance ++; // adiciona +1% na chance
                Debug.Log("Chance de noite: " + chance + "%");
                Debug.Log("Pontos restantes: " + pontos);
                textoChanceNoite.text = "(N) Chance de Noite: " + chance + "%";
                textoPontos.text = "Pontos: " + pontos;
                custoNoite += 100; // aumenta o preço em 100
                textoCustoNoite.text = "Preço: " + custoNoite;
            }
            else
            {
                Debug.Log("Pontos insuficientes!");
            }
        }
    
        // CLICK
        if (Input.GetMouseButtonDown(0))
        {
            if ((HUDManager.hudsecundariaoneoff == false) &&
                hudmenu == false &&
                hudconfig == false)
            {
                Ray ray = cameraTransform
                    .GetComponent<Camera>()
                    .ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, maxDistance, hitLayers))
                {
                    Debug.Log("Acertou: " + hit.collider.gameObject.name);

                    // COMPUTADOR
                    if (hit.collider.gameObject.name == "Computador")
                    {
                        StartCoroutine(animacaoClick());
                    }

                    // INTERRUPTOR
                    if (hit.collider.gameObject.name == "Interruptor")
                    {
                        if (luzQuarto.intensity > 0f)
                        {
                            luzQuarto.intensity = 0f;
                        }
                        else
                        {
                            luzQuarto.intensity = 50f;
                        }
                    }

                    // CAFETEIRA
                    if (hit.collider.gameObject.name == "Cafeteira" && eventoNoiteAtivo && bonusAtivo == false)
                    {  
                        Debug.Log("Bônus ativado");
                        bonusAtivo = true;
                        tempoBonus = 0f;
                        tempoEventoNoite = 0f;
                        eventoNoiteAtivo = false;
                        luzSol.intensity = 0f;
                    }
                }
                else
                {
                    Debug.Log("Não acertou nada");
                }
            }
        }
        

        // AUTO CLICK
        if ((HUDManager.hudsecundariaoneoff == false) && hudmenu == false && hudconfig == false)
        {
            tempoAuto += Time.deltaTime;

            if (tempoAuto >= intervaloAuto)
            {
                tempoAuto = 0f;

                pontos += clicksAuto * clicksautomaticosclasse;

                pontos = Mathf.Clamp(pontos, 0, pontosMaximos);

                textoPontos.text = "Pontos: " + pontos;
                textoPontos2.text = textoPontos.text;
            }
        }
        //----------------------------------------------- SEÇÃO DE EVENTO DE NOITE --------------------------------------------------------------
        // Controle do Evento Noite

        if (!eventoNoiteAtivo && !HUDManager.hudsecundariaoneoff && !hudmenu && !hudconfig)
        {
            if (!bonusAtivo)
            {
                tempoNoite += Time.deltaTime;

                if (tempoNoite >= 2f)
                {
                    tempoNoite = 0f;

                    if (Random.Range(0f, 100f) <= chance)
                    {
                        Debug.Log("Evento ativado");

                        eventoNoiteAtivo = true;
                        tempoEventoNoite = 0f;
                        luzSol.intensity = 0f;
                    }
                    else
                    {
                        Debug.Log("Evento não ativado");
                    }
                }
            }
        }
        if (eventoNoiteAtivo == true && bonusAtivo == false && HUDManager.hudsecundariaoneoff == false && hudmenu == false && hudconfig == false)
        {
            tempoEventoNoite += Time.deltaTime;
        }

        if (tempoEventoNoite >= 5f)
        {
            eventoNoiteAtivo = false;
            tempoEventoNoite = 0f;
            Debug.Log("Evento acabou");
            tempoNoite = 0f;
            luzSol.intensity = 500f;
        }
        if (bonusAtivo == true && HUDManager.hudsecundariaoneoff == false && hudmenu == false && hudconfig == false)
        {
            tempoBonus += Time.deltaTime;
            if (tempoBonus >= 10)
            {
                bonusAtivo = false;
                tempoBonus = 0f;
                Debug.Log("Bônus Acabou");
                tempoNoite = 0f;
                tempoEventoNoite = 0f;
                eventoNoiteAtivo = false;
                luzSol.intensity = 500f;
            }
        }


        //----------------------------------------------- SEÇÃO DE DIA E NOITE --------------------------------------------------------------
        // Verifica se ambas as luzes estão apagadas para acender a luz do computador
        if (luzSol.intensity == 0f && luzQuarto.intensity == 0f)
        {
            luzComputador.intensity = 500f;
        }
        else
        {
            luzComputador.intensity = 0f;
        }
        //Texturas da janela dependendo da luz do sol
        if (luzSol.intensity == 0f)
        {
            //coloca a textura dependente do pacote de texturas
            if (realista == 1)
            {
                janelaRenderer.material = janelanoiterealista;
                multiplicadorCiclo = 2; // Dobra o multiplicador de pontos quando a luz do sol estiver apagada
                textoMultiplicador.text = " (H) Multiplicador: " + (multiplicadorPontos * multiplicadorCiclo * multiplicadorClasse);
            }
            else if (mono == 1)
            {
                janelaRenderer.material = janelanoitemono;
                multiplicadorCiclo = 2; // Dobra o multiplicador de pontos quando a luz do sol estiver apagada
                textoMultiplicador.text = " (H) Multiplicador: " + (multiplicadorPontos * multiplicadorCiclo * multiplicadorClasse);
            }
            else if (hyperpop == 1)
            {
                janelaRenderer.material = janelanoitehyperpop;
                multiplicadorCiclo = 2; // Dobra o multiplicador de pontos quando a luz do sol estiver apagada
                textoMultiplicador.text = " (H) Multiplicador: " + (multiplicadorPontos * multiplicadorCiclo * multiplicadorClasse);
            }
            else
            {
                janelaRenderer.material = janelanoitepadrao;
                multiplicadorCiclo = 2; // Dobra o multiplicador de pontos quando a luz do sol estiver apagada
                textoMultiplicador.text = " (H) Multiplicador: " + (multiplicadorPontos * multiplicadorCiclo * multiplicadorClasse);
            }
        }
        else
        {
            //coloca a textura dependente do pacote de texturas
            if (realista == 1)
            {
                janelaRenderer.material = janeladiarealista;
                multiplicadorCiclo = 1; // Restaura o multiplicador de pontos para o normal quando a luz do sol estiver acesa
                textoMultiplicador.text = " (H) Multiplicador: " + (multiplicadorPontos * multiplicadorCiclo * multiplicadorClasse);
            }
            else if (mono == 1)
            {
                janelaRenderer.material = janeladiamono;
                multiplicadorCiclo = 1; // Restaura o multiplicador de pontos para o normal quando a luz do sol estiver acesa
                textoMultiplicador.text = " (H) Multiplicador: " + (multiplicadorPontos * multiplicadorCiclo * multiplicadorClasse);
            }
            else if (hyperpop == 1)
            {
                janelaRenderer.material = janeladiahyperpop;
                multiplicadorCiclo = 1; // Restaura o multiplicador de pontos para o normal quando a luz do sol estiver acesa
                textoMultiplicador.text = " (H) Multiplicador: " + (multiplicadorPontos * multiplicadorCiclo * multiplicadorClasse);
            }
            else
            {
                janelaRenderer.material = janeladiapadrao;
                multiplicadorCiclo = 1; // Restaura o multiplicador de pontos para o normal quando a luz do sol estiver acesa
                textoMultiplicador.text = " (H) Multiplicador: " + (multiplicadorPontos * multiplicadorCiclo * multiplicadorClasse);
            }
        }

        // -------------------------------------------------------------- SEÇÃO DE TEXTURAS --------------------------------------------------------------
        //Compra de texturas e etc
        if (hudmenu == false && HUDManager.hudsecundariaoneoff == true && HUDManager.hudtexturaoneoff == true) //trava segurança
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (possuirtexturarealista == true)
                {
                    TexturasRealistas();
                }
                else if (possuirtexturarealista == false && pontos >= 100)
                {
                    TexturasRealistas();
                    pontos -= 100;
                    possuirtexturarealista = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                TexturasPadrao();                           
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (possuirtexturamonocromatica)
                {
                    TexturasMono();
                }
                else if (possuirtexturamonocromatica == false && pontos >=100)
                {
                    TexturasMono();
                    pontos -= 100;
                    possuirtexturamonocromatica = true;
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (possuirtexturahyperpop)
                {
                    Texturashyperpop();
                }
                else if (possuirtexturahyperpop == false && pontos >= 100)
                {
                    Texturashyperpop();
                    pontos -= 100;
                    possuirtexturahyperpop = true;
                }
            }
        }

        // -------------------------------------------------------------- SEÇÃO DE CLASSES --------------------------------------------------------------
        //Compra de classes
        if (hudmenu == false && HUDManager.hudsecundariaoneoff == true && HUDManager.hudclasseoneoff == true) //trava segurança
        {
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                ClasseTexto.text = "Classe: Nenhuma";
                //trava de segurança para evitar bugs envolvendo o limite de pontos ao trocar de classe, dividindo o limite atual pelo limite da classe anterior
                if (limiteclasse == 3)
                {
                    pontosMaximos = pontosMaximos / limiteclasse;
                }
                else if (limiteclasse == 2)
                {
                    pontosMaximos = pontosMaximos / limiteclasse;
                }
                //atributos da classe
                multiplicadorClasse = 1;
                clicksautomaticosclasse = 1;
                limiteclasse = 1;
                //alterando a hud ao trocar de classe
                textoLimite.text = " (K) Limite: " + (pontosMaximos * limiteclasse);
                textoAutoClick.text = " (J) Clicks Automaticos: " + (clicksAuto * clicksautomaticosclasse);
                pontosMaximos = pontosMaximos * limiteclasse;
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                if (possuirclassepython == true)
                {
                    ClasseTexto.text = "Classe: Python";
                    if (limiteclasse == 3)
                    {
                        pontosMaximos = pontosMaximos / limiteclasse;
                    }
                    else if (limiteclasse == 2)
                    {
                        pontosMaximos = pontosMaximos / limiteclasse;
                    }
                    multiplicadorClasse = 1;
                    clicksautomaticosclasse = 5;
                    limiteclasse = 1;
                    textoLimite.text = " (K) Limite: " + (pontosMaximos * limiteclasse);
                    textoAutoClick.text = " (J) Clicks Automaticos: " + (clicksAuto * clicksautomaticosclasse);
                    pontosMaximos = pontosMaximos * limiteclasse;
                }
                else if (possuirclassepython == false && moedapaga >= 10)
                {
                    ClasseTexto.text = "Classe: Python";
                    if (limiteclasse == 3)
                    {
                        pontosMaximos = pontosMaximos / limiteclasse;
                    }
                    else if (limiteclasse == 2)
                    {
                        pontosMaximos = pontosMaximos / limiteclasse;
                    }
                    multiplicadorClasse = 1;
                    clicksautomaticosclasse = 5;
                    limiteclasse = 1;
                    textoLimite.text = " (K) Limite: " + (pontosMaximos * limiteclasse);
                    textoAutoClick.text = " (J) Clicks Automaticos: " + (clicksAuto * clicksautomaticosclasse);
                    pontosMaximos = pontosMaximos * limiteclasse;
                    moedapaga -= 10;
                    possuirclassepython = true;
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                if (possuirclassecsharp == true)
                {
                    ClasseTexto.text = "Classe: C#";
                    if (limiteclasse == 3)
                    {
                        pontosMaximos = pontosMaximos / limiteclasse;
                    }
                    else if (limiteclasse == 2)
                    {
                        pontosMaximos = pontosMaximos / limiteclasse;
                    }
                    multiplicadorClasse = 1;
                    clicksautomaticosclasse = 1;
                    limiteclasse = 3;
                    textoLimite.text = " (K) Limite: " + (pontosMaximos * limiteclasse);
                    textoAutoClick.text = " (J) Clicks Automaticos: " + (clicksAuto * clicksautomaticosclasse);
                    pontosMaximos = pontosMaximos * limiteclasse;
                }
                else if (possuirclassecsharp == false && moedapaga >= 10)
                {
                    ClasseTexto.text = "Classe: C#";
                    if (limiteclasse == 3)
                    {
                        pontosMaximos = pontosMaximos / limiteclasse;
                    }
                    else if (limiteclasse == 2)
                    {
                        pontosMaximos = pontosMaximos / limiteclasse;
                    }
                    multiplicadorClasse = 1;
                    clicksautomaticosclasse = 1;
                    limiteclasse = 3;
                    textoLimite.text = " (K) Limite: " + (pontosMaximos * limiteclasse);
                    textoAutoClick.text = " (J) Clicks Automaticos: " + (clicksAuto * clicksautomaticosclasse);
                    pontosMaximos = pontosMaximos * limiteclasse;
                    moedapaga -= 10;
                    possuirclassecsharp = true;

                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                if (possuirclassejava)
                {
                    ClasseTexto.text = "Classe: Java";
                    if (limiteclasse == 3)
                    {
                        pontosMaximos = pontosMaximos / limiteclasse;
                    }
                    else if (limiteclasse == 2)
                    {
                        pontosMaximos = pontosMaximos / limiteclasse;
                    }
                    multiplicadorClasse = 5;
                    clicksautomaticosclasse = 1;
                    limiteclasse = 1;
                    textoLimite.text = " (K) Limite: " + (pontosMaximos * limiteclasse);
                    textoAutoClick.text = " (J) Clicks Automaticos: " + (clicksAuto * clicksautomaticosclasse);
                    pontosMaximos = pontosMaximos * limiteclasse;
                }
                else if (possuirclassejava == false && moedapaga >= 10)
                {
                    ClasseTexto.text = "Classe: Java";
                    if (limiteclasse == 3)
                    {
                        pontosMaximos = pontosMaximos / limiteclasse;
                    }
                    else if (limiteclasse == 2)
                    {
                        pontosMaximos = pontosMaximos / limiteclasse;
                    }
                    multiplicadorClasse = 5;
                    clicksautomaticosclasse = 1;
                    limiteclasse = 1;
                    textoLimite.text = " (K) Limite: " + (pontosMaximos * limiteclasse);
                    textoAutoClick.text = " (J) Clicks Automaticos: " + (clicksAuto * clicksautomaticosclasse);
                    pontosMaximos = pontosMaximos * limiteclasse;
                    moedapaga -= 10;
                    possuirclassejava = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                if (possuirclasseholyc)
                {
                    ClasseTexto.text = "Classe: Holy C";
                    if (limiteclasse == 3)
                    {
                        pontosMaximos = pontosMaximos / limiteclasse;
                    }
                    else if (limiteclasse == 2)
                    {
                        pontosMaximos = pontosMaximos / limiteclasse;
                    }
                    multiplicadorClasse = 2;
                    clicksautomaticosclasse = 2;
                    limiteclasse = 2;
                    textoLimite.text = " (K) Limite: " + (pontosMaximos * limiteclasse);
                    textoAutoClick.text = " (J) Clicks Automaticos: " + (clicksAuto * clicksautomaticosclasse);
                    pontosMaximos = pontosMaximos * limiteclasse;
                }
                else if (possuirclasseholyc == false && moedapaga >= 10)
                {
                    ClasseTexto.text = "Classe: Holy C";
                    if (limiteclasse == 3)
                    {
                        pontosMaximos = pontosMaximos / limiteclasse;
                    }
                    else if (limiteclasse == 2)
                    {
                        pontosMaximos = pontosMaximos / limiteclasse;
                    }
                    multiplicadorClasse = 2;
                    clicksautomaticosclasse = 2;
                    limiteclasse = 2;
                    textoLimite.text = " (K) Limite: " + (pontosMaximos * limiteclasse);
                    textoAutoClick.text = " (J) Clicks Automaticos: " + (clicksAuto * clicksautomaticosclasse);
                    pontosMaximos = pontosMaximos * limiteclasse;
                }
            }

            clickSpawner.multiplicador = multiplicadorPontos * multiplicadorCiclo * multiplicadorClasse;

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

    IEnumerator animacaoClick()
{
    Animator animClick = textoPontos2.GetComponent<Animator>();

    Debug.Log(animClick);

    if (animClick != null)
    {
        Debug.Log("Animator encontrado");

        animClick.enabled = true;

        animClick.Rebind();
        animClick.Update(0f);

        animClick.Play("animacaopontos");

        Debug.Log("Tentou tocar animação");
    }
    else
    {
        Debug.Log("Animator NULL");
    }

    pontos += multiplicadorPontos * multiplicadorCiclo * multiplicadorClasse;

    pontos = Mathf.Clamp(pontos, 0, pontosMaximos);

    textoPontos.text = "Pontos: " + pontos;
    textoPontos2.text = textoPontos.text;

    yield return null;
}
}