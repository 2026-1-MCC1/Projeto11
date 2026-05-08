using UnityEngine;

using UnityEngine;

public class ExitButton : MonoBehaviour
{
    // Esse método será chamado quando o botão for clicado
    public void ExitGame()
    {
        // Mostra no console que o botão foi pressionado (útil no Editor)
        Debug.Log("Saindo do jogo...");

        // Fecha o jogo quando estiver em build
        Application.Quit();

        // Para testes dentro do Editor do Unity
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

