using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Player : MonoBehaviour
{
    public Transform _transform;
    public Transform cameraTransform;

    Vector2 rotacaoMouse;
    public int sensibilidade;
    public float velocidade = 5.0f;

    public float maxDistance = 10f;
    public LayerMask hitLayers;

    int pontos = 0;

    public TextMeshProUGUI textoPontos;
    public TextMeshProUGUI textoMultiplicador;
    int multiplicadorPontos = 1;

    int clicksAuto = 0;
    float tempoAuto = 0f;
    float intervaloAuto = 1f;
    public TextMeshProUGUI textoAutoClick;

    int pontosMaximos = 100;
    public TextMeshProUGUI textoLimite;


    public TextMeshPro precoMulti;
    public TextMeshPro precoAuto;
    public TextMeshPro precoLimite;
    int custoMulti;
    int custoAuto;
    int custoLimite;

    public Light luz;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Screen.fullScreen = true;
        custoMulti = 25 * multiplicadorPontos;
        custoAuto = 20;
        custoLimite = pontosMaximos;
    }

    void Update()
    { //Camera
        Vector2 controleMouse = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        rotacaoMouse = new Vector2(rotacaoMouse.x + controleMouse.x * sensibilidade * Time.deltaTime, rotacaoMouse.y + controleMouse.y * sensibilidade * Time.deltaTime);

        _transform.eulerAngles = new Vector3(_transform.eulerAngles.x, rotacaoMouse.x, _transform.eulerAngles.z);

        rotacaoMouse.y = Mathf.Clamp(rotacaoMouse.y, -80, 80);

        cameraTransform.localEulerAngles = new Vector3(-rotacaoMouse.y,
                                                       cameraTransform.localEulerAngles.y,
                                                       cameraTransform.localEulerAngles.z);

        //Movimenta��o
        float moverHorizontal = Input.GetAxis("Horizontal");
        float moverVertical = Input.GetAxis("Vertical");

        Vector3 movimento = new Vector3(moverHorizontal, 0.0f, moverVertical);


        transform.Translate(movimento * velocidade * Time.deltaTime);


        //Compra de itens com teclado
        if (Input.GetKey(KeyCode.H))
        {
            if (pontos >= custoMulti)
            {
                pontos -= custoMulti;
                multiplicadorPontos++;

                Debug.Log("Multiplicador: " + multiplicadorPontos);
                Debug.Log("Pontos restantes: " + pontos);


                textoMultiplicador.text = " (H) Multiplicador: " + multiplicadorPontos;
                textoPontos.text = "Pontos: " + pontos;
                custoMulti = 25 * multiplicadorPontos;
                precoMulti.text = "Preço: " + custoMulti;

            }
            else
            {
                Debug.Log("Pontos insuficientes!");
            }
        }

        if (Input.GetKey(KeyCode.J))
        {
            if (pontos >= custoAuto)
            {
                pontos -= custoAuto;
                clicksAuto++;
                Debug.Log("Clicks automáticos: " + clicksAuto);
                Debug.Log("Pontos restantes: " + pontos);
                textoPontos.text = "Pontos: " + pontos;
                textoAutoClick.text = " (J) Clicks Automaticos: " + clicksAuto;
                custoAuto = 20 * clicksAuto;
                precoAuto.text = "Preço: " + custoAuto;
            }
            else
            {
                Debug.Log("Pontos insuficientes!");
            }
        }

        if (Input.GetKey(KeyCode.K))
        {
            if (pontos >= custoLimite)
            {
                pontos -= custoLimite;
                pontosMaximos += 50;
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
                    pontos += multiplicadorPontos;
                    pontos = Mathf.Clamp(pontos, 0, pontosMaximos);
                    Debug.Log("Pontos: " + pontos);
                    textoPontos.text = "Pontos: " + pontos;
                }
                if (hit.collider.gameObject.name == "Interruptor")
                {
                    if (luz.intensity > 0)
                    {
                        luz.intensity = 0f;
                    }
                    else
                    {
                        luz.intensity = 50f;
                    }
                }
                else
                {
                    Debug.Log("Não acertou nada");
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

        }
    }
}
