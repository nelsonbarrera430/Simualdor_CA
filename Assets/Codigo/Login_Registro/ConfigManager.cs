using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ConfigManager : MonoBehaviour
{
    [Header("UI Feedback")]
    public TextMeshProUGUI textoEstado;

    [Header("Escena del simulador")]
    public string nombreEscenaSimulador = "03_Simulador";

    void Start()
    {
        ActualizarInterfaz();
    }

    public void SetModoEstacionar()
    {
        GameData.ModoElegido = "Estacionar";
        ActualizarInterfaz();
    }

    public void SetModoLibre()
    {
        GameData.ModoElegido = "Libre";
        ActualizarInterfaz();
    }

    public void SetModoPrueba()
    {
        GameData.ModoElegido = "Prueba";
        ActualizarInterfaz();
    }

    public void SetModoCali()
    {
        GameData.ModoElegido = "Cali";
        ActualizarInterfaz();
    }

    public void SetClimaDia()
    {
        GameData.ClimaElegido = "Dia";
        ActualizarInterfaz();
    }

    public void SetClimaNoche()
    {
        GameData.ClimaElegido = "Noche";
        ActualizarInterfaz();
    }

    public void SetClimaLluvia()
    {
        GameData.ClimaElegido = "Lluvioso";
        ActualizarInterfaz();
    }

    void ActualizarInterfaz()
    {
        if (textoEstado != null)
            textoEstado.text = "Modo: " + GameData.ModoElegido + " | Clima: " + GameData.ClimaElegido;
    }

    public void ConfirmarEIniciar()
    {
        if (GameData.ModoElegido == "Libre")
        {
            int checkpointAleatorio = Random.Range(1, 4);
            PlayerPrefs.SetInt("CheckpointActual", checkpointAleatorio);
        }
        else
        {
            PlayerPrefs.SetInt("CheckpointActual", 0);
        }

        PlayerPrefs.Save();
        SceneManager.LoadScene(nombreEscenaSimulador);
    }
}
