using UnityEngine;

public class TextoFlutuante : MonoBehaviour
{
    void Start()
    {
        // O número 1.5f é o tempo (1 segundo e meio). 
        // Depois desse tempo, o texto se destrói!
        Destroy(gameObject, 1.5f);
    }

    void Update()
    {
        // Isso faz o texto ir para cima devagarzinho todos os frames
        transform.position += Vector3.up * 2f * Time.deltaTime;
    }
}
