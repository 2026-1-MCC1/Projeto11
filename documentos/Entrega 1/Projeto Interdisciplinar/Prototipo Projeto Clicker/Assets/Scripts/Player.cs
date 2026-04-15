using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Player : MonoBehaviour
{
    // Referências para a câmera e o transform do jogador
    public Transform _transform;
    public Transform cameraTransform;
    Vector2 rotacaoMouse;
    public int sensibilidade;
    public float velocidade = 5.0f;

    // Configurações para o raycast
    public float maxDistance = 10f;
    public LayerMask hitLayers;

    // Variáveis para o sistema de pontos e upgrades
    int pontos = 0;
    public TextMeshProUGUI textoPontos;
    public TextMeshProUGUI textoMultiplicador;
    int multiplicadorPontos = 1;
    int clicksAuto = 0;
    float tempoAuto = 0f;
    float intervaloAuto = 1f;
    public TextMeshProUGUI textoAutoClick;
    int pontosMaximos = 500;
    public TextMeshProUGUI textoLimite;

    //HUD para mostrar os preços dos upgrades
    public TextMeshPro precoMulti;
    public TextMeshPro precoAuto;
    public TextMeshPro precoLimite;
    int custoMulti;
    int custoAuto;
    int custoLimite;

    // Variáveis para as luzes e a janela
    public Light luzQuarto;
    public Light luzSol;
    public Light luzComputador;
    public Renderer janelaRenderer;


    int multiplicadorCiclo = 1;

    //Pra travar a câmera no portal
    public bool travarCamera = false;
    public bool moveble = true;
    public bool moverhorizontal = true;

    //materiais padrao
    public Material materialparedepadrao;
    public Material materialchaopadrao;
    public Texture portapadrao;
    public Material forrocamapadrao;
    public Material janeladiapadrao;
    public Material janelanoitepadrao;

    //materais realistas
    public Material materialparederealista;
    public Material materialchaorealista;
    public Material janelarealistadia;
    public Material janelarealistanoite;
    public Texture portarealista;
    public Material forrocamarelista;
    public Material janeladiarealista;
    public Material janelanoiterealista;
    int realista;

    //renderer dos objetos
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

    void Start()
    // Configurações iniciais do cursor e tela cheia
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Screen.fullScreen = true;
        // Configurações iniciais dos custos dos upgrades
        custoMulti = 25 * multiplicadorPontos;
        custoAuto = 10;
        custoLimite = pontosMaximos;
        textoMultiplicador.text = " (H) Multiplicador: " + (multiplicadorPontos * multiplicadorCiclo);
        textoAutoClick.text = " (J) Clicks Automaticos: " + clicksAuto;
        textoLimite.text = " (K) Limite: " + pontosMaximos;
        precoAuto.text = "Preço: " + custoAuto;
        precoMulti.text = "Preço: " + custoMulti;
        precoLimite.text = "Preço: " + custoLimite;
        //padroniza as texturas no começo do jogo
        TexturasPadrao();
        janelaRenderer.material = janeladiapadrao;
    }

    void Update()
    { //Camera

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

        //Movimenta  o
        if (moveble == true)
        {
            float moverVertical = Input.GetAxis("Vertical");
            float moverHorizontal = Input.GetAxis("Horizontal");

            Vector3 movimento = new Vector3(moverHorizontal, 0.0f, moverVertical);


            transform.Translate(movimento * velocidade * Time.deltaTime);
        }
        //Compra de itens com teclado
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (pontos >= custoMulti)
            {
                pontos -= custoMulti;
                multiplicadorPontos++;

                Debug.Log("Multiplicador: " + multiplicadorPontos);
                Debug.Log("Pontos restantes: " + pontos);


                textoMultiplicador.text = " (H) Multiplicador: " + (multiplicadorPontos * multiplicadorCiclo);
                textoPontos.text = "Pontos: " + pontos;
                custoMulti = 25 * multiplicadorPontos;
                precoMulti.text = "Preço: " + custoMulti;
                precoLimite.text = "Preço: " + custoLimite;

            }
            else
            {
                Debug.Log("Pontos insuficientes!");
            }
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (pontos >= custoAuto)
            {
                pontos -= custoAuto;
                clicksAuto++;
                Debug.Log("Clicks automáticos: " + clicksAuto);
                Debug.Log("Pontos restantes: " + pontos);
                textoPontos.text = "Pontos: " + pontos;
                textoAutoClick.text = " (J) Clicks Automaticos: " + clicksAuto;
                custoAuto = 10 * clicksAuto;
                precoAuto.text = "Preço: " + custoAuto;
            }
            else
            {
                Debug.Log("Pontos insuficientes!");
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            if (pontos >= custoLimite)
            {
                pontos -= custoLimite;
                pontosMaximos += 500;
                Debug.Log("Novo limite: " + pontosMaximos);
                Debug.Log("Pontos restantes: " + pontos);
                textoLimite.text = " (K) Limite: " + pontosMaximos;
                textoPontos.text = "Pontos: " + pontos;
                custoLimite = pontosMaximos;
                precoLimite.text = "Preço: " + custoLimite;
            }
            else
            {
                Debug.Log("Pontos insuficientes!");
            }
        }

        //Compra de itens com clique
        if (Input.GetMouseButtonDown(0)) // só dispara quando clicar
        {
            Ray ray = cameraTransform.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance, hitLayers))
            {
                Debug.Log("Acertou: " + hit.collider.gameObject.name);

                if (hit.collider.gameObject.name == "Computador")
                {
                    pontos += multiplicadorPontos * multiplicadorCiclo;
                    pontos = Mathf.Clamp(pontos, 0, pontosMaximos);
                    Debug.Log("Pontos: " + pontos);
                    textoPontos.text = "Pontos: " + pontos;
                }

                // Verifica se o objeto clicado é o interruptor e alterna a luz do quarto
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
                else
                {
                    Debug.Log("Não acertou nada");
                }

            }

        }
        tempoAuto += Time.deltaTime;
        if (tempoAuto >= intervaloAuto)
        {
            tempoAuto = 0f;

            pontos += clicksAuto;
            pontos = Mathf.Clamp(pontos, 0, pontosMaximos);
            textoPontos.text = "Pontos: " + pontos;
        }

        // Verifica se ambas as luzes estão apagadas para acender a luz do computador
        if (luzSol.intensity == 0f && luzQuarto.intensity == 0f)
        {
            luzComputador.intensity = 500f;
        }
        else
        {
            luzComputador.intensity = 0f;
        }

        // Só template para mim mesmo como interruptor de sol 
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (luzSol.intensity > 0f)
            {
                luzSol.intensity = 0f;
            }
            else
            {
                luzSol.intensity = 500f;
            }
        }

        //Texturas da janela dependendo da luz do sol
        if (luzSol.intensity == 0f)
        {
            //coloca a textura dependente do pacote de texturas
            if (realista == 0)
            {
                janelaRenderer.material = janelanoitepadrao;
                multiplicadorCiclo = 2; // Dobra o multiplicador de pontos quando a luz do sol estiver apagada
                textoMultiplicador.text = " (H) Multiplicador: " + (multiplicadorPontos * multiplicadorCiclo);
            }
            else
            {
                janelaRenderer.material = janelanoiterealista;
                multiplicadorCiclo = 2; // Dobra o multiplicador de pontos quando a luz do sol estiver apagada
                textoMultiplicador.text = " (H) Multiplicador: " + (multiplicadorPontos * multiplicadorCiclo);
            }
        }
        else
        {
            //coloca a textura dependente do pacote de texturas
            if (realista == 0)
            {
                janelaRenderer.material = janeladiapadrao;
                multiplicadorCiclo = 1; // Restaura o multiplicador de pontos para o normal quando a luz do sol estiver acesa
                textoMultiplicador.text = " (H) Multiplicador: " + (multiplicadorPontos * multiplicadorCiclo);
            }
            else
            {
                janelaRenderer.material = janeladiarealista;
                multiplicadorCiclo = 1; // Restaura o multiplicador de pontos para o normal quando a luz do sol estiver acesa
                textoMultiplicador.text = " (H) Multiplicador: " + (multiplicadorPontos * multiplicadorCiclo);
            }
        }

        //Compra de texturas e etc
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TexturasRealistas();
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            TexturasPadrao();
        }
    }
    public void ResetarCamera()
    {
        rotacaoMouse = Vector2.zero;

        // Zera rotação do player (eixo Y)
        _transform.eulerAngles = Vector3.zero;

        // Zera rotação da câmera
        cameraTransform.localEulerAngles = Vector3.zero;
    }


    public void TexturasRealistas()
    {
        //funcao para colocar as texturas realistas
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

    }

    public void TexturasPadrao()
    {
        //funcao para colocar as texturas padrao
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
    }


}



