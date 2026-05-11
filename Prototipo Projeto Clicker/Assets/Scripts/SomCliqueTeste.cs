using UnityEngine;

public class SomCliqueTeste : MonoBehaviour
{
    public PlayerAudio audioPlayer;

    private void OnMouseDown()
    {
        audioPlayer.sonsPython();
    }
}