using UnityEngine;

public class ClickSpawner : MonoBehaviour
{
    [Header("Arraste o Cubo Azul do Texto aqui")]
    public GameObject prefabTextoFlutuante;

    // Essa função roda sozinha quando você clica em algo que tem Collider
    void OnMouseDown()
    {
        // 1. Criar o texto um pouco acima do computador
        // O Vector3(0, 2, 0) joga ele 2 metros para cima
        Vector3 posicaoDeNascimento = transform.position + Vector3.up * 2f;

        // 2. Faz o "carimbo" (Instantiate)
        GameObject novoTexto = Instantiate(prefabTextoFlutuante, posicaoDeNascimento, Quaternion.identity);

        // 3. (Opcional) Se o texto estiver de costas, essa linha faz ele olhar para a câmera
        novoTexto.transform.LookAt(Camera.main.transform);
        novoTexto.transform.Rotate(0, 180, 0);

        Debug.Log("Cliquei no computador e criei o texto!");
    }
}
