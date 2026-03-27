using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Teleporte
            other.transform.position = new Vector3(110.9057f, 1.109f, 118.3031f);

            // Trava só a câmera
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.travarCamera = true;
            }

        }
    }
}