using UnityEngine;

public class PortalVolta : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Teleporta o player de volta para a posição inicial
            other.transform.position = new Vector3(-0.422f, 1.109f, 5.538f);
        }
    }
}