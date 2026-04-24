using UnityEngine;

public class TextoFlutuante : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
        // Isso faz o texto ir para cima devagarzinho todos os frames
        transform.position += Vector3.up * 0f * Time.deltaTime;
        Destroy(this.gameObject, 0.5f);
    }
}
