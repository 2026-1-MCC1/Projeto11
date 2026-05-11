using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Unity.VisualScripting;

public class HUDTutorial : MonoBehaviour
{
    public GameObject TutorialPanel;
    public GameObject CaixaDeTexto;
    public GameObject CaixaDeTextoSistema;
    public TextMeshProUGUI TextoIntrodução;

    public bool tutorialOneOff = false;

    private HUDManager hudManager;
    private Player player;

    public float velocidade = 0.05f;
    public string textoCompleto;

    public Image bocaaberta;
    public bool bocaabertaoneoff = false;
    public Image bocafechada;
    public Image bocaabertaconfig;
    public Image bocafechadaconfig;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<Player>();
        hudManager = FindAnyObjectByType<HUDManager>();
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public IEnumerator StartTutorial()
    {
        hudManager.celularidle.gameObject.SetActive(false);
        CaixaDeTextoSistema.SetActive(false);
        player.moveble = false;
        tutorialOneOff = true;
        TutorialPanel.SetActive(true);
        CaixaDeTexto.SetActive(true);
        textoCompleto = "Caramba, eu preciso tomar um rumo na minha vida, ouvi dizer que programação da muito dinheiro, acho que vou estudar alguma coisa sobre isso";
        yield return StartCoroutine(EscreverTexto(textoCompleto));
        //espera o input enter para continuar
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        textoCompleto = "Mas por onde eu começo? Acho que vou dar uma olhada no meu computador";
        yield return StartCoroutine(EscreverTexto(textoCompleto));
        //espera o input enter para continuar
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        tutorialOneOff = false;
        TutorialPanel.SetActive(false);
        CaixaDeTexto.SetActive(false);
        hudManager.primeiravez = false;
        player.moveble = true;
        hudManager.celularidle.gameObject.SetActive(true);
    }

    public IEnumerator IntroducaoComputador()
    {
        CaixaDeTextoSistema.SetActive(false);
        hudManager.celularidle.gameObject.SetActive(false);
        player.moveble = false;
        tutorialOneOff = true;
        TutorialPanel.SetActive(true);
        CaixaDeTexto.SetActive(true);
        textoCompleto = "Opa parece que eu tenho o jeito para isso, toda vez que mecho um pouco nele eu pareco ganhar mais conhecimento";
        yield return StartCoroutine(EscreverTexto(textoCompleto));
        //espera o input enter para continuar   
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        textoCompleto = "Só me pergunto o que eu posso fazer com esse conhecimento...";
        yield return StartCoroutine(EscreverTexto(textoCompleto));
        //espera o input enter para continuar
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        tutorialOneOff = false;
        TutorialPanel.SetActive(false);
        CaixaDeTexto.SetActive(false);
        player.moveble = true;  
        hudManager.celularidle.gameObject.SetActive(true);
    }

    public IEnumerator IntroducaoCelular()
    {
        CaixaDeTextoSistema.SetActive(false);
        player.moveble = false;
        tutorialOneOff = true;
        TutorialPanel.SetActive(true);
        CaixaDeTexto.SetActive(true);
        textoCompleto = "Esse é meu celular, eu instalei alguns apps hoje mais cedo, talvez eles me ajudam a aprender mais sobre programação";
        yield return StartCoroutine(EscreverTexto(textoCompleto));
        //espera o input enter para continuar
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        CaixaDeTexto.SetActive(false);
        CaixaDeTextoSistema.SetActive(true);
        textoCompleto = "Utilize o celular como seu menu, aqui você poderá comprar upgrades, classes e customizações para seu quarto";
        yield return StartCoroutine(EscreverTextoConfig(textoCompleto));
        //espera o input enter para continuar
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        CaixaDeTextoSistema.SetActive(false);
        tutorialOneOff = false;
        TutorialPanel.SetActive(false);
        CaixaDeTexto.SetActive(false);
        player.moveble = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public IEnumerator IntroducaoUpgrades()
    {
        CaixaDeTextoSistema.SetActive(false);
        Cursor.visible = false;
        player.moveble = false;
        tutorialOneOff = true;
        TutorialPanel.SetActive(true);
        CaixaDeTexto.SetActive(true);
        textoCompleto = "Parece que eu posso usar meu conhecimento para comprar alguns upgrades";
        yield return StartCoroutine(EscreverTexto(textoCompleto));
        //espera o input enter para continuar
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        textoCompleto = "Mas o que esses upgrades fazem? Vou comprar um para testar";
        yield return StartCoroutine(EscreverTexto(textoCompleto));
        //espera o input enter para continuar
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        tutorialOneOff = false;
        TutorialPanel.SetActive(false);
        CaixaDeTexto.SetActive(false);
        player.moveble = true;
        Cursor.visible = true;
    }

    public IEnumerator IntroducaoMultiplicador()
    {
        CaixaDeTextoSistema.SetActive(false);
        player.moveble = false;
        tutorialOneOff = true;
        TutorialPanel.SetActive(true);
        CaixaDeTexto.SetActive(true);
        textoCompleto = "Humm parece que esse upgrade aumenta a quantidade de conhecimento que eu ganho por clique, isso pode ser util";
        yield return StartCoroutine(EscreverTexto(textoCompleto));
        //espera o input enter para continuar
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        textoCompleto = "Me disseram que uma linguagem de programação com um nome de café é especialista nesses multiplciadores, parece interessante";
        yield return StartCoroutine(EscreverTexto(textoCompleto));
        //espera o input enter para continuar
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        tutorialOneOff = false;
        TutorialPanel.SetActive(false);
        CaixaDeTexto.SetActive(false);
        player.moveble = true;
    }

    public IEnumerator IntroducaoAutoClicker()
    {
        CaixaDeTextoSistema.SetActive(false);
        player.moveble = false;
        tutorialOneOff = true;
        TutorialPanel.SetActive(true);
        CaixaDeTexto.SetActive(true);
        textoCompleto = "Este aqui parece me dar um pouco de conhecimento a cada segundo que se passa, parece bom para quando eu estiver ocupado fazendo outras coisas";
        yield return StartCoroutine(EscreverTexto(textoCompleto));
        //espera o input enter para continuar
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        textoCompleto = "Não sei por que essas automações são relacionadas a uma linguagem com nome de cobra, mas tudo bem";
        yield return StartCoroutine(EscreverTexto(textoCompleto));
        //espera o input enter para continuar
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        tutorialOneOff = false;
        TutorialPanel.SetActive(false);
        CaixaDeTexto.SetActive(false);
        player.moveble = true;
    }

    public IEnumerator IntroducaoLimite()
    {
        CaixaDeTextoSistema.SetActive(false);
        player.moveble = false;
        tutorialOneOff = true;
        TutorialPanel.SetActive(true);
        CaixaDeTexto.SetActive(true);
        textoCompleto = "Parece que esse aqui organiza um pouco melhor meu conhecimento, melhorando meu limite de conhecimento";
        yield return StartCoroutine(EscreverTexto(textoCompleto));
        //espera o input enter para continuar
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        textoCompleto = "Me dissera sobre uma linguagem sem limites, ela é utilizada pra fazer jogos, mas parece um palavrão censurado, estranho";
        yield return StartCoroutine(EscreverTexto(textoCompleto));
        //espera o input enter para continuar
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        tutorialOneOff = false;
        TutorialPanel.SetActive(false);
        CaixaDeTexto.SetActive(false);
        player.moveble = true;
    }

    public IEnumerator IntroducaoNoite()
    {
        CaixaDeTextoSistema.SetActive(false);
        player.moveble = false;
        tutorialOneOff = true;
        TutorialPanel.SetActive(true);
        CaixaDeTexto.SetActive(true);
        textoCompleto = "Como assim chance de noite? O que isso tem a ver com programação?";
        yield return StartCoroutine(EscreverTexto(textoCompleto));
        //espera o input enter para continuar
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        textoCompleto = "Bom, dizem que programadores são noturnos, talvez isso me ajude em algo a noite";
        yield return StartCoroutine(EscreverTexto(textoCompleto));
        //espera o input enter para continuar
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        tutorialOneOff = false;
        TutorialPanel.SetActive(false);
        CaixaDeTexto.SetActive(false);
        player.moveble = true;
    }
    
    public IEnumerator IntroducaoEventoNoite()
    {
        hudManager.AlternarHUD();
        hudManager.celularidle.gameObject.SetActive(false);
        CaixaDeTextoSistema.SetActive(false);
        player.moveble = false;
        tutorialOneOff = true;
        TutorialPanel.SetActive(true);
        CaixaDeTexto.SetActive(true);
        hudManager.celularidle.gameObject.SetActive(false);
        textoCompleto = "Caramba que cansaço, finalmente a noite chegou, será que eu deveria dormir?";
        yield return StartCoroutine(EscreverTexto(textoCompleto));
        //espera o input enter para continuar
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        textoCompleto = "Se bem que eu poderia tomar um cafézinho e aproveitar a noite toda enquanto eu programo, me disseram que de noite você fica ate mais produtivo";
        yield return StartCoroutine(EscreverTexto(textoCompleto));
        //espera o input enter para continuar
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        textoCompleto = "Vou la na minha cafeteira tomar um café e aproveitar a noite!";
        yield return StartCoroutine(EscreverTexto(textoCompleto));
        //espera o input enter para continuar
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        CaixaDeTexto.SetActive(false);
        CaixaDeTextoSistema.SetActive(true);
        textoCompleto = "Clique na cafeteira para tomar um café";
        yield return StartCoroutine(EscreverTextoConfig(textoCompleto));
        //espera o input enter para continuar
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        CaixaDeTextoSistema.SetActive(false);
        tutorialOneOff = false;
        TutorialPanel.SetActive(false);
        player.moveble = true;
        hudManager.celularidle.gameObject.SetActive(true);
    }

    public IEnumerator IntroducaoCafeteira()
    {
        hudManager.celularidle.gameObject.SetActive(false);
        CaixaDeTextoSistema.SetActive(false);
        player.moveble = false;
        tutorialOneOff = true;
        TutorialPanel.SetActive(true);
        CaixaDeTexto.SetActive(true);
        textoCompleto = "Humm, esse café é realmente bom, me sinto mais acordado e produtivo, parece que a noite vai ser bem aproveitada";
        yield return StartCoroutine(EscreverTexto(textoCompleto));
        //espera o input enter para continuar
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        CaixaDeTexto.SetActive(false);
        CaixaDeTextoSistema.SetActive(true);
        textoCompleto = "Toda vez que a noite chegar, utilize da cafeteira para ficar acordado, recebendo um bônus por 2 minutos";
        yield return StartCoroutine(EscreverTextoConfig(textoCompleto));
        //espera o input enter para continuar
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        CaixaDeTextoSistema.SetActive(false);
        player.moveble = true;
        tutorialOneOff = false;
        TutorialPanel.SetActive(false);
        hudManager.celularidle.gameObject.SetActive(true);
    }

    public IEnumerator IntroducaoTexturas()
    {
        hudManager.celularidle.gameObject.SetActive(false);
        CaixaDeTextoSistema.SetActive(false);
        player.moveble = false;
        tutorialOneOff = true;
        TutorialPanel.SetActive(true);
        CaixaDeTexto.SetActive(true);
        textoCompleto = "Já estava na hora de uma reforma nesse quarto, acho que vou mudar um pouco dele";
        yield return StartCoroutine(EscreverTexto(textoCompleto));
        //espera o input enter para continuar
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        CaixaDeTexto.SetActive(false);
        CaixaDeTextoSistema.SetActive(true);
        textoCompleto = "Compre um pacote de texturas por 100 pontos, mantendo o ar comico do cenario mas variando o estilo";
        yield return StartCoroutine(EscreverTextoConfig(textoCompleto));
        //espera o input enter para continuar
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        CaixaDeTextoSistema.SetActive(false);
        player.moveble = true;
        tutorialOneOff = false;
        TutorialPanel.SetActive(false);
        hudManager.celularidle.gameObject.SetActive(true); 
    }
    public IEnumerator IntroducaoClasses()
    {
        hudManager.celularidle.gameObject.SetActive(false);
        CaixaDeTextoSistema.SetActive(false);
        player.moveble = false;
        tutorialOneOff = true;
        TutorialPanel.SetActive(true);
        CaixaDeTexto.SetActive(true);
        textoCompleto = "Me falaram muito dessas linguagens de programação, talvez seja uma boa ideia sair das pseudolinguagens";
        yield return StartCoroutine(EscreverTexto(textoCompleto));
        //espera o input enter para continuar
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        textoCompleto = "Mas infelizmente todos os cursos de linguagens que conheço são pagos, mas ja estava na hora de investir um dinheiro";
        yield return StartCoroutine(EscreverTexto(textoCompleto));
        //espera o input enter para continuar
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        CaixaDeTexto.SetActive(false);
        CaixaDeTextoSistema.SetActive(true);
        textoCompleto = "Aqui você terá a opção de comprar classes para seu personagem que expandem mais o SEU jeito de jogar o jogo";
        yield return StartCoroutine(EscreverTextoConfig(textoCompleto));
        //espera o input enter para continuar
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        textoCompleto = "Classes podem ser compradas com moedas pagas, para mais informações veja a seção shop";
        yield return StartCoroutine(EscreverTextoConfig(textoCompleto));
        //espera o input enter para continuar
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        CaixaDeTextoSistema.SetActive(false);
        player.moveble = true;
        tutorialOneOff = false;
        TutorialPanel.SetActive(false);
        hudManager.celularidle.gameObject.SetActive(true); 
        
    } 

    public IEnumerator Introducaomoedapaga()
    {
        hudManager.celularidle.gameObject.SetActive(false);
        CaixaDeTextoSistema.SetActive(false);
        player.moveble = false;
        tutorialOneOff = true;
        TutorialPanel.SetActive(true);
        CaixaDeTexto.SetActive(true);
        textoCompleto = "Aqui será onde posso investir um dinheirinho para poder comprar meus cursos de programação";
        yield return StartCoroutine(EscreverTexto(textoCompleto));
        //espera o input enter para continuar
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        CaixaDeTexto.SetActive(false);
        CaixaDeTextoSistema.SetActive(true);
        textoCompleto = "O jogo YGGD_CODE é um jogo totalmente gratuito financiado somente por microtransações, as moedas pagas não são conseguidas somente pagando";
        yield return StartCoroutine(EscreverTextoConfig(textoCompleto));
        //espera o input enter para continuar
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        textoCompleto = "mas agilizam muito este processo ;)";
        yield return StartCoroutine(EscreverTextoConfig(textoCompleto));
        //espera o input enter para continuar
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        textoCompleto = "Para conseguir moedas pagas não envolvendo pagamento direto, cada moeda paga são 10 mil pontos, e cada classe custam 10 moedas pagas";
        yield return StartCoroutine(EscreverTextoConfig(textoCompleto));
        //espera o input enter para continuar
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        CaixaDeTextoSistema.SetActive(false);
        player.moveble = true;
        tutorialOneOff = false;
        TutorialPanel.SetActive(false);
        hudManager.celularidle.gameObject.SetActive(true); 
    } 

    public IEnumerator IntroducaoRebyrth()
    {
        hudManager.celularidle.gameObject.SetActive(false);
        CaixaDeTextoSistema.SetActive(false);
        player.moveble = false;
        tutorialOneOff = true;
        TutorialPanel.SetActive(true);
        CaixaDeTexto.SetActive(true);
        textoCompleto = "Durante meus estudos ouvi falar de uma tal de Yggdrasil, de como ela possui inúmeras fortunas e como todos os programadores almejam ela";
        yield return StartCoroutine(EscreverTexto(textoCompleto));
        //espera o input enter para continuar
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        textoCompleto = "A arvore de inúmeros frutos de aprendizados e fortunas";  //talvez vou tentar colorir esse texto aqui de alguma forma na hud
        yield return StartCoroutine(EscreverTexto(textoCompleto));
        //espera o input enter para continuar
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        textoCompleto = "Acho que já evolui o suficiente nesse meio tempo, talvez esta na hora de ir atrás dela...";
        yield return StartCoroutine(EscreverTexto(textoCompleto));
        //espera o input enter para continuar
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        CaixaDeTexto.SetActive(false);
        CaixaDeTextoSistema.SetActive(true);
        textoCompleto = "Você chegou muito longe no jogo, nós desenvolvedores do YGGD_CODE agradeçemos solenemente por você ter dedicado seu tempo para experimentar esse projeto";
        yield return StartCoroutine(EscreverTextoConfig(textoCompleto));
        //espera o input enter para continuar
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        textoCompleto = "Você acabou de desbloquear o Rebyrth, clique no poster possuindo 15mil pontos e você alcançará a Yggdrasil ;)";
        yield return StartCoroutine(EscreverTextoConfig(textoCompleto));
        //espera o input enter para continuar
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
    }

    public IEnumerator EscreverTexto(string texto)
    {
        TextoIntrodução.text = "";
        
        bocaaberta.gameObject.SetActive(false);
        bocafechada.gameObject.SetActive(true);

        bocaabertaconfig.gameObject.SetActive(false);
        bocafechadaconfig.gameObject.SetActive(false);

        int contadorLetras = 0;

        foreach (char letra in texto)
        {
            TextoIntrodução.text += letra;

            // ignora espaços
            if (letra != ' ')
            {
                contadorLetras++;

                // troca sprite a cada 2 letras
                if (contadorLetras % 2 == 0)
                {
                    bocaabertaoneoff = !bocaabertaoneoff;

                    bocaaberta.gameObject.SetActive(bocaabertaoneoff);
                    bocafechada.gameObject.SetActive(!bocaabertaoneoff);
                }
            }

            yield return new WaitForSeconds(velocidade);
        }

        // termina com boca fechada
        bocaaberta.gameObject.SetActive(false);
        bocafechada.gameObject.SetActive(true);
    }

    public IEnumerator EscreverTextoConfig(string texto)
    {
        TextoIntrodução.text = "";
        bocaaberta.gameObject.SetActive(false);
        bocafechada.gameObject.SetActive(false);

        bocaabertaconfig.gameObject.SetActive(false);
        bocafechadaconfig.gameObject.SetActive(true);

        int contadorLetras = 0;

        foreach (char letra in texto)
        {
            TextoIntrodução.text += letra;

            // ignora espaços
            if (letra != ' ')
            {
                contadorLetras++;

                // troca sprite a cada 2 letras
                if (contadorLetras % 2 == 0)
                {
                    bocaabertaoneoff = !bocaabertaoneoff;

                    bocaabertaconfig.gameObject.SetActive(bocaabertaoneoff);
                    bocafechadaconfig.gameObject.SetActive(!bocaabertaoneoff);
                }
            }

            yield return new WaitForSeconds(velocidade);
        }

        // termina com boca fechada
        bocaabertaconfig.gameObject.SetActive(false);
        bocafechadaconfig.gameObject.SetActive(true);
    }
}
