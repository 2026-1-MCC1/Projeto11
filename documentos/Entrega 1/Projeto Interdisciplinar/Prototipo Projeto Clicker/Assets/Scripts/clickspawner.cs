using UnityEngine;

public class ClickSpawner : MonoBehaviour
{
    public GameObject prefabTextoFlutuante;
    public GameObject prefabParticula;
    // Essa função roda sozinha quando você clica em algo que tem Collider
    void OnMouseDown()
    {
        Vector3 posicaoDeNascimento = transform.position + Vector3.up * 0f;

        if (prefabTextoFlutuante != null)
        {
            GameObject novoTexto = Instantiate(prefabTextoFlutuante, posicaoDeNascimento, Quaternion.identity);
            novoTexto.transform.LookAt(Camera.main.transform);
            novoTexto.transform.Rotate(0, 180, 0);
        }

        if (prefabParticula != null)
        {
            Instantiate(prefabParticula, posicaoDeNascimento, Quaternion.identity);
        }
    }
}

