using UnityEngine;

public class PhoneSystem : MonoBehaviour
{
    [Header("Referências")]
    public GameObject phoneUI;
    public Transform phoneModel; // modelo 3D do celular (opcional)
    public Player player; // seu script atual

    [Header("Config")]
    public KeyCode teclaAbrir = KeyCode.Z;
    public float velocidadeAnimacao = 10f;

    private bool aberto = false;
    private Vector3 posInicial;
    private Quaternion rotInicial;

    public Vector3 posAberto;
    public Vector3 rotAberto;

    void Start()
    {
        phoneUI.SetActive(false);

        if (phoneModel != null)
        {
            posInicial = phoneModel.localPosition;
            rotInicial = phoneModel.localRotation;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(teclaAbrir))
        {
            TogglePhone();
        }

        AnimarTelefone();
    }

    void TogglePhone()
    {
        aberto = !aberto;

        phoneUI.SetActive(aberto);

        // trava player
        player.travarCamera = aberto;
        player.moveble = !aberto;

        Cursor.lockState = aberto ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = aberto;

        Time.timeScale = aberto ? 0f : 1f;
    }

    void AnimarTelefone()
    {
        if (phoneModel == null) return;

        if (aberto)
        {
            phoneModel.localPosition = Vector3.Lerp(phoneModel.localPosition, posAberto, Time.unscaledDeltaTime * velocidadeAnimacao);
            phoneModel.localRotation = Quaternion.Lerp(phoneModel.localRotation, Quaternion.Euler(rotAberto), Time.unscaledDeltaTime * velocidadeAnimacao);
        }
        else
        {
            phoneModel.localPosition = Vector3.Lerp(phoneModel.localPosition, posInicial, Time.unscaledDeltaTime * velocidadeAnimacao);
            phoneModel.localRotation = Quaternion.Lerp(phoneModel.localRotation, rotInicial, Time.unscaledDeltaTime * velocidadeAnimacao);
        }
    }
}