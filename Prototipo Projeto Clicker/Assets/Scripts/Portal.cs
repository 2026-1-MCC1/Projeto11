using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Teleporta o player
            other.transform.position = new Vector3(110.9057f, 1.109f, 118.3031f);
        }
    }
}

