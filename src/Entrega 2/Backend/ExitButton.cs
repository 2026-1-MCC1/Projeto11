using UnityEngine;


public class ExitButton : MonoBehaviour
{
    // Esse m�todo ser� chamado quando o bot�o for clicado
    public void ExitGame()
    {
        // Mostra no console que o bot�o foi pressionado (�til no Editor)
        Debug.Log("Saindo do jogo...");

        // Fecha o jogo quando estiver em build
        Application.Quit();

        // Para testes dentro do Editor do Unity
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

