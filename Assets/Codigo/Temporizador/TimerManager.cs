using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviour 
{
    [Header("Configuración")]
    public float minutosIniciales = 5f;
    private float tiempoRestante;
    private bool tiempoAgotado = false;

    [Header("Referencias UI")]
    public TextMeshProUGUI textoReloj; // Aquí va el Texto_Reloj
    public GameObject panelFinal;     // Aquí va el Panel_Final

    void Start() {
        tiempoRestante = minutosIniciales * 60;
        if (panelFinal != null) panelFinal.SetActive(false);
        Time.timeScale = 1f; 
    }

    void Update() {
        if (tiempoRestante > 0) {
            tiempoRestante -= Time.deltaTime;
            ActualizarReloj();
        } else if (!tiempoAgotado) {
            tiempoAgotado = true;
            FinalizarSimulacion();
        }
    }

    void ActualizarReloj() {
        int min = Mathf.FloorToInt(tiempoRestante / 60);
        int seg = Mathf.FloorToInt(tiempoRestante % 60);
        textoReloj.text = string.Format("{0:00}:{1:00}", min, seg);
    }

    void FinalizarSimulacion() {
        if (panelFinal != null) panelFinal.SetActive(true);
        Time.timeScale = 0f; // Congela el juego
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true;
    }

    public void Reintentar() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SalirAlMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("02_Configuracion"); 
    }
}