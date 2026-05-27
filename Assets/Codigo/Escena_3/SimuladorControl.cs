using UnityEngine;

public class SimuladorControl : MonoBehaviour
{
    [Header("Contenedores de Mapas")]
    public GameObject contenedorLibre;
    public GameObject contenedorParqueo;
    public GameObject contenedorRural;

    [Header("Configuración de Cielo")]
    public Material skyDia;
    public Material skyNoche;
    public Light luzSol;

    void Awake()
    {
        
        ConfigurarEscena();
    }

    void ConfigurarEscena()
    {
        // 1. Apagar TODO primero (Limpieza)
        contenedorLibre.SetActive(false);
        contenedorParqueo.SetActive(false);
        contenedorRural.SetActive(false);

        // 2. Activar solo el elegido
        switch (GameData.ModoElegido)
        {
            case "Libre":
                contenedorLibre.SetActive(true);
                break;
            case "Estacionar":
                contenedorParqueo.SetActive(true);
                break;
            case "Rural":
                contenedorRural.SetActive(true);
                break;
        }

        // 3. Configurar Clima
        if (GameData.ClimaElegido == "Noche")
        {
            RenderSettings.skybox = skyNoche;
            luzSol.intensity = 0.1f; // Casi apagado
        }
        else
        {
            RenderSettings.skybox = skyDia;
            luzSol.intensity = 1.0f;
        }
        DynamicGI.UpdateEnvironment(); // Actualiza la luz en el mapa
    }
}