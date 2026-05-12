using System.Collections;
using UnityEngine;

public class HUDCreditos : MonoBehaviour
{
    public GameObject creditos;
    public GameObject canvascreditos;
    public GameObject textocreditos;
    public GameObject textocreditos2;

    HUDTutorial hudTutorial;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hudTutorial = FindAnyObjectByType<HUDTutorial>();
        creditos.gameObject.SetActive(false);
        canvascreditos.gameObject.SetActive(false);
        textocreditos.gameObject.SetActive(false);
        textocreditos2.gameObject.SetActive(false);

    }

    public IEnumerator Creditos()
    {
        creditos.gameObject.SetActive(true);
        canvascreditos.gameObject.SetActive(true);
        textocreditos.gameObject.SetActive(true);
        textocreditos2.gameObject.SetActive(true);

        Animator animCredito = creditos.GetComponent<Animator>();

        if (animCredito != null)
        {
            animCredito.Play("animacaocreditos", 0, 0f);
            yield return new WaitForSeconds(29f);
        }
        hudTutorial.gameObject.SetActive(true);
        creditos.gameObject.SetActive(false);
        canvascreditos.gameObject.SetActive(false);
        textocreditos.gameObject.SetActive(false);
        textocreditos2.gameObject.SetActive(false);
        StartCoroutine(hudTutorial.ResetJogo());
    }
}
