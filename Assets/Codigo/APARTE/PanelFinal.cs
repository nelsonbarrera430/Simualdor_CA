using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class PanelFinal : MonoBehaviour
{
    public GameObject panelImagen;
    public TextMeshProUGUI textoPuntos;
    public PuntajeManager puntajeManagerRef;
    public string nombreEscenaMenu = "02_Configuracion";
    public float duracionAnimacion = 1.5f;

    private PuntajeManager puntajeManager;

    void Start()
    {
        if (puntajeManagerRef != null)
            puntajeManager = puntajeManagerRef;
        else
            puntajeManager = FindObjectOfType<PuntajeManager>();
    }

    public void Mostrar()
    {
        if (panelImagen != null) panelImagen.SetActive(true);
        Time.timeScale = 0f;
        if (puntajeManagerRef != null)
            puntajeManager = puntajeManagerRef;
        else
            puntajeManager = FindObjectOfType<PuntajeManager>();
        int puntajeFinal = puntajeManager != null ? puntajeManager.puntajeActual : 0;
        StartCoroutine(AnimarPuntaje(puntajeFinal));
    }

    IEnumerator AnimarPuntaje(int puntajeFinal)
    {
        float tiempoTranscurrido = 0f;
        while (tiempoTranscurrido < duracionAnimacion)
        {
            tiempoTranscurrido += Time.unscaledDeltaTime;
            int valorActual = Mathf.RoundToInt(Mathf.Lerp(0, puntajeFinal, tiempoTranscurrido / duracionAnimacion));
            if (textoPuntos != null)
                textoPuntos.text = "Puntaje: " + valorActual;
            yield return null;
        }
        if (textoPuntos != null)
            textoPuntos.text = "Puntaje: " + puntajeFinal;
    }

    public void BotonReiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BotonGuardarMenu()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("CheckpointActual", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene(nombreEscenaMenu);
    }
}
